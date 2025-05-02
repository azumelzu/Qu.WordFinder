using Qu.WordFinder.Library.Extensions;
using Qu.WordFinder.Library.Interfaces;
using System.Text;

namespace Qu.WordFinder.Library
{
    public class WordFinder : IWordFinder
    {
        private readonly List<string> _matrix;

        public WordFinder(IEnumerable<string> matrix)
        {
            // First, validate if the matrix is valid
            // I prefer to be flexible on input parameters,
            // so I will not throw an exception if the matrix contains numbers or special characters
            if (matrix is null)
                throw new ArgumentNullException(nameof(matrix), "Matrix cannot be null.");

            if (!matrix.Any()) 
                throw new ArgumentException("Matrix cannot be empty.", nameof(matrix));

            // Normalize matrix to compare invariant case
            _matrix = matrix
                .Select(s => s.NormalizeStream())
                .ToList();
        }

        public IEnumerable<string> Find(IEnumerable<string> wordsStream)
        {
            return FindAndCount(wordsStream)
                .Where(x => x.Value > 0) // Filter out words that were not found
                .OrderByDescending(x => x.Value) // Sort by frequency
                .ThenBy(x => x.Key) // Sort alphabetically
                .Take(10) // Take the top 10 most frequent words
                .Select(x => x.Key) // Select only the words
                .AsEnumerable();
        }

        internal IDictionary<string, int> FindAndCount(IEnumerable<string> wordsStream)
        {
            // I'll use a dictionary to count how many times a word appears in the matrix
            var wordsCounter = CreateWordDictionary(wordsStream);

            if (_matrix.Count == 1)
            {
                FindHorizontalWords(_matrix[0], wordsCounter);
            }
            else 
            {
                FindWords(_matrix, wordsCounter, isOriginalMatrix: true);
            }

            return wordsCounter;
        }

        private void FindWords(List<string> matrix, IDictionary<string, int> words, bool isOriginalMatrix)
        {
            // Recursive exit control
            if (matrix.Count == 0) return; 

            var columnCount = matrix[0].Length;
            var newMatrix = new List<string>();
            var first = new StringBuilder();
            var last = new StringBuilder();

            // In each iteration I'm:
            // 1 - Taking the first and last column stream
            // 2 - Removing the first and last column from the matrix (reducing the matrix) to be used in next recursive call
            // 3 - Checking if the first and last column stream contains any of the words in wordsStream
            for (var i = 0; i < matrix.Count; i++)
            {
                // If this is the very first iteration I'll find and count horizontal words
                // This is using the original matrix values. Doing this to avoid an extra loop
                if (isOriginalMatrix)
                {
                    FindHorizontalWords(matrix[i], words);
                }

                // At the end of the loop, I'll have the first and last column stream
                first.Append(matrix[i].First());

                if (columnCount > 1) // In this case of ood number of columns I'll have first but no last
                {
                    last.Append(matrix[i].Last());
                }

                // If current column count is less than 3 (2 or 1), we have finished,
                // so newMatrix will be empty for next call, making recursive call to end
                if (columnCount > 2)
                {
                    newMatrix.Add(matrix[i].Substring(1, columnCount - 2));
                }
            }

            FindVerticalWords(first.ToString(), last.ToString(), words);

            FindWords(newMatrix, words, isOriginalMatrix: false);
        }

        private void FindVerticalWords(string first, string last, IDictionary<string, int> words)
        {
            foreach (var word in words)
            {
                words[word.Key] += first.CountOccurrences(word.Key);
                words[word.Key] += last.CountOccurrences(word.Key);
            }
        }

        private void FindHorizontalWords(string stream, IDictionary<string, int> words)
        {
            foreach (var word in words)
            {
                words[word.Key] += stream.CountOccurrences(word.Key);
            }
        }

        private IDictionary<string, int> CreateWordDictionary(IEnumerable<string> wordsStream)
        {
            int initialValue = 0;
            
            // I'll first normalize the wordsStream as I do with the matrix, to compare invariant case
            // I'll also dedupe the wordsStream, so I don't have to check the same word multiple times
            return wordsStream
                .Select(w => w.NormalizeStream())
                .Distinct()
                .ToDictionary(s => s, (x) => initialValue);
        }
    }
}