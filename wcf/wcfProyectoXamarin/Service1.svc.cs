using System;
using AccesoDatos;

namespace wcfProyectoXamarin
{
    public class Service1 : IService1
    {
        clsDaoUsuario clsDaoUsuario = new clsDaoUsuario();
        clsDaoOrden clsDaoOrden = new clsDaoOrden();
        clsDaoInventa1 clsDaoInventa1 = new clsDaoInventa1();

        public string validarUsuario(string pStrUsuario, string pStrPassword)
        {
            return clsDaoUsuario.validarUsuario(pStrUsuario, pStrPassword);
        }

        public string obtenerOrden()
        {
            return clsDaoOrden.obtenerOrden();
        }

        public bool guardarOrden(string pStrOrden)
        {
            return clsDaoOrden.guardarOrden(pStrOrden);
        }

        public string obtenerAticulo()
        {
            return clsDaoInventa1.obtenerArticulo();
        }
    }
}
