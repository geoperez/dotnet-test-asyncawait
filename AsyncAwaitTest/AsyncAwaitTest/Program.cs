namespace AsyncAwaitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var logger = new FileLogger();
            logger.LogMessage("Starting Async/Await test");
            var path = "C:/Users/ramiro.flores/Desktop/Books/";

            var downloadTasks = new List<Task>();
            var bows = new List<Dictionary<string, int>>();

            Info.Books.ForEach((x) =>
                            downloadTasks.Add(new Task(() =>
                                Utils.SaveFile(path + $"{x.Title}.txt", Utils.GetFile(Utils.GetUrl(x.Id)))
                                )));

            downloadTasks.ForEach(x => x.Start());
            Task.WaitAll(downloadTasks.ToArray());

            var directory = new DirectoryInfo(path);
            var bookToBoowTasks = new List<Task>();

            Info.Books.ForEach((x) =>
                           bookToBoowTasks.Add(new Task(
                               () =>
                               {
                                   //create the bow
                                   var text = File.ReadAllText(path + $"{x.Title}.txt");
                                   var words = Regex.Matches(text, @"\w+").Cast<Match>().Where(m => m.Success).Select(m => m.Value);
                                   var bow = words.GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count());
                                   bows.Add(bow);
                               }
                               )
                           ));

            bookToBoowTasks.ForEach(x => x.Start());
            Task.WaitAll(bookToBoowTasks.ToArray());

            bows.ForEach(x => {
                foreach (KeyValuePair<string, int> kvp in x)
                {
                    logger.LogMessage($"Key = {kvp.Key}, Value = {kvp.Value}");
                }
            });


            Console.WriteLine("Press any key to finish");
            _ = Console.ReadKey();
        }

    }
}
