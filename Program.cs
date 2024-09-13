

namespace AutomacaoLinkDown
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelData excelData = new ExcelData();
            PingHelper helper = new PingHelper();

            excelData.ExcelDataReturn();

            foreach (var item in excelData.conveniado)
            {
                helper.PingIP(item.IP);
            }            
        }
    }
}