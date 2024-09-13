using OfficeOpenXml;
using OfficeOpenXml.Packaging.Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutomacaoLinkDown
{
    public class ExcelData
    {
        public List<Alert> conveniado = new List<Alert>();

        public void ExcelDataReturn(string path, string file) 
        {
            string filePath = $"{path}{file}";

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
                                chm = excelWorksheet.Cells[row, col].Text;

                            if (col == 3)
                                emp = excelWorksheet.Cells[row, col].Text;

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
                }
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado!");
            }
        }
    }
}
