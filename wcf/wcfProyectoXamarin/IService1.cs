using System.ServiceModel;

namespace wcfProyectoXamarin
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string validarUsuario(string pStrUsuario, string pStrPassword);

        [OperationContract]
        string obtenerOrden();

        [OperationContract]
        bool guardarOrden(string pStrOrden);

        [OperationContract]
        string obtenerAticulo();
    }
}
