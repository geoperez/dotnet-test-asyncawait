namespace AsyncAwaitTest.Abstractions
{
    using System;

    public interface ILogger
    {
        void LogMessage (string message);

        void LogError(Exception ex);
    }
}
