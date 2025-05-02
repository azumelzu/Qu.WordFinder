using Microsoft.Extensions.DependencyInjection;
using Qu.WordFinder.Console;
using Qu.WordFinder.Library.Interfaces;

var wordStream = new string[] { "cold", "wind", "snow", "chill", "chill" };

var matrix = new string[]
{
        "abcocc",
        "fgwioh",
        "chilli",
        "pqnsdl",
        "uvdxyl",
        "uvdxyl"
};

var services = DependencyInjection.CreateServices(matrix);

IWordFinder wordFinder = services.GetRequiredService<IWordFinder>();

var words = wordFinder.Find(wordStream);

Console.WriteLine("Top 10 most repeated words found in the matrix:");
foreach (var word in words)
{
    Console.WriteLine(word);
}

