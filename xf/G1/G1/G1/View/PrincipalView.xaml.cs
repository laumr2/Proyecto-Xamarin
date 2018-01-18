using G1.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalView : ContentPage
    {
        public PrincipalView()
        {
            InitializeComponent();

            BindingContext = OrdenViewModel.GetInstance();
        }

        protected override bool OnBackButtonPressed()
        {
            OrdenViewModel OrdenViewModel = OrdenViewModel.GetInstance();

            Device.BeginInvokeOnMainThread(() =>
            {
                OrdenViewModel.SeleccionaMenu(2);
            });
            return true;
        }
    }
}