using Options;

namespace Parsers
{
    interface IDataParser
    {
        IDataOptions Options { get; }
        IAsyncEnumerable<string> Records();
    }
}