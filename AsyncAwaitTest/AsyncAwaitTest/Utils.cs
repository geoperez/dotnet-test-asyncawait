namespace AsyncAwaitTest
{
    using System;
    using System.IO;
    using System.Net.Http;

    public static class Utils
    {
        public static string GetUrl(int id)
        {
            return "https://www.gutenberg.org/files/" + id + "/" + id + "-0.txt";
        }

        public static string GetFile(string url)
        {
            var logger = new FileLogger();
            var data = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    data = client.GetStringAsync(url).GetAwaiter().GetResult();
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex);
            }

            return data;
        }

        public static void SaveFile(string path, string contents)
        {
            var pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var pathToSave = $"{pathDesktop}/Books/{path}.txt";
            using (var sw = new StreamWriter(pathToSave, true))
            {
                sw.WriteLine(contents);
                sw.Flush();
            }
        }
    }
}
