using Plugin.Media;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using G1.View;

namespace G1.ViewModel
{
    public class ImagenViewModel: INotifyPropertyChanged
    {
        #region Singleton

        private static ImagenViewModel instance = null;

        public ImagenViewModel()
        {
            BuscaImagen();
            BuscaImagenCommand = new Command(BuscaImagen);
        }

        public static ImagenViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new ImagenViewModel();
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

        public ICommand BuscaImagenCommand { get; set; }

        private ImageSource _Imagen;
        public ImageSource Imagen
        {
            get
            {
                return _Imagen;
            }
            set
            {
                _Imagen = value;
                OnPropertyChanged("Imagen");
            }
        }

        #endregion Instances

        #region Metodos

        private async void BuscaImagen()
        {
            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Error de imagen", "No posee permisos de imagen en el dispositivo.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });


                if (file == null)
                    return;

                Imagen = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }
            catch (System.Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error de imagen", "No posee permisos de imagen en el dispositivo.", "OK");
            }            
        }

        public async static void SalirImagen()
        {
            var varRes = await App.Current.MainPage.DisplayAlert("Aviso", "Desea regresar al menu principal?", "Si", "No");
            if (varRes == true)
            {
                DeleteInstance();
                NavigationPage navigation = new NavigationPage(new PrincipalView());
                App.Current.MainPage = new MasterDetailPage
                {
                    Master = new MenuView(),
                    Detail = navigation
                };
            }
        }

        #endregion Metodos

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
