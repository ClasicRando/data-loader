using DataLoader.Options;

namespace DataLoader.Parsers
{
    public interface IDataParser<O> where O: IDataOptions
    {
        O Options { get; }
        IAsyncEnumerable<IEnumerable<string>> Records();
    }
}