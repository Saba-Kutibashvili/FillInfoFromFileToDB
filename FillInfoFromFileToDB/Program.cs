using FillInfoFromFileToDB;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System.Data;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var data = new List<GovNumber>();

using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"C:\Users\Saba\source\repos\FillInfoFromFileToDB\FillInfoFromFileToDB\Data.xlsx")))
{
    var myWorksheet = xlPackage.Workbook.Worksheets.First();
    var totalRows = myWorksheet.Dimension.End.Row;
    var totalColumns = myWorksheet.Dimension.End.Column;

    for (int rowNum = 2; rowNum <= totalRows; rowNum++)
    {
        var govNum = new GovNumber();
        var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToArray();

        govNum.CreateDate = DateTime.Parse(row[0]);
        govNum.IsActive = bool.Parse(row[1]);
        govNum.IsDeleted = bool.Parse(row[2]);
        govNum.IsHidden = bool.Parse(row[3]);

        govNum.GovNumber_FullNumber = row.Length > 4 && row[4].Length == 7 ? row[4]:string.Empty;
        govNum.VinCode = row.Length > 4 && row[4].Length > 7 ? row[4] : row.Length > 5 ? row[5] : string.Empty;

        data.Add(govNum);
    }
}

SqlConnection connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; Initial Catalog=master; Integrated Security=True");

string DatabaseName = "Data";
string TableName = "GovNumbers";

try
{
    connection.Open();

    GovNumberData.CreateDatabase(connection, DatabaseName);
    GovNumberData.CreateTable(connection, TableName);
    GovNumberData.AddRows(connection, TableName, data);

    Console.WriteLine("Finished");
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
    connection.Close();
}
