using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace AutomacaoLinkDown
{
    public class PingHelper
    {
        public void PingIP(string ip)
        {
            IpAddress ipAddress = new IpAddress(ip, 4);

            using (Ping ping = new Ping())
            {
                try
                {
                    List<string> teste = new List<string>();
                    for (int i = 0; i < ipAddress.nTeste; i++)
                    {
                        PingReply reply = ping.Send(ipAddress.IpServe);

                        if (reply.Status == IPStatus.Success)
                        {
                            ipAddress.Recebidos++;
                            ipAddress.TtlData += reply.Options.Ttl;
                        }
                        else
                        {
                            ipAddress.Perdidos++;
                        }

                        ipAddress.Enviados++;
                        ipAddress.Resposta += reply.RoundtripTime;
                        ipAddress.QntBytes += reply.Buffer.Length;
                        ipAddress.ipStatus = reply.Status.ToString();
                    }

                    ipAddress.calValue();
                    teste.Add("***Serve***\n" + ipAddress.stringValue(ipAddress.IpServe));
                    Console.WriteLine(teste[0]);

                    for (int i = 0; i < ipAddress.nTeste; i++)
                    {
                        PingReply reply = ping.Send(ipAddress.IpLink);

                        if (reply.Status == IPStatus.Success)
                        {
                            ipAddress.Recebidos++;
                            ipAddress.TtlData += reply.Options.Ttl;
                        }
                        else
                        {
                            ipAddress.Perdidos++;
                        }

                        ipAddress.Enviados++;
                        ipAddress.Resposta += reply.RoundtripTime;
                        ipAddress.QntBytes += reply.Buffer.Length;
                        ipAddress.ipStatus = reply.Status.ToString();
                    }

                    ipAddress.calValue();
                    teste.Add("***Link***\n" + ipAddress.stringValue(ipAddress.IpLink));
                    Console.WriteLine(teste[1]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao tentar pingar {ipAddress.IpServe}: {ex.Message}");
                }
            }
        }
    }
}