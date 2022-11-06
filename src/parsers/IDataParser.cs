using Options;

namespace Parsers
{
    public interface IDataParser<O> where O: IDataOptions
    {
        O Options { get; }
        IAsyncEnumerable<IEnumerable<string>> Records();
    }
}