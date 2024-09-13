namespace AutomacaoLinkDown
{
    public class Alert
    {
        public String Chamado;
        public String Empresa;
        public String IP;

        public Alert(string chamado, string empresa, string ip)
        {
            Chamado = chamado;
            Empresa = empresa;
            IP = ip;
        }

        public Alert(string chamado, string empresa)
        {
            Chamado = chamado;
            Empresa = empresa;
        }

        public override String ToString()
        {
            return $"Chamado: {Chamado}\nEmpresa: {Empresa}\nIP: {IP}";
        }
    }
}
