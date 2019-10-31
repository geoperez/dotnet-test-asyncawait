namespace AsyncAwaitTest
{
    using System;
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
            catch(Exception ex)
            {
                // Log Error
            }

            return data;
        }

        public static void SaveFile(string path, string contents)
        {
            // Code here
        }
    }
}
