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
