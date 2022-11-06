using Options;

namespace Parsers
{
    public class DelimitedDataParser: IDataParser<DelimitedDataOptions>
    {
        public DelimitedDataParser(DelimitedDataOptions options)
        {
            this.Options = options;
        }

        public DelimitedDataOptions Options { get; }

        public async IAsyncEnumerable<IEnumerable<string>> Records()
        {
            using var reader = File.OpenText(Options.FilePath);
            var line = string.Empty;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                yield return new[] { line };
            }
        }
    }
}