namespace DataLoader.Options
{
    public interface IDataOptions
    {
        public char Delimiter { get; }
        public bool HasHeader { get; }
        public bool IsQualified { get; }
        public string FilePath { get; }
    }
}