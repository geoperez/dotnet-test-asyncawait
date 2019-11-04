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
            var data = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    data = client.GetStringAsync(url).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage(ex.Message);
            }

            return data;
        }

        public static void SaveFile(string path, string contents)
        {
            var directoryName = Path.GetDirectoryName(path);
            Directory.CreateDirectory(directoryName);
            
            File.WriteAllText(Path.Combine(directoryName, path), contents);
        }
    }
}
