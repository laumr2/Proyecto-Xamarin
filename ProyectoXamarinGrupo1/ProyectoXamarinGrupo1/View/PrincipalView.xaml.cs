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
	public partial class PrincipalView : ContentPage
	{
		public PrincipalView ()
		{
			InitializeComponent ();

            btn01.Clicked += Btn01_Clicked;
		}

        private void Btn01_Clicked(object sender, EventArgs e)
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TabView());
        }
    }
}