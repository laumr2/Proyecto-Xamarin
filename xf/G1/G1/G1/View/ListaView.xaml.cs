using G1.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaView : ContentPage
    {
        OrdenViewModel OrdenViewModel = OrdenViewModel.GetInstance();

        public ListaView()
        {
            InitializeComponent();

            BindingContext = OrdenViewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                OrdenViewModel.SalirMesa();
            });
            return true;
        }
    }
}