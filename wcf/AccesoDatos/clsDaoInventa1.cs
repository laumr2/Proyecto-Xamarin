using FramNet.ClSDataBaseDestok;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class clsDaoInventa1
    {
        #region Variables

        private String strConexion;                                                                 //Almacena el String de conexion
        private SqlConnection sqlConexion;                                                          //Almacena la conexion
        private SqlCommand sqlCommand;                                                              //Comando de la conexion
        private SqlDataAdapter sqlDataAdapter;                                                      //DataAdapter de la conexion
        private DataTable dtDatos = new DataTable();                                                //Carga los datos del sql
        private StringBuilder strSql;                                                               //Constructor de string de conexion
        private SqlConnectionStringBuilder[] strSqlConexion = new SqlConnectionStringBuilder[1];    //Arreglo de conexiones 

        #endregion Variables

        #region Miembros del contrato

        [DataMember]
        public string CodArticulo { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string ImpVentas { get; set; }

        #endregion Miembros del contrato

        #region Constructor

        public clsDaoInventa1()
        {
            strConexion = "strConexion";
            strSqlConexion[0] = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[strConexion].ConnectionString);
            ClsFactoriaDAO.metTipoBD = "SQLSERVER";
            strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[strConexion].ConnectionString;
        }

        #endregion Constructor

        #region Metodos

        /// <summary>
        /// Obtiene todos los artículos que no son materia prima
        /// </summary>
        /// <returns>un json con la informacion de los artículos</returns>
        public string obtenerArticulo()
        {
            List<clsDaoInventa1> lstArticulos = new List<clsDaoInventa1>();
            clsDaoInventa1 Articulo = new clsDaoInventa1();
            try
            {
                strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append("from Inventa1 Inventa1 ");
                strSql.Append("where Inventa1.MatPrima = 'N' ");
                strSql.Append("order by Inventa1.Articulo ");

                using (sqlConexion = new SqlConnection(strConexion))
                {
                    if (sqlConexion.State == ConnectionState.Closed)
                    {
                        sqlConexion.Open();
                    }
                    sqlCommand = new SqlCommand(" " + strSql + " ", sqlConexion);
                    sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    dtDatos.Clear();
                    sqlDataAdapter.Fill(dtDatos);
                    sqlConexion.Close();
                }

                if (dtDatos != null)
                {
                    for (int i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        Articulo = new clsDaoInventa1();

                        Articulo.CodArticulo = dtDatos.Rows[i]["Articulo"].ToString();
                        Articulo.Descripcion = dtDatos.Rows[i]["Descrip"].ToString();
                        Articulo.ImpVentas = dtDatos.Rows[i]["ImpVentas"].ToString();

                        lstArticulos.Add(Articulo);
                    }

                    return JsonConvert.SerializeObject(lstArticulos);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        #endregion Metodos

    }
}
