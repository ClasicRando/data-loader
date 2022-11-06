using NPOI.SS.UserModel;
using Options;

namespace Parsers
{
    public class ExcelDataParser : IDataParser<ExcelDataOptions>
    {
        public ExcelDataParser(ExcelDataOptions options)
        {
            Options = options;
        }

        public ExcelDataOptions Options { get; }

        public async IAsyncEnumerable<IEnumerable<string>> Records()
        {
            using var stream = new FileStream(Options.FilePath, FileMode.Open);
            var workbook = WorkbookFactory.Create(stream, true);
            var sheet = workbook.GetSheet(Options.SheetName)
                ?? throw new Exception($"Could not find sheet name \"{Options.SheetName}\" in workbook");
            var headerRow = sheet.GetRow(sheet.FirstRowNum)
                ?? throw new Exception($"Could not find the first row ({sheet.FirstRowNum})");
            var evaluator = WorkbookFactory.CreateFormulaEvaluator(workbook);
            evaluator.EvaluateAll();

            for (var i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i)
                    ?? throw new Exception($"Could not find the  row #{i}");;
                var cells = row.Cells;
                if (cells.Count != headerRow.Cells.Count)
                {
                    throw new Exception($"Found row with more cells than header. Expected {headerRow.Cells.Count} got {cells.Count}");
                }
                yield return row.Cells.Select(c => MapCellValue(c, evaluator));
            }
            workbook.Close();
        }

        string MapCellValue(ICell cell, IFormulaEvaluator evaluator)
        {
            if (cell == null)
            {
                return string.Empty;
            }
            if (cell.CellType == CellType.Formula)
            {
                evaluator.EvaluateInCell(cell);
            }
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue ? "true" : "false";
                case CellType.Error:
                    throw new Exception($"Cell Error: {cell.Address}");
                case CellType.Formula:
                    throw new Exception($"Found Unevaluated Formula: {cell.Address}");
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue.ToString("u");
                    }
                    return cell.NumericCellValue.ToString() ?? "";
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Unknown:
                    throw new Exception($"Unknown Cell Type: {cell.Address}");
            }
            return string.Empty;
        }
    }
}