using FramNet.ClSDataBaseDestok;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Text;

namespace AccesoDatos
{
    public class clsDaoUsuario
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
        public string Usuario { get; set; }

        [DataMember]
        public string Clave { get; set; }

        [DataMember]
        public DateTime Fecha_Act { get; set; }

        [DataMember]
        public int Dias { get; set; }

        [DataMember]
        public string Ult_Ingreso { get; set; }

        [DataMember]
        public double Nivel { get; set; }

        [DataMember]
        public string Clave1 { get; set; }

        [DataMember]
        public string Clave2 { get; set; }

        [DataMember]
        public string Clave3 { get; set; }

        [DataMember]
        public string Clave4 { get; set; }

        [DataMember]
        public int Intervalo_Clave { get; set; }

        [DataMember]
        public string Direccion_Ip { get; set; }

        #endregion Miembros del contrato

        #region Constructor

        public clsDaoUsuario()
        {
            strConexion = "strConexion";
            strSqlConexion[0] = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[strConexion].ConnectionString);
            ClsFactoriaDAO.metTipoBD = "SQLSERVER";
            strConexion = System.Configuration.ConfigurationManager.ConnectionStrings[strConexion].ConnectionString;
        }

        #endregion Constructor

        #region Metodos


        public string validarUsuario(string pStrUsuario, string pStrPassword)
        {
            List<clsDaoUsuario> lstUsuario = new List<clsDaoUsuario>();
            clsDaoUsuario miUsuario = new clsDaoUsuario();
            try
            {
                strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append("from USUARIOS ");
                strSql.Append("where USUARIO = '" + pStrUsuario + "' ");
                strSql.Append("and CLAVE = '" + pStrPassword + "' ");

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
                        miUsuario.Usuario = dtDatos.Rows[i]["USUARIO"].ToString();
                        miUsuario.Clave = dtDatos.Rows[i]["CLAVE"].ToString();
                        miUsuario.Fecha_Act = Convert.ToDateTime(dtDatos.Rows[i]["FECHA_ACT"].ToString());
                        miUsuario.Dias = Convert.ToInt32(dtDatos.Rows[i]["DIAS"].ToString());
                        miUsuario.Ult_Ingreso = dtDatos.Rows[i]["ULT_INGRESO"].ToString();
                        miUsuario.Nivel = Convert.ToDouble(dtDatos.Rows[i]["NIVEL"].ToString());
                        miUsuario.Clave1 = dtDatos.Rows[i]["CLAVE1"].ToString();
                        miUsuario.Clave2 = dtDatos.Rows[i]["CLAVE2"].ToString();
                        miUsuario.Clave3 = dtDatos.Rows[i]["CLAVE3"].ToString();
                        miUsuario.Clave4 = dtDatos.Rows[i]["CLAVE4"].ToString();
                        miUsuario.Intervalo_Clave = Convert.ToInt32(dtDatos.Rows[i]["INTERVALO_CLAVE"].ToString());
                        miUsuario.Direccion_Ip = dtDatos.Rows[i]["DireccionIP"].ToString();

                        lstUsuario.Add(miUsuario);
                    }
                    return JsonConvert.SerializeObject(lstUsuario);
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
