namespace AsyncAwaitTest
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public static class Utils
    {
        private static object locker = new object();
        public static string GetUrl(int id)
        {
            return "https://www.gutenberg.org/files/" + id + "/" + id + "-0.txt";
        }

        public async static Task<string> GetFile(string url)
        {
            var data = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    data = await client.GetStringAsync(url); //.GetAwaiter().GetResult();
                }
            }
            catch(Exception ex)
            {
                // Log Error
            }

            return data;
        }

        public static void SaveFile(string path, string contents)
        {
            // Code here
            Monitor.Enter(locker);
            File.WriteAllText(path, contents);
            Monitor.Exit(locker);
        }
    }
}
