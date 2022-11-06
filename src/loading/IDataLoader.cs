using LanguageExt.Common;

namespace Loading
{
    interface IDataLoader
    {
        Task<Result<long>> LoadData();
    }
}