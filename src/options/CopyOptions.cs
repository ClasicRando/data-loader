namespace DataLoader.Options
{
    public class CopyOptions : ICopyOptions
    {
        private readonly string _tableName;
        private readonly string[] _columns;
        public CopyOptions(string tableName, string[] columns)
        {
            _tableName = tableName;
            _columns = columns;
        }

        public string CopyStatement(IDataOptions options)
        {
            var columnNames = string.Join(',', _columns);
            var header = options.HasHeader ? "true" : "false";
            var qualifierOptions = options.IsQualified ? ", QUOTE '\"', ESCAPE '\"'" : "";
            return $"COPY {_tableName}({columnNames}) FROM STDIN WITH (FORMAT csv, DELIMITER '{options.Delimiter}', HEADER '{header}', NULL ''{qualifierOptions}";
        }
    }
}