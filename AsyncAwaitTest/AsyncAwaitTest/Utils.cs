namespace AsyncAwaitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class Utils
    {
        public static string GetUrl(int id)
        {
            return "https://www.gutenberg.org/files/" + id + "/" + id + "-0.txt";
        }

        public static async Task<string> GetFile(string url, FileLogger logger)
        {
            var data = string.Empty;

            try
            {
                using var client = new HttpClient();
                data = await client.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }

            return data;
        }

        public static async Task SaveFileAsync(string path, string contents) =>
            await File.WriteAllTextAsync(path, contents);
        

        public static async Task<string> ReadFileAsync(string path) =>
            await File.ReadAllTextAsync(path);
        

        public static Dictionary<string, int> BuildBagOfWords(string text)
        {
            var cleanText = Regex.Replace(text, "[^a-zA-Z0-9 ]+", "", RegexOptions.Compiled);
            var words = cleanText
                .Split(" ");

            var result = words
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());

            return result;
        }

        public static string DictToString(Dictionary<string, int> dict)
        {
            StringBuilder result = new StringBuilder();
            foreach (var a in dict)
            {
                result.Append($"{a.Key}: {a.Value} \n");
            }

            return result.ToString();
        }
    }
}
