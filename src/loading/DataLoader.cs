using LanguageExt.Common;
using Npgsql;
using DataLoader.Options;
using DataLoader.Parsers;

namespace DataLoader.Loading
{
    class DataLoader<O> : IDataLoader where O: IDataOptions
    {
        private readonly NpgsqlConnection _connection;
        private readonly ICopyOptions _copyOptions;
        private readonly IDataParser<O> _parser;

        public DataLoader(NpgsqlConnection connection, ICopyOptions copyOptions, IDataParser<O> parser)
        {
            _connection = connection;
            _copyOptions = copyOptions;
            _parser = parser;
        }

        public async Task<Result<long>> LoadData()
        {
            var copyCommand = _copyOptions.CopyStatement(_parser.Options);
            using var writer = await _connection.BeginTextImportAsync(copyCommand);
            long recordCount = 0;
            await foreach (var record in _parser.Records())
            {
                var line = string.Join(',', record.Select(s => ParsingUtilities.escapeCsvString(s)));
                await writer.WriteLineAsync(line);
                recordCount++;
            }
            return recordCount;
        }
    }
}