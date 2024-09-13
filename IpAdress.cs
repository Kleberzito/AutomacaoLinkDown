using System;
using System.Net;

namespace AutomacaoLinkDown
{
    internal class IpAddress
    {
        public string IpLink { get; set; }
        public string IpServe { get; set; }
        public int Enviados { get; set; }
        public float Resposta { get; set; }
        public int QntBytes { get; set; }
        public int TtlData { get; set; }
        public int Recebidos { get; set; }
        public int Perdidos { get; set; }
        public int nTeste { get; set; }
        public string ipStatus { get; set; }

        public IpAddress(string ipServe, int nTeste)
        {
            string[] ip = ipServe.Split('.');
            int valueSplit = int.Parse(ip[3]);
            valueSplit = valueSplit - 1;

            IpLink = $"{ip[0]}.{ip[1]}.{ip[2]}.{valueSplit}";
            IpServe = ipServe;
            this.nTeste = nTeste;
        }

        public void calValue()
        {
            Enviados = Enviados / nTeste;
            Recebidos = Recebidos / nTeste;
            Resposta = Resposta / nTeste;
            QntBytes = QntBytes / nTeste;
            if (ipStatus == "Success")
                TtlData = TtlData / nTeste;
            Perdidos = Perdidos / nTeste;
        }

        public string stringValue(string value)
        {
            return $"Ping para {value}: {ipStatus}\n" +
                   $"Tempo de resposta: {Resposta} ms\n" +
                   $"Tamanho do buffer: {QntBytes} bytes\n" +
                   $"TTL (Time to Live): {TtlData}\n";
        }
    }
}