namespace Qu.WordFinder.Tests
{
    public class WordFinderTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenMatrixIsNull()
        {
            // Given
            Library.WordFinder wordFinder;

            // Then
            Assert.Throws<ArgumentNullException>(() => wordFinder = new Library.WordFinder(null!));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWhenMatrixIsEmpty()
        {
            // Given
            Library.WordFinder wordFinder;

            // Then
            Assert.Throws<ArgumentException>(() => wordFinder = new Library.WordFinder([]));
        }

        [Fact]
        public void ShouldFindAndCountWordsInMatrix()
        {
            // Given
            var wordStream = new string[] { "cold", "wind", "snow", "chill" };

            var matrix = new string[] 
            {
                "abcec",
                "fgwio",
                "chill",
                "pqnsd",
                "uvdxy"
            };

            var wordFinder = new Library.WordFinder(matrix);

            // When
            var result = wordFinder.FindAndCount(wordStream);

            // Then
            Assert.Equal(4, result.Count());
            Assert.Contains(result, x => x.Key == "chill" && x.Value == 1);
            Assert.Contains(result, x => x.Key == "wind" && x.Value == 1);
            Assert.Contains(result, x => x.Key == "cold" && x.Value == 1);
            Assert.Contains(result, x => x.Key == "snow" && x.Value == 0);
        }

        [Fact]
        public void ShouldNotFindAndCountDuplicatedWords()
        {
            // Given
            var wordStream = new string[] { "cold", "wind", "chill", "snow", "chill" };

            var matrix = new string[]
            {
                "abcec",
                "fgwio",
                "chill",
                "pqnsd",
                "uvdxy"
            };

            var wordFinder = new Library.WordFinder(matrix);

            // When
            var result = wordFinder.FindAndCount(wordStream);

            // Then
            Assert.Equal(4, result.Count());
            Assert.Contains(result, x => x.Key == "chill" && x.Value == 1);
            Assert.Contains(result, x => x.Key == "wind" && x.Value == 1);
            Assert.Contains(result, x => x.Key == "cold" && x.Value == 1);
            Assert.Contains(result, x => x.Key == "snow" && x.Value == 0);
        }

        [Fact]
        public void ShouldFindAndCountDuplicatedWordsInMatrix()
        {
            // Given
            var wordStream = new string[] { "cold", "wind", "chill", "snow" };

            var matrix = new string[]
            {
                "abcecc",
                "fgwioh",
                "chilli",
                "pqnsdl",
                "uvdxyl",
                "iopres"
            };

            var wordFinder = new Library.WordFinder(matrix);

            // When
            var result = wordFinder.FindAndCount(wordStream);

            // Then
            Assert.Equal(4, result.Count());
            Assert.Contains(result, x => x.Key == "chill" && x.Value == 2);
            Assert.Contains(result, x => x.Key == "wind" && x.Value == 1);
            Assert.Contains(result, x => x.Key == "cold" && x.Value == 1);
            Assert.Contains(result, x => x.Key == "snow" && x.Value == 0);
        }

        [Fact]
        public void ShouldReturnEmptySetWhenNoWordIsFound()
        {
            // Given
            var wordStream = new string[] {
                "cat",
                "dog",
                "bird"
            };

            var matrix = new string[]
            {
                "abcecc",
                "fgwioh",
                "chilli",
                "pqnsdl",
                "uvdxyl",
                "iopres"
            };

            var wordFinder = new Library.WordFinder(matrix);

            // When
            var result = wordFinder.Find(wordStream);

            // Then
            Assert.Empty(result);

        }

        [Fact]
        public void ShouldGetTopTenMostRepeatedWordsInMatrixOrderedByFrequencyAndThenAlphabetically()
        {
            // Given
            var wordStream = new string[] { 
                "cold", 
                "wind", 
                "chill", 
                "snow",
                "cat",
                "dog",
                "bird",
                "laptop",
                "pc",
                "speaker",
                "code",
                "house"
            };

            var matrix = new string[]
            {
                "abceccs",
                "fgwiohn",
                "chillio",
                "pqnsdlw",
                "uvdxyli",
                "lomaneg",
                "iopresi",
                "laptopi",
                "abcdpci",
                "catttti",
                "doggggi",
                "birdddd",
                "speaker",
                "codeeee"
            };

            var wordFinder = new Library.WordFinder(matrix);

            // When
            var findAndCountResult = wordFinder.FindAndCount(wordStream).ToDictionary();
            var findResult = wordFinder.Find(wordStream);

            // Then

            // Verify correct count
            Assert.Equal(12, findAndCountResult.Count());
            Assert.Equal(3, findAndCountResult["pc"]);
            Assert.Equal(2, findAndCountResult["chill"]);
            Assert.Equal(1, findAndCountResult["cold"]);
            Assert.Equal(1, findAndCountResult["wind"]);
            Assert.Equal(1, findAndCountResult["snow"]);
            Assert.Equal(1, findAndCountResult["cat"]);
            Assert.Equal(1, findAndCountResult["dog"]);
            Assert.Equal(1, findAndCountResult["bird"]);
            Assert.Equal(1, findAndCountResult["laptop"]);
            Assert.Equal(1, findAndCountResult["speaker"]);
            Assert.Equal(1, findAndCountResult["code"]);
            Assert.Equal(0, findAndCountResult["house"]);

            // Verify end result
            Assert.Equal(10, findResult.Count());
            Assert.Equal("pc", findResult.ElementAt(0));
            Assert.Equal("chill", findResult.ElementAt(1));
            Assert.Equal("bird", findResult.ElementAt(2));
            Assert.Equal("cat", findResult.ElementAt(3));
            Assert.Equal("code", findResult.ElementAt(4));
            Assert.Equal("cold", findResult.ElementAt(5));
            Assert.Equal("dog", findResult.ElementAt(6));
            Assert.Equal("laptop", findResult.ElementAt(7));
            Assert.Equal("snow", findResult.ElementAt(8));
            Assert.Equal("speaker", findResult.ElementAt(9));
        }
    }
}