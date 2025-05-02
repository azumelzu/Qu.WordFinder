namespace Qu.WordFinder.Library.Extensions
{
    internal static class StringExtensions
    {
        public static string NormalizeStream(this string str) => str.ToLower();

        public static int CountOccurrences(this string str, string word)
        {
            int count = 0;
            var index = str.IndexOf(word, StringComparison.OrdinalIgnoreCase);
            while (index != -1)
            {
                count++;
                index = str.IndexOf(word, index + word.Length, StringComparison.OrdinalIgnoreCase);
            }

            return count;
        }
    }
}
