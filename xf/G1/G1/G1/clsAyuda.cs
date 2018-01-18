using G1.Model;
using System;
using System.ServiceModel;

namespace G1
{
    public class clsAyuda
    {
        public static string strArticulo { get; set; }

        public static UsuarioModel Usuario { get; set; }

        static public bool EsNumero(string pStrEsNumero)
        {
            int intVal = 0;
            return int.TryParse(pStrEsNumero, out intVal);
        }

        static public string RellenaCeros(string pStrNumero, int pIntCantCeros)
        {
            string strCadena = "";
            int intLength = pStrNumero.ToString().Length;
            intLength = pIntCantCeros - intLength;
            for (int i = 1; i <= intLength; i++)
            {
                strCadena += 0;
            }
            strCadena += pStrNumero;
            pStrNumero = strCadena;
            return pStrNumero;
        }

        public static Service1Client Cliente()
        {
            try
            {
                EndpointAddress endPoint = new EndpointAddress("http://192.168.43.171/wcfProyecto/Service1.svc");
                BasicHttpBinding binding = CreateBasicHttp();
                return new Service1Client(binding, endPoint);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static BasicHttpBinding CreateBasicHttp()
        {
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Name = "BasicHttpBinding_IService1",
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = long.MaxValue
            };
            TimeSpan timeout = new TimeSpan(0, 0, 120);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            binding.Security.Mode = BasicHttpSecurityMode.None;

            return binding;
        }
    }
}
