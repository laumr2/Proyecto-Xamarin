using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1.Model
{
    public class OrdenModel
    {
        public string NroMesa { get; set; }
        public DateTime Fecha { get; set; }
        public string NroOrden { get; set; }
        public string Fcia43 { get; set; }
        public DateTime Fcia44 { get; set; }
        public int Codigo_Empresa { get; set; }
        public string TipoAsiento { get; set; }
        public string Lugar { get; set; }
        public string NroFactura { get; set; }
        public string Anulada { get; set; }
        public string Articulo { get; set; }
        public string Descrip { get; set; }
        public string ImpVentas { get; set; }
        public double Cantidad { get; set; }
        public string NomCliente { get; set; }
        public string EnvCocina { get; set; }
        public string NroLinOrden { get; set; }
        public string Observacion { get; set; }
        public string CantidadArticulo { get; set; }


        public static List<OrdenModel> ObtenerOrdenes()
        {
            string strJson = string.Empty;
            List<OrdenModel> lstOrden = new List<OrdenModel>();
            OrdenModel miOrden = new OrdenModel();

            try
            {
                Service1Client cliente = clsAyuda.Cliente();
                strJson = cliente.obtenerOrden();

                if (!string.IsNullOrEmpty(strJson) || strJson == "[]")
                {
                    JArray jArray = JArray.Parse(strJson);
                    for (int i = 0; i < jArray.Count; i++)
                    {
                        miOrden = new OrdenModel();

                        miOrden.NroMesa = jArray[i]["NroMesa"].ToString();
                        miOrden.Fecha = Convert.ToDateTime(jArray[i]["Fecha"].ToString());
                        miOrden.NroOrden = jArray[i]["NroOrden"].ToString();
                        miOrden.Fcia43 = jArray[i]["Fcia43"].ToString();
                        miOrden.Fcia44 = Convert.ToDateTime(jArray[i]["Fcia44"].ToString());
                        miOrden.Codigo_Empresa = Convert.ToInt32(jArray[i]["Codigo_Empresa"].ToString());
                        miOrden.TipoAsiento = jArray[i]["TipoAsiento"].ToString();
                        miOrden.Lugar = jArray[i]["Lugar"].ToString();
                        miOrden.NroFactura = jArray[i]["NroFactura"].ToString();
                        miOrden.Anulada = jArray[i]["Anulada"].ToString();
                        miOrden.Articulo = jArray[i]["Articulo"].ToString();
                        miOrden.Descrip = jArray[i]["Descrip"].ToString();
                        miOrden.Cantidad = Convert.ToDouble(jArray[i]["Cantidad"].ToString());
                        miOrden.CantidadArticulo = jArray[i]["Cantidad"].ToString() + " " + jArray[i]["Articulo"].ToString();
                        miOrden.NomCliente = jArray[i]["NomCliente"].ToString();
                        miOrden.EnvCocina = jArray[i]["EnvCocina"].ToString();
                        miOrden.NroLinOrden = jArray[i]["NroLinOrden"].ToString();
                        miOrden.Observacion = jArray[i]["Observacion"].ToString();
                        miOrden.ImpVentas = jArray[i]["ImpVentas"].ToString();

                        lstOrden.Add(miOrden);
                    }

                    return lstOrden;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool GuardarOrden(List<OrdenModel> lstOrden)
        {
            try
            {
                Service1Client cliente = clsAyuda.Cliente();
                return cliente.guardarOrden(JsonConvert.SerializeObject(lstOrden));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
