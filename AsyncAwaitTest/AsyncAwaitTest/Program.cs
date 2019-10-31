namespace AsyncAwaitTest
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var logger = new FileLogger();
            logger.LogMessage("Starting Async/Await test");

            // Code here

            Console.WriteLine("Press any key to finish");
            _ = Console.ReadKey();
        }
    }
}
