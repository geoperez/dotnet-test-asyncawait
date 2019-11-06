namespace AsyncAwaitTest
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    class OldVersion
    {
        static string basePath = "C:\\Users\\jose.correa\\Desktop\\Books\\";
        private static Dictionary<string, int> resultBag = new Dictionary<string, int>();
        private static bool _spinLock = false;
        private static object _object = new object();

        static async Task OldVersionMain(string[] args)
        {
            var logger = new FileLogger();
            logger.LogMessage("Starting Async/Await test");


            Parallel.ForEach(Info.Books, (book) =>
            {
                var text = Utils.GetFile(Utils.GetUrl(book.Id), logger).GetAwaiter().GetResult();
                logger.LogMessage($"Downloaded file {book.Title}");
                Utils.SaveFileAsync($"{basePath}{book.Title}.txt", text).GetAwaiter().GetResult();
                logger.LogMessage($"Saved file {book.Title}");
            });

            // Code here
            //Downloading and saving files
            /*var tasks = new List<Task>();
            foreach (var book in Info.Books)
            {
                tasks.Add(Task.Run(async () => {
                    var text = await Utils.GetFile(Utils.GetUrl(book.Id), logger);
                    logger.LogMessage($"Downloaded file {book.Title}");
                    await Utils.SaveFileAsync($"{basePath}{book.Title}.txt", text);
                    logger.LogMessage($"Saved file {book.Title}");
                }));
            }
            await Task.WhenAll(tasks.ToArray());*/

            var bags = new List<Dictionary<string, int>>();
            SpinLock sl = new SpinLock(false);
            Parallel.ForEach(Info.Books, (book) =>
            {
                var text = Utils.ReadFileAsync($"{basePath}{book.Title}.txt").GetAwaiter().GetResult();
                logger.LogMessage($"Readed file {book.Title}");
                _spinLock = false;
                sl.Enter(ref _spinLock);
                bags.Add(Utils.BuildBagOfWords(text));
                sl.Exit(false);
                logger.LogMessage($"Bag of Words - {book.Title}");
            });
            //Reading files
            /*var bags = new List<Dictionary<string, int>>();
            var tasksRead = new List<Task>();
            foreach (var book in Info.Books)
            {
                tasksRead.Add(Task.Run(async () => {
                    var text = await Utils.ReadFileAsync($"{basePath}{book.Title}.txt");
                    logger.LogMessage($"Readed file {book.Title}");
                    bags.Add(Utils.BuildBagOfWords(text));
                    logger.LogMessage($"Bag of Words - {book.Title}");
                }));
            }
            await Task.WhenAll(tasksRead.ToArray());*/

            //Merege Bags
            SpinLock sl1 = new SpinLock(false);
            Parallel.ForEach(bags, (bag) =>
            {
                foreach (var word in bag)
                {
                    var lockTaken = false;
                    sl1.Enter(ref lockTaken);
                    if (resultBag.TryGetValue(word.Key, out var value))
                    {
                        resultBag[word.Key] = value + word.Value;
                    }
                    else
                    {
                        resultBag.Add(word.Key, word.Value);
                    }
                    sl1.Exit();
                }
            });

            logger.LogMessage("Dictionary to String");
            await Utils.SaveFileAsync($"{basePath}finalBag.txt", Utils.DictToString(resultBag));
            logger.LogMessage("resultBag.txt");

            Console.WriteLine("Press any key to finish");
            _ = Console.ReadKey();
        }
    }
}
