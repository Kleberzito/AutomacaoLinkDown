namespace AutomacaoLinkDown
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelData excelData = new ExcelData();
            PingHelper helper = new PingHelper();            
            
            string caminhoRaiz = AppDomain.CurrentDomain.BaseDirectory;
            string[] dirSplit = caminhoRaiz.Split("bin");
            string caminhoPastaLog = Path.Combine(dirSplit[0], "log");

            Console.WriteLine("Entre com o nome do arquivo de dados:");
            string file = Console.ReadLine();

            excelData.ExcelDataReturn($"{dirSplit[0]}data/", file);

            if (!Directory.Exists(caminhoPastaLog))
            {
                Directory.CreateDirectory(caminhoPastaLog);
            }

            foreach (var item in excelData.conveniado)
            {
                helper.PingIP(item.IP);
                DateTime dataAtual = DateTime.Now;
                string dataFormatada = dataAtual.ToString("dd/MM/yyyy HH:mm");

                string caminhoArquivo = Path.Combine(caminhoPastaLog, $"{item.Chamado} - {item.Empresa}.txt");
                string conteudo = $"Inicio da validação: {dataFormatada}\n" +
                                  $"{helper.listIP[0]}\n" +
                                  $"{helper.listIP[1]}\n";

                File.WriteAllText(caminhoArquivo, conteudo);
                Console.WriteLine($"Arquivo para: {item.Empresa} criado!");
            }

            Console.WriteLine("Finalizado!!!");
        }
    }
}