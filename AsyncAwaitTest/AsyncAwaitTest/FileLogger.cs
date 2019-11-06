namespace AsyncAwaitTest
{
    using System;
    using System.IO;
    using Abstractions;

    public class FileLogger : ILogger
    {
        private const string LogFile = "log.txt";
        private const string ErrorLogFile = "errlog.txt";
        private static object _object = new object();

        public void LogError(Exception ex)
        {
            lock (_object)
            {
                using var sw = new StreamWriter(ErrorLogFile, true);
                var msg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + ex.Message;
                Console.WriteLine(msg);
                sw.WriteLine(msg);
                sw.Flush();
            }
        }

        public void LogMessage(string message)
        {
            lock (_object)
            {
                using var sw = new StreamWriter(LogFile, true);
                var msg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + message;
                Console.WriteLine(msg);
                sw.WriteLine(msg);
                sw.Flush();
            }
        }
    }
}
