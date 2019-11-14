namespace AsyncAwaitTest
{
    using System;
    using System.IO;
    using System.Threading;
    using Abstractions;

    public class FileLogger : ILogger
    {   private static object locker = new object();
        private const string LogFile = "log.txt";
        private const string ErrorLogFile = "errlog.txt";

        public void LogError(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void LogMessage(string message)
        {
            Monitor.Enter(locker);
            using (var sw = new StreamWriter(LogFile, true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + message);
                sw.Flush();
            }
            Monitor.Exit(locker);
        }
    }
}
