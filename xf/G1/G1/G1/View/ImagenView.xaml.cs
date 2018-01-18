using G1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagenView : ContentPage
    {
        public ImagenView()
        {
            InitializeComponent();

            BindingContext = ImagenViewModel.GetInstance();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ImagenViewModel.SalirImagen();
            });
            return true;
        }
    }
}