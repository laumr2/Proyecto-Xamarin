using System;
using FramNet.ClSDataBaseDestok;

namespace AccesoDatos
{
    public class clsTransaccion
    {
        public clsTransaccion(String psTrServidor, String pStrBD, String pStrUsuario, String pStrClave)
        {
            ((ClsSqlServerDAO)ClsFactoriaDAO.metGetConexion()).metServidorBaseDatos = psTrServidor;
            ((ClsSqlServerDAO)ClsFactoriaDAO.metGetConexion()).metBaseDatos = pStrBD;
            ((ClsSqlServerDAO)ClsFactoriaDAO.metGetConexion()).metCodigoUsuario = pStrUsuario;
            ((ClsSqlServerDAO)ClsFactoriaDAO.metGetConexion()).metClaveUsuario = pStrClave;

            //Abrir la conexión con la base de datos
            ClsFactoriaDAO.metGetConexion().metAbrirConexion();
        }
    }
}
