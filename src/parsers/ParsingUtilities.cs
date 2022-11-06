namespace DataLoader.Parsers
{
    public static class ParsingUtilities
    {
        public static string escapeCsvString(string csvValue)
        {
            if (csvValue.Any(c => c == ',' || c == '"' || c == '\n' || c == '\r'))
            {
                return $"\"{csvValue.Replace(",", "")}\"";
            }
            return csvValue;
        }
    }
}