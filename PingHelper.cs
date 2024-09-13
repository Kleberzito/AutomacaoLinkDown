using System.Net;
using System.Net.NetworkInformation;

namespace AutomacaoLinkDown
{
    public class PingHelper
    {
        public void PingIP(string ipAddress)
        {
            string[] ip = ipAddress.Split('.');
            int nIPSplit = int.Parse(ip[3]);
            nIPSplit = nIPSplit - 1;
            string newIP = $"{ip[0]}.{ip[1]}.{ip[2]}.{nIPSplit}";

            using (Ping ping = new Ping())
            {
                int enviados = 0;
                float resposta = 0;
                int nBytes = 0;
                int ttl = 0;
                int recebidos = 0;
                int perdidos = 0;                

                try
                {
                    for (int i = 0; i < 4; i++)
                    {
                        PingReply reply = ping.Send(ipAddress);
                        enviados++;
                        resposta += reply.RoundtripTime;
                        nBytes += reply.Buffer.Length;                        

                        if (reply.Status == IPStatus.Success)
                        {
                            recebidos++;
                            ttl += reply.Options.Ttl;
                        }
                        else
                        {
                            perdidos++;
                        }
                    }

                    resposta = resposta / 4;
                    nBytes = nBytes / 4;
                    ttl = ttl / 4;

                    Console.WriteLine($"NUC responde OK:\n");
                    Console.WriteLine($"Ping para {ipAddress} bem-sucedido:");
                    Console.WriteLine($"Tempo de resposta: {resposta} ms");
                    Console.WriteLine($"Tamanho do buffer: {nBytes} bytes");
                    Console.WriteLine($"TTL (Time to Live): {ttl}");
                    Console.WriteLine();
                    /*
                    else
                    {
                        reply = ping.Send(newIP);
                        if (reply.Status == IPStatus.Success)
                        {
                            Console.WriteLine($"NUC não responde NOK: {ipAddress}: {reply.Status}, Link responde OK {newIP}: {reply.Status}\n");
                            Console.WriteLine($"Ping para {newIP} bem-sucedido:");
                            Console.WriteLine($"Tempo de resposta: {reply.RoundtripTime} ms");
                            Console.WriteLine($"Tamanho do buffer: {reply.Buffer.Length} bytes");
                            Console.WriteLine($"TTL (Time to Live): {reply.Options.Ttl}");
                            Console.WriteLine($"Fragmentação permitida: {reply.Options.DontFragment}");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine($"Link não responde NOK: {newIP}: {reply.Status}");
                            Console.WriteLine($"Tempo de resposta: {reply.RoundtripTime} ms");
                            Console.WriteLine($"Tamanho do buffer: {reply.Buffer.Length} bytes");
                            Console.WriteLine();
                        };


                    }*/
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao tentar pingar {ipAddress}: {ex.Message}");
                }
            }
        }
    }
}
