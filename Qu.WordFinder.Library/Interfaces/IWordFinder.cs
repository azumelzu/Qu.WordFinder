namespace Qu.WordFinder.Library.Interfaces
{
    public interface IWordFinder
    {
        IEnumerable<string> Find(IEnumerable<string> wordsStream);
    }
}
