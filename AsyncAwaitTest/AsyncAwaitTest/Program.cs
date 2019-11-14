namespace AsyncAwaitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {

        private static Dictionary<string, int> bagOfWords = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            var logger = new FileLogger();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFo‌​lder.Desktop), "Books");
            logger.LogMessage("Starting Async/Await test");
            createFolder(path);
            List<Task> tasks = new List<Task>();
           foreach(var book in Info.Books) {
                tasks.Add(Task.Factory.StartNew(async () => {
                    logger.LogMessage("Starting downloading book :" + book.Id + " " + book.Title);
                    var bookFile = await Utils.GetFile(Utils.GetUrl(book.Id));
                    logger.LogMessage("Finishing downloading book :" + book.Id + " " + book.Title);
                    return bookFile;
                })
                    .ContinueWith((bookFile) => {
                        logger.LogMessage("Starting saving book :" + book.Id + " " + book.Title);
                        Utils.SaveFile(Path.Combine(path, book.Title + ".txt"), bookFile.Result.Result);
                        logger.LogMessage("Finishing saving book :" + book.Id + " " + book.Title);
                        return bookFile;
                    })
                        .ContinueWith((bookFile) => {
                            logger.LogMessage("Starting makeing the bag of words :" + book.Id + " " + book.Title);
                            BagOfWords(bookFile.Result.Result.Result);
                            logger.LogMessage("Finishing makeing the bag of words:" + book.Id + " " + book.Title);
                        }));
           }
            Task.WhenAll(tasks).ContinueWith((x)=>
            {
                logger.LogMessage("Counting words in the bag: ");
                Console.WriteLine("Counting words in the bag: ");
                foreach (var word in bagOfWords)
                {
                    Console.WriteLine($"The word: {word.Key} appears {word.Value} times");
                    logger.LogMessage($"The word: {word.Key} appears {word.Value} times");
                };
                Console.WriteLine("Press any key to finish");
            });


            // Code here
            Console.WriteLine("Wait I am processing the data");
            
            _ = Console.ReadKey();
        }

        static void createFolder(string path)
        {        

            try
            {
                if(Directory.Exists(path))
                {
                    Directory.Delete(path, true);                    
                }
                Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {

            }
        }

        static void BagOfWords(string bookFile)
        {
            Monitor.Enter(bagOfWords);
            var bookString = new string(bookFile.Where(c => char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)).ToArray());
            bookString = bookString.Replace(System.Environment.NewLine, string.Empty);
            string[] words = bookString.Split(" "); 

            foreach(string word in words)
            {
                if (bagOfWords.ContainsKey(word))
                {
                    bagOfWords[word] = bagOfWords[word] + 1;
                }
                else
                {
                    bagOfWords.Add(word, 1);
                }
            }
            Monitor.Exit(bagOfWords);
        }
    }
}
