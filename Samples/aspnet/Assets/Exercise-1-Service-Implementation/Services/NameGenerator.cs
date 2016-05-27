using System;
using System.Collections.Generic;
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

        // This is just a dummy. We need to read that from blob storage later
        private const string bookNameTokensDummy = "The,A|Red,Blue|Street,Road";

        public async Task<string> GenerateRandomBookTitleAsync()
        {
            var rand = new Random(Interlocked.Increment(ref seed));
            var result = new StringBuilder();

            IEnumerable<string[]> parts;
            var bookNameTokens = await Task.FromResult(bookNameTokensDummy);
            parts = bookNameTokens.Split('|').Select(s => s.Split(','));

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
