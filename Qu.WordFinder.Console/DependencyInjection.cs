using Microsoft.Extensions.DependencyInjection;
using Qu.WordFinder.Library.Interfaces;

namespace Qu.WordFinder.Console
{
    internal static class DependencyInjection
    {
        public static ServiceProvider CreateServices(IEnumerable<string> matrix)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IWordFinder>(new Library.WordFinder(matrix))
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
