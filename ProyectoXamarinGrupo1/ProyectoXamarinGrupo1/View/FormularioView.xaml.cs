using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarinGrupo1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FormularioView : ContentPage
	{
		public FormularioView ()
		{
			InitializeComponent ();

            txtArticulo.Focused += TxtArticulo_Focused;

        }

        private void TxtArticulo_Focused(object sender, FocusEventArgs e)
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new PopArticuloView());
        }
    }
}