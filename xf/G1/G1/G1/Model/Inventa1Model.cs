using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1.Model
{
    public class Inventa1Model
    {
        public string CodArticulo { get; set; }
        public string Descripcion { get; set; }
        public string ImpVentas { get; set; }

        public static List<Inventa1Model> ObtenerOrdenes()
        {
            string strJson = string.Empty;
            List<Inventa1Model> lstArticulo = new List<Inventa1Model>();
            Inventa1Model miArticulo = new Inventa1Model();

            try
            {
                Service1Client cliente = clsAyuda.Cliente();
                strJson = cliente.obtenerAticulo();

                if (!string.IsNullOrEmpty(strJson) || strJson == "[]")
                {
                    JArray jArray = JArray.Parse(strJson);
                    for (int i = 0; i < jArray.Count; i++)
                    {
                        miArticulo = new Inventa1Model();

                        miArticulo.CodArticulo = jArray[i]["CodArticulo"].ToString();
                        miArticulo.Descripcion = jArray[i]["Descripcion"].ToString();
                        miArticulo.ImpVentas = jArray[i]["ImpVentas"].ToString();

                        lstArticulo.Add(miArticulo);
                    }

                    return lstArticulo;
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
    }
}
