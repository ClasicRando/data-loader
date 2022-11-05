using Options;

namespace Parsers
{
    class DelimitedDataParser: IDataParser
    {
        public DelimitedDataParser(string filePath, char delimiter, bool isQualified)
        {
            this.Options = new DelimitedDataOptions(filePath, delimiter, isQualified);
        }

        public IDataOptions Options { get; }

        public async IAsyncEnumerable<string> Records()
        {
            using var reader = File.OpenText(Options.FilePath);
            var line = string.Empty;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                yield return line;
            }
        }
    }
}