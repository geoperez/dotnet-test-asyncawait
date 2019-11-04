namespace AsyncAwaitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Abstractions;

    public class FileLogger : ILogger
    {
        private const string LogFile = "log.txt";
        private const string ErrorLogFile = "errlog.txt";
        private const string BoWFile = "bow.txt";

        public void LogError(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void LogMessage(string message)
        {
            using (var sw = new StreamWriter(LogFile, true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + message);
                sw.Flush();
            }
        }

        public void LogBoW(Dictionary<string, int> dictionary)
        {
            using (var sw = new StreamWriter(BoWFile, true))
            {
                foreach (var entry in dictionary)
                {
                    sw.WriteLine("[{0} {1}]", entry.Key, entry.Value);
                }
            }
        }
    }
}
