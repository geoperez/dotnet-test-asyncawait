namespace AsyncAwaitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static string basePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Books");
        private static int count = 0;

        static async Task Main(string[] args)
        {
            Directory.CreateDirectory(basePath);
            var bags = new SyncQueue<Dictionary<string, int>>();
            var resultBag = new Dictionary<string, int>();
            var logger = new FileLogger();
            logger.LogMessage("Starting Async/Await test");

            var timer = new Timer((a) => // Consumer
            {
                logger.LogMessage($"Timer Check");
                if (bags.TryGetItem(out var bag))
                {
                    foreach (var word in bag)
                    {
                        if (resultBag.TryGetValue(word.Key, out var value))
                            resultBag[word.Key] = value + word.Value;
                        else
                            resultBag.Add(word.Key, word.Value);
                    }
                    logger.LogMessage($"Book Merged");
                    Interlocked.Increment(ref count);
                }

            }, null, 5000, 1000);


            Parallel.ForEach(Info.Books, (book) =>
            {
                var text = Utils.GetFile(Utils.GetUrl(book.Id), logger).GetAwaiter().GetResult();
                logger.LogMessage($"Downloaded file {book.Title}");
                Utils.SaveFileAsync(Path.Combine(basePath, $"{book.Title}.txt"), text).GetAwaiter().GetResult();
                logger.LogMessage($"Saved file {book.Title}");

                var readText = Utils.ReadFileAsync(Path.Combine(basePath, $"{book.Title}.txt")).GetAwaiter().GetResult();
                logger.LogMessage($"Readed file {book.Title}");
                bags.Enqueue(Utils.BuildBagOfWords(readText));
                logger.LogMessage($"Bag of Words - {book.Title}");
            });

            while (count < Info.Books.Count) // Waiting for all bags to be merged
                await Task.Delay(100);

            timer.Dispose();

            logger.LogMessage("Dictionary to String");
            await Utils.SaveFileAsync(Path.Combine(basePath, "final.txt"), Utils.DictToString(resultBag));
            logger.LogMessage("resultBag.txt");

            Console.WriteLine("Press any key to finish");
            _ = Console.ReadKey();
        }
    }
}
