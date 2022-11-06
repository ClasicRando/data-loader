namespace Options
{
    public struct ExcelDataOptions: IDataOptions
    {
        public char Delimiter { get => ','; }
        public bool HasHeader { get => false; }
        public bool IsQualified { get => true; }
        public string FilePath { get; }
        public string SheetName { get; }
        public ExcelDataOptions(string filePath, string sheetName)
        {
            FilePath = filePath;
            SheetName = sheetName;
        }
    }
}