using Npgsql;
using Options;
using Parsers;

namespace Loading
{
    class DataLoader : IDataLoader
    {
        private readonly NpgsqlConnection _connection;
        private readonly ICopyOptions _copyOptions;
        private readonly IDataParser _parser;

        public DataLoader(NpgsqlConnection connection, ICopyOptions copyOptions, IDataParser parser)
        {
            _connection = connection;
            _copyOptions = copyOptions;
            _parser = parser;
        }

        public async Task<long> LoadData()
        {
            var copyCommand = _copyOptions.CopyStatement(_parser.Options);
            using var writer = await _connection.BeginTextImportAsync(copyCommand);
            long recordCount = 0;
            await foreach (var record in _parser.Records())
            {
                await writer.WriteAsync(record);
                recordCount++;
            }
            return recordCount;
        }
    }
}