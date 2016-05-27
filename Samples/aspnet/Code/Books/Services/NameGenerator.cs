using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Services
{
    /// <summary>
    /// Implements a generator for dummy book names
    /// </summary>
    public class NameGenerator : INameGenerator
    {
        private static int seed = 0;

        private static string bookNameTokensUrl = ConfigurationManager.AppSettings["BookNameTokenUrl"];

        public async Task<string> GenerateRandomBookTitleAsync()
        {
            var rand = new Random(Interlocked.Increment(ref seed));
            var result = new StringBuilder();

            IEnumerable<string[]> parts;
            using (var client = new HttpClient())
            {
                var bookNameTokens = await client.GetStringAsync(bookNameTokensUrl);
                parts = bookNameTokens.Split('|').Select(s => s.Split(','));
            };

            foreach (var part in parts)
            {
                if (result.Length > 0)
                {
                    result.Append(' ');
                }

                result.Append(part[rand.Next(part.Length)]);
            }

            return result.ToString();
        }
    }
}
