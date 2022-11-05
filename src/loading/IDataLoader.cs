using Npgsql;
using Options;
using Parsers;

namespace Loading
{
    interface IDataLoader
    {
        Task<long> LoadData();
    }
}