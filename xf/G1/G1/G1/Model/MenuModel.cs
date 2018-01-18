using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1.Model
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Comentario { get; set; }

        public static ObservableCollection<MenuModel> ObtenerListaMenu()
        {
            ObservableCollection<MenuModel> lstMenu = new ObservableCollection<MenuModel>();

            lstMenu.Add(new MenuModel { Id = 1, Descripcion = "Refrescar", Comentario = "Refresca los datos del servidor." });
            lstMenu.Add(new MenuModel { Id = 2, Descripcion = "Buscar", Comentario = "Busca imagenes en el dispositivo." });
            lstMenu.Add(new MenuModel { Id = 3, Descripcion = "Salir", Comentario = "Cierra la sesion del mesero."});


            return lstMenu;
        }
    }
}
