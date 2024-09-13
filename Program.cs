using System;
using System.IO;
using OfficeOpenXml;
using System.Text.RegularExpressions;


namespace AutomacaoLinkDown
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Alert> conveniado = new List<Alert>();

            string filePath = @"C:\Users\klebe\Downloads\wm_task (2).xlsx";

            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[0];

                    string pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
                    Regex regex = new Regex(pattern);

                    for (int row = 2; row <= excelWorksheet.Dimension.End.Row; row++)
                    {
                        string chm = "";
                        string emp = "";
                        string ip = "";

                        for (int col = 1; col <= excelWorksheet.Dimension.End.Column; col++)
                        {
                            if (col == 1)
                                chm = excelWorksheet.Cells[row, col].Text + "\t";

                            if (col == 3)
                                emp = excelWorksheet.Cells[row, col].Text + "\t";

                            if (col == 11)
                            {
                                string cellValue = excelWorksheet.Cells[row, col].Text;
                                Match match = regex.Match(cellValue);
                                if (match.Success)
                                {
                                    ip = match.Value;
                                }
                            }
                        }  
                        conveniado.Add(new Alert(chm, emp, ip));                       
                    }

                    foreach (Alert alert in conveniado)
                    {
                        Console.WriteLine(alert);
                    }
                }
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado!");
            }

        }
    }
}