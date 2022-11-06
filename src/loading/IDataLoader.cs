using LanguageExt.Common;

namespace DataLoader.Loading
{
    interface IDataLoader
    {
        Task<Result<long>> LoadData();
    }
}