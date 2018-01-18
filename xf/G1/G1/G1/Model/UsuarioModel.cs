using Newtonsoft.Json.Linq;
using Realms;
using System;
using System.Linq;

namespace G1.Model
{
    public class UsuarioModel : RealmObject
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Fecha_Act { get; set; }
        public int Dias { get; set; }
        public string Ult_Ingreso { get; set; }
        public double Nivel { get; set; }
        public string Clave1 { get; set; }
        public string Clave2 { get; set; }
        public string Clave3 { get; set; }
        public string Clave4 { get; set; }
        public int Intervalo_Clave { get; set; }
        public string Direccion_Ip { get; set; }

        public static UsuarioModel ObtenerUsuario(string pStrUsuario, string pStrClave)
        {
            string strJson = string.Empty;
            UsuarioModel miUsuario = new UsuarioModel();

            try
            {
                Service1Client cliente = clsAyuda.Cliente();
                strJson = cliente.validarUsuario(pStrUsuario, pStrClave);

                if (!string.IsNullOrEmpty(strJson) || strJson == "[]")
                {
                    JArray jArray = JArray.Parse(strJson);
                    miUsuario.Usuario = jArray[0]["Usuario"].ToString();
                    miUsuario.Clave = jArray[0]["Clave"].ToString();
                    miUsuario.Fecha_Act = jArray[0]["Fecha_Act"].ToString();
                    miUsuario.Dias = Convert.ToInt32(jArray[0]["Dias"].ToString());
                    miUsuario.Nivel = Convert.ToDouble(jArray[0]["Nivel"].ToString());
                    miUsuario.Clave1 = jArray[0]["Clave1"].ToString();
                    miUsuario.Clave2 = jArray[0]["Clave2"].ToString();
                    miUsuario.Clave3 = jArray[0]["Clave3"].ToString();
                    miUsuario.Intervalo_Clave = Convert.ToInt32(jArray[0]["Intervalo_Clave"].ToString());
                    miUsuario.Direccion_Ip = jArray[0]["Direccion_Ip"].ToString();

                    return miUsuario;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void GuardarUsuario(UsuarioModel pUsuario)
        {
            var realm = Realm.GetInstance();
            realm.Write(() =>
            {
                realm.Add(new UsuarioModel
                {
                    Usuario = pUsuario.Usuario,
                    Clave = pUsuario.Clave,
                    Fecha_Act = pUsuario.Fecha_Act,
                    Dias = pUsuario.Dias,
                    Ult_Ingreso = pUsuario.Ult_Ingreso,
                    Nivel = pUsuario.Nivel,
                    Clave1 = pUsuario.Clave1,
                    Clave2 = pUsuario.Clave2,
                    Clave3 = pUsuario.Clave3,
                    Clave4 = pUsuario.Clave4,
                    Intervalo_Clave = pUsuario.Intervalo_Clave,
                    Direccion_Ip = pUsuario.Direccion_Ip
                });
            });
        }

        public static bool RecordarUsuario()
        {
            var realm = Realm.GetInstance();
            var miUsuario = realm.All<UsuarioModel>();
            if (miUsuario.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void OlvidarUsuario()
        {
            var realm = Realm.GetInstance();
            var miUsuario = realm.All<UsuarioModel>();
            if (miUsuario.Count() > 0)
            {
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(miUsuario.First());
                    trans.Commit();
                }
            }
        }
    }
}
