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
	public partial class LoginView : ContentPage
	{
		public LoginView ()
		{
			InitializeComponent ();

            btnIngresar.Clicked += BtnIngresar_Clicked;
		}

        private void BtnIngresar_Clicked(object sender, EventArgs e)
        {
            NavigationPage navigation = new NavigationPage(new PrincipalView());

            App.Current.MainPage = new MasterDetailPage
            {
                Master = new MenuView(),
                Detail = navigation
            };
        }
    }
}