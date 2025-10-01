using System.Text;
using System.Text.RegularExpressions;

namespace Mvc0Compress.RLE
{
    public class Program
    {
        static void Main(string[] args)
        {
            string text = "aaabbbcccaaa";
            string compressedText = RLE.Compress(text);
            string decompressedText = RLE.Decompress(compressedText);

            Console.WriteLine($"Original: {text} - {text.Count()}");
            Console.WriteLine($"Compressed: {compressedText} - {compressedText.Count()}");
            Console.WriteLine($"Decompressed: {decompressedText} - {decompressedText.Count()}");
        }
    }

    public static class RLE
    {
        /// <summary>
        /// Compresses a string using Run-Length Encoding (RLE).
        /// Ex: "aaabbc" becomes "3a2b1c"
        /// </summary>
        /// <param name="input">The input string to be compressed.</param>
        /// <returns>The compressed string.</returns>
        public static string Compress(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            StringBuilder compressed = new StringBuilder();
            int count = 1;

            for(int i = 1; i < input.Length; i++)
            {
                if (input[i] == input[i - 1])
                {
                    count++;
                }
                else
                {
                    compressed.Append(count).Append(input[i - 1]);
                    count = 1;
                }

            }

            compressed.Append(count).Append(input[input.Length - 1]);
            return compressed.ToString();
        }

        /// <summary>
        /// Decompresses a string encoded with Run-Length Encoding (RLE).
        /// Ex: "3a2b1c" becomes "aaabbc"
        /// </summary>
        /// <param name="compressed">The compressed input string.</param>
        /// <returns>The decompressed string.</returns>
        public static string Decompress(string compressed)
        {
            if(string.IsNullOrEmpty(compressed))
            {
                return string.Empty;
            }

            string pattern = @"(\d+)(\D)";

            string decompressed = Regex.Replace(compressed, pattern, match =>
            {
                int quantity = int.Parse(match.Groups[1].Value);
                string character = match.Groups[2].Value;

                return new string(character[0], quantity);
            });

            return decompressed;
        }
    }
}
