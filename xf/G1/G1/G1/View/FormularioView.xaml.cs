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
    public partial class FormularioView : ContentPage
    {
        OrdenViewModel OrdenViewModel = OrdenViewModel.GetInstance();

        public FormularioView()
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

        private void EntryArticulo_Completed(object sender, EventArgs e)
        {
            OrdenViewModel.FiltrarArticulo();
        }
    }
}