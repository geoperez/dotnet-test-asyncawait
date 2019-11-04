namespace AsyncAwaitTest
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var wordPattern = new Regex(@"\w+");
            var logger = new FileLogger();
            var words = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);

            logger.LogMessage("Starting Async/Await test");

            var logSempahore = new Semaphore(1, 1);
            var getSemaphore = new Semaphore(5, 5);
            var bowSemaphore = new Semaphore(1, 1);

            var books = new List<string[]>();
            logger.LogMessage("Starting Download");
            Parallel.ForEach(Info.Books, x =>
            {
                getSemaphore.WaitOne();
                var book = Utils.GetFile(Utils.GetUrl(x.Id));
                getSemaphore.Release();

                if (book.Length > 0)
                {
                    logSempahore.WaitOne();
                    logger.LogMessage($"Finished Download{x.Title}");
                    logSempahore.Release();

                    logSempahore.WaitOne();
                    logger.LogMessage($"Saving {x.Title}");
                    logSempahore.Release();

                    Utils.SaveFile(x.Title, book);

                    logSempahore.WaitOne();
                    logger.LogMessage($"Suceesfully saved {x.Title}");
                    logSempahore.Release();

                    bowSemaphore.WaitOne();
                    foreach (Match match in wordPattern.Matches(book))
                    {
                        var currentCount = 0;
                        var word = Regex.Replace(match.Value, @"[^0 - 9A - Za - z._\s]", "");
                        words.TryGetValue(match.Value, out currentCount);

                        currentCount++;
                        words[match.Value] = currentCount;
                    }
                    bowSemaphore.Release();
                }
            });

            logger.LogMessage("Started to log BoW");
            logger.LogBoW(words);
            logger.LogMessage("Finished to log BoW");

            Console.WriteLine("Press any key to finish");
            _ = Console.ReadKey();
        }
    }
}
