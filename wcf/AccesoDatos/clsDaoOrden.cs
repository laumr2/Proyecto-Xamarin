using FramNet.ClSDataBaseDestok;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AccesoDatos
{
    public class clsDaoOrden
    {
        #region Variables

        private String strConexion;                                                                 //Almacena el String de conexion
        private SqlConnection sqlConexion;                                                          //Almacena la conexion
        private SqlCommand sqlCommand;                                                              //Comando de la conexion
        private SqlDataAdapter sqlDataAdapter;                                                      //DataAdapter de la conexion
        private DataTable dtDatos = new DataTable();                                                //Carga los datos del sql
        private SqlConnectionStringBuilder[] strSqlConexion = new SqlConnectionStringBuilder[1];    //Arreglo de conexiones 
        private clsTransaccion istTransaccion;                                                      //Instancia de la tansaccion

        #endregion Variables

        #region Miembros del contrato

        [DataMember]
        public string NroMesa { get; set; }

        [DataMember]
        public DateTime Fecha { get; set; }

        [DataMember]
        public string NroOrden { get; set; }

        [DataMember]
        public string Fcia43 { get; set; }

        [DataMember]
        public DateTime Fcia44 { get; set; }

        [DataMember]
        public int Codigo_Empresa { get; set; }

        [DataMember]
        public string TipoAsiento { get; set; }

        [DataMember]
        public string Lugar { get; set; }

        [DataMember]
        public string NroFactura { get; set; }

        [DataMember]
        public string Anulada { get; set; }

        [DataMember]
        public string Articulo { get; set; }

        [DataMember]
        public string Descrip { get; set; }

        [DataMember]
        public string ImpVentas { get; set; }

        [DataMember]
        public double Cantidad { get; set; }

        [DataMember]
        public string NomCliente { get; set; }

        [DataMember]
        public string EnvCocina { get; set; }

        [DataMember]
        public string NroLinOrden { get; set; }

        [DataMember]
        public string Observacion { get; set; }

        #endregion Miembros del contrato

        #region Constructor

        public clsDaoOrden()
        {
            strConexion = "strConexion";
            strSqlConexion[0] = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[strConexion].ConnectionString);
            ClsFactoriaDAO.metTipoBD = "SQLSERVER";
            strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[strConexion].ConnectionString;
        }

        #endregion Constructor

        #region Metodos

        /// <summary>
        /// Obtiene todas las ordenes, de la mas nueva a la mas antigua
        /// </summary>
        /// <returns>un json con la informacion de las ordenes</returns>
        public string obtenerOrden()
        {
            StringBuilder strSql = new StringBuilder();
            List<clsDaoOrden> lstOrden = new List<clsDaoOrden>();
            try
            {
                strSql = new StringBuilder();
                strSql.Append("Select Max(ordMesa.NroMesa) as NroMesa, ");
                strSql.Append("Max(ordMesa.Fecha) as Fecha, ");
                strSql.Append("Max(ordMesa.NroOrden) as NroOrden, ");
                strSql.Append("Max(detMesa.Fcia43) as Fcia43, ");
                strSql.Append("Max(detMesa.Fcia44) as Fcia44, ");
                strSql.Append("Max(detMesa.Codigo_Empresa) as Codigo_Empresa, ");
                strSql.Append("Max(ordMesa.TipoAsiento) as TipoAsiento, ");
                strSql.Append("Max(ordMesa.Lugar) as Lugar, ");
                strSql.Append("detMesa.NroFactura, ");
                strSql.Append("detMesa.Anulada, ");
                strSql.Append("detMesa.Articulo, ");
                strSql.Append("Max(i1.descrip) As descrip, ");
                strSql.Append("Max(i1.IMPVENTAS) As IMPVENTAS, ");
                strSql.Append("detMesa.EnvCocina, ");
                strSql.Append("detMesa.NroLinOrden, ");
                strSql.Append("detMesa.Observacion, ");
                strSql.Append("Count(detMesa.Cantidad) as Cantidad, ");
                strSql.Append("Max(detMesa.NomCliente) as NomCliente ");
                strSql.Append("From FRtblOrdMesa ordMesa ");
                strSql.Append("join FRtblDetOrdMesa detMesa on(ordMesa.NroOrden=detMesa.NroOrden) ");
                strSql.Append("join inventa1 i1 on(detMesa.Articulo=i1.Articulo) ");
                strSql.Append("Where detMesa.Anulada='N' And detMesa.NroFactura='' ");
                strSql.Append("group by detMesa.Articulo,detMesa.NroFactura,detMesa.Anulada,detMesa.Articulo,");
                strSql.Append("detMesa.EnvCocina,detMesa.NroLinOrden,detMesa.Observacion");
                strSql.Append(" order by NroMesa, NroOrden, NroLinOrden ");

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
                        clsDaoOrden miOrden = new clsDaoOrden();

                        miOrden.NroMesa = dtDatos.Rows[i]["NroMesa"].ToString();
                        miOrden.Fecha = Convert.ToDateTime(dtDatos.Rows[i]["Fecha"].ToString());
                        miOrden.NroOrden = dtDatos.Rows[i]["NroOrden"].ToString();
                        miOrden.Fcia43 = dtDatos.Rows[i]["Fcia43"].ToString();
                        miOrden.Fcia44 = Convert.ToDateTime(dtDatos.Rows[i]["Fcia44"].ToString());
                        miOrden.Codigo_Empresa = Convert.ToInt32(dtDatos.Rows[i]["Codigo_Empresa"].ToString());
                        miOrden.TipoAsiento = dtDatos.Rows[i]["TipoAsiento"].ToString().Trim();
                        miOrden.Lugar = dtDatos.Rows[i]["Lugar"].ToString();
                        miOrden.NroFactura = dtDatos.Rows[i]["NroFactura"].ToString();
                        miOrden.Anulada = dtDatos.Rows[i]["Anulada"].ToString();
                        miOrden.Articulo = dtDatos.Rows[i]["Articulo"].ToString();
                        miOrden.Descrip = dtDatos.Rows[i]["Descrip"].ToString();
                        miOrden.ImpVentas = dtDatos.Rows[i]["ImpVentas"].ToString();
                        miOrden.Cantidad = Convert.ToDouble(dtDatos.Rows[i]["Cantidad"].ToString());
                        miOrden.NomCliente = dtDatos.Rows[i]["NomCliente"].ToString();
                        miOrden.EnvCocina = dtDatos.Rows[i]["EnvCocina"].ToString();
                        miOrden.NroLinOrden = dtDatos.Rows[i]["NroLinOrden"].ToString().Trim();
                        miOrden.Observacion = dtDatos.Rows[i]["Observacion"].ToString();

                        lstOrden.Add(miOrden);
                    }

                    return JsonConvert.SerializeObject(lstOrden);
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

        /// <summary>
        /// Guarda la orden enviada desde el parametro
        /// </summary>
        /// <param name="pStrOrden">Lista de las ordenes en formato JSON para almacenarlas</param>
        /// <returns>Indica si se realizo la transaccion de manera correcta true, de lo contrario false</returns>
        public bool guardarOrden(string pStrOrden)
        {
            StringBuilder strSql = new StringBuilder();
            bool blnCorrecto = false;
            string strNroOrden = "";

            try
            {
                istTransaccion = new clsTransaccion(strSqlConexion[0].DataSource, strSqlConexion[0].InitialCatalog, strSqlConexion[0].UserID, strSqlConexion[0].Password);

                JArray jArray = JArray.Parse(pStrOrden);

                if (jArray.Count() > 0)
                {
                    strNroOrden = obtenerConsecutivo("10", true).Trim();

                    strSql = new StringBuilder();
                    strSql.Append("INSERT INTO FRtblOrdMesa (NroMesa,Fecha,NroOrden,FCIA43,FCIA44,CODIGO_EMPRESA,tipoAsiento,lugar");
                    strSql.Append(")Values(");
                    strSql.Append("'" + jArray[0]["NroMesa"].ToString().Trim() + "',");
                    strSql.Append("'" + Convert.ToDateTime(jArray[0]["Fecha"].ToString()).ToString("yyyyMMdd").Trim() + "',");
                    strSql.Append("'" + strNroOrden + "',");
                    strSql.Append("'" + jArray[0]["Fcia43"].ToString().Trim() + "',");
                    strSql.Append("'" + Convert.ToDateTime(jArray[0]["Fcia44"].ToString()).ToString("yyyyMMdd").Trim() + "',");
                    strSql.Append("" + jArray[0]["Codigo_Empresa"].ToString().Trim() + ",");
                    strSql.Append("'" + jArray[0]["TipoAsiento"].ToString().Trim() + "',");
                    strSql.Append("'" + jArray[0]["Lugar"].ToString().Trim() + "')");

                    blnCorrecto = ClsFactoriaDAO.metGetConexion().metEjecutarSQLTransac(strSql.ToString());
                    if (blnCorrecto)
                    {
                        throw new System.ArgumentException("Error", "Error");
                    }

                    for (int i = 0; i < jArray.Count(); i++)
                    {
                        for (int j = 0; j < Convert.ToInt32(jArray[i]["Cantidad"].ToString()); j++)
                        {
                            strSql = new StringBuilder();
                            strSql.Append("INSERT INTO FRtblDetOrdMesa (NroOrden,NroFactura,Anulada,Articulo,Cantidad,NomCliente,EnvCocina,FCIA43,FCIA44,CODIGO_EMPRESA,NroLinOrden,observacion");
                            strSql.Append(")Values(");
                            strSql.Append("'" + strNroOrden + "',");
                            strSql.Append("'" + jArray[i]["NroFactura"].ToString() + "',");
                            strSql.Append("'" + jArray[i]["Anulada"].ToString() + "',");
                            strSql.Append("'" + jArray[i]["Articulo"].ToString() + "',");
                            strSql.Append("" + 1 + ",");
                            strSql.Append("'" + jArray[i]["NomCliente"].ToString() + "',");
                            strSql.Append("'" + jArray[i]["EnvCocina"].ToString() + "',");
                            strSql.Append("'" + jArray[i]["Fcia43"].ToString() + "',");
                            strSql.Append("'" + Convert.ToDateTime(jArray[i]["Fcia44"].ToString()).ToString("yyyyMMdd").Trim() + "',");
                            strSql.Append("" + jArray[i]["Codigo_Empresa"].ToString() + ",");
                            strSql.Append("'" + jArray[i]["NroLinOrden"].ToString() + "',");
                            strSql.Append("'" + jArray[i]["Observacion"].ToString() + "')");

                            blnCorrecto = ClsFactoriaDAO.metGetConexion().metEjecutarSQLTransac(strSql.ToString());
                            if (blnCorrecto)
                            {
                                throw new System.ArgumentException("Error", "Error");
                            }
                        }
                    }
                }
                ClsFactoriaDAO.metGetConexion().metCommit();
                return true;
            }
            catch (Exception)
            {
                ClsFactoriaDAO.metGetConexion().metRollback();
                return false;
            }
        }

        /// <summary>
        /// Obtiene el numero consecutivo
        /// </summary>
        /// <returns>El numero consecutivo de la tabla orden.</returns>
        public string obtenerConsecutivo(String pStrCodigoUnico, bool pBlnEsTransaccion = true)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                String strRelleno = "0";

                strSql = new StringBuilder();
                strSql.Append("Update tblConsecutivo ");
                strSql.Append("SET NROCONSECUTIVO = ");
                strSql.Append("(SELECT ");
                strSql.Append("Replicate('" + strRelleno + "', (LEN(NROCONSECUTIVO) - LEN(CONVERT(int, NROCONSECUTIVO) + 1))) ");
                strSql.Append("+ CONVERT(CHAR,CONVERT(int, NROCONSECUTIVO) + 1) ");
                strSql.Append("From tblConsecutivo ");
                strSql.Append("WHERE  (CODIGO = '" + pStrCodigoUnico + "')) ");
                strSql.Append("WHERE  (CODIGO = '" + pStrCodigoUnico + "') ");

                bool blnCorrecto = ClsFactoriaDAO.metGetConexion().metEjecutarSQLTransac(strSql.ToString());
                if (blnCorrecto)
                {
                    throw new System.ArgumentException("Error", "Error");
                }

                strSql = new StringBuilder();
                strSql.Append("Select NroConsecutivo From tblConsecutivo Where Codigo ='" + pStrCodigoUnico + "'");

                DataRow Row = ClsFactoriaDAO.metGetConexion().metEjeSQLDataRowTrans(strSql);

                if (Row != null)
                {
                    return Row[0].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                ClsFactoriaDAO.metGetConexion().metRollback();
                return "";
            }
        }

        #endregion Metodos
    }
}
