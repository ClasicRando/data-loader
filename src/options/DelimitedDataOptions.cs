namespace DataLoader.Options
{
    public struct DelimitedDataOptions : IDataOptions
    {
        public char Delimiter { get; }
        public bool HasHeader { get => true; }
        public bool IsQualified { get; }
        public string FilePath { get; }
        public DelimitedDataOptions(string filePath, char delimiter, bool isQualified)
        {
            Delimiter = delimiter;
            FilePath = filePath;
            IsQualified = isQualified;
        }
    }
}