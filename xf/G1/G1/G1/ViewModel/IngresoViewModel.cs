using G1.Model;
using G1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace G1.ViewModel
{
    public class IngresoViewModel 
    {
        #region Singleton

        private static IngresoViewModel instance = null;

        private IngresoViewModel()
        {
            InitClass();
        }

        public static IngresoViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new IngresoViewModel();
            }
            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
            {
                instance = null;
            }
        }

        #endregion Singleton

        #region Methods
        
        private void InitClass()
        {
            if (UsuarioModel.RecordarUsuario())
            {
                AbreMenuPrincipal();
            }
            else
            {
                App.Current.MainPage = new LoginView();
            }
        }
        
        private void AbreMenuPrincipal()
        {
            NavigationPage navigation = new NavigationPage(new PrincipalView());
            App.Current.MainPage = new MasterDetailPage
            {
                Master = new MenuView(),
                Detail = navigation
            };
        }
        
        #endregion Methods
    }
}
