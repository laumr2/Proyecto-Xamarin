using G1.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : ContentPage
    {
        public MenuView()
        {
            InitializeComponent();

            BindingContext = OrdenViewModel.GetInstance();
        }
    }
}