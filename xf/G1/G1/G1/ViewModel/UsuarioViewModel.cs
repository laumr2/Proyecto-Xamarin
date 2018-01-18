using G1.Model;
using G1.View;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace G1.ViewModel
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        #region Singleton

        private static UsuarioViewModel instance = null;

        private UsuarioViewModel()
        {
            InitCommands();
        }

        public static UsuarioViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new UsuarioViewModel();
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

        #region Instances

        //Instancia a la clase Modelo
        //UsuarioModel UsuarioModel = new UsuarioModel();

        //Instancias de los comandos
        public ICommand IngresarCommand { get; set; }

        //Istancias de los campos
        private string _Usuario = string.Empty;
        public string Usuario
        {
            get
            {
                return _Usuario;
            }
            set
            {
                _Usuario = value;
                OnPropertyChanged("Usuario");
            }
        }

        private string _Clave = string.Empty;
        public string Clave
        {
            get
            {
                return _Clave;
            }
            set
            {
                _Clave = value;
                OnPropertyChanged("Clave");
            }
        }

        public bool _Check = false;
        public bool Check
        {
            get
            {
                return _Check;
            }
            set
            {
                _Check = value;
                OnPropertyChanged("Check");
            }
        }

        #endregion Instances

        #region Methods

        private void InitCommands()
        {
            IngresarCommand = new Command(ValidarUsuario);
        }
        
        private void ValidarUsuario()
        {
            UsuarioModel miUsuario = UsuarioModel.ObtenerUsuario(Usuario, Clave);
            if (miUsuario != null)
            {
                if (Check)
                {
                    GuardarUsuario(miUsuario);
                }

                clsAyuda.Usuario = miUsuario;
                Usuario = string.Empty;
                Clave = string.Empty;
                AbreMenuPrincipal();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error","Usuario o Clave incorrecta.","Ok");
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

        private void GuardarUsuario(UsuarioModel pUsuario)
        {
            UsuarioModel.GuardarUsuario(pUsuario);
        }

        #endregion Methods

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // if there is any subscribers 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Implementation
    }
}
