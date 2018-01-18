using G1.Model;
using G1.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace G1.ViewModel
{
    public class OrdenViewModel : INotifyPropertyChanged
    {
        #region Variables

        private string strNroLinea = string.Empty;
        private Inventa1Model miArticuloSeleccionado = new Inventa1Model();

        #endregion Variables

        #region Singleton

        private static OrdenViewModel instance = null;

        private OrdenViewModel()
        {
            InitClass();
            InitCommands();
        }

        public static OrdenViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new OrdenViewModel();
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

        public ICommand SeleccionaMenuCommand { get; set; }
        public ICommand MesaCommand { get; set; }
        public ICommand AgregaLineaCommand { get; set; }
        public ICommand OrdenSeleccionadaCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand SalirCommand { get; set; }
        public ICommand DetalleSeleccionadoCommand { get; set; }
        public ICommand DetalleArticuloSeleccionadoCommand { get; set; }
        public ICommand ArticuloSeleccionadoCommand { get; set; }

        private ObservableCollection<MenuModel> _lstMenu = new ObservableCollection<MenuModel>();
        public ObservableCollection<MenuModel> lstMenu
        {
            get
            {
                return _lstMenu;
            }
            set
            {
                _lstMenu = value;
                OnPropertyChanged("lstMenu");
            }
        }

        private List<OrdenModel> lstOrdenOriginal = new List<OrdenModel>();
        private ObservableCollection<OrdenModel> _lstOrden = new ObservableCollection<OrdenModel>();
        public ObservableCollection<OrdenModel> lstOrden
        {
            get
            {
                return _lstOrden;
            }
            set
            {
                _lstOrden = value;
                OnPropertyChanged("lstOrden");
            }
        }

        private List<OrdenModel> lstDetalleOriginal = new List<OrdenModel>();
        private ObservableCollection<OrdenModel> _lstDetalle = new ObservableCollection<OrdenModel>();
        public ObservableCollection<OrdenModel> lstDetalle
        {
            get
            {
                return _lstDetalle;
            }
            set
            {
                _lstDetalle = value;
                OnPropertyChanged("lstDetalle");
            }
        }

        public List<Inventa1Model> lstOriginalArticulo = new List<Inventa1Model>();
        private ObservableCollection<Inventa1Model> _lstArticulo = new ObservableCollection<Inventa1Model>();
        public ObservableCollection<Inventa1Model> lstArticulo
        {
            get
            {
                return _lstArticulo;
            }
            set
            {
                _lstArticulo = value;
                OnPropertyChanged("lstArticulo");
            }
        }

        private string _Fecha = string.Empty;
        public string Fecha
        {
            get
            {
                return _Fecha;
            }
            set
            {
                _Fecha = value;
                OnPropertyChanged("Fecha");
            }
        }

        private string _NombreCliente = string.Empty;
        public string NombreCliente
        {
            get
            {
                return _NombreCliente;
            }
            set
            {
                _NombreCliente = value.ToUpper();
                OnPropertyChanged("NombreCliente");
            }
        }

        private string _Cantidad = string.Empty;
        public string Cantidad
        {
            get
            {
                return _Cantidad;
            }
            set
            {
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        private string _Articulo = string.Empty;
        public string Articulo
        {
            get
            {
                return _Articulo;
            }
            set
            {
                _Articulo = value.ToUpper();
                OnPropertyChanged("Articulo");
            }
        }

        private string _CantidadArticulo = string.Empty;
        public string CantidadArticulo
        {
            get
            {
                return _CantidadArticulo;
            }
            set
            {
                _CantidadArticulo = value;
                OnPropertyChanged("CantidadArticulo");
            }
        }

        private string _Descripcion = string.Empty;
        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }
            set
            {
                _Descripcion = value.ToUpper();
                OnPropertyChanged("Descripcion");
            }
        }

        private string _Gravado = string.Empty;
        public string Gravado
        {
            get
            {
                return _Gravado;
            }
            set
            {
                _Gravado = value;
                OnPropertyChanged("Gravado");
            }
        }

        private string _Observacion = string.Empty;
        public string Observacion
        {
            get
            {
                return _Observacion;
            }
            set
            {
                _Observacion = value.ToUpper();
                OnPropertyChanged("Observacion");
            }
        }

        private string _Mesa = string.Empty;
        public string Mesa
        {
            get
            {
                return _Mesa;
            }
            set
            {
                _Mesa = value;
                OnPropertyChanged("Mesa");
            }
        }

        private string _PopArticulo = string.Empty;
        public string PopArticulo
        {
            get
            {
                return _PopArticulo;
            }
            set
            {
                _PopArticulo = value.ToUpper();
                FiltrarPopArticulo(_PopArticulo);
                OnPropertyChanged("PopArticulo");
            }
        }

        #endregion Instances

        #region Methods

        private async Task InitClass()
        {
            lstMenu = MenuModel.ObtenerListaMenu();
            lstOriginalArticulo = Inventa1Model.ObtenerOrdenes();
            lstOrdenOriginal = OrdenModel.ObtenerOrdenes();
        }

        private void InitCommands()
        {
            GuardarCommand = new Command(GuardarOrden);
            SalirCommand = new Command(SalirMesa);
            AgregaLineaCommand = new Command(AgregaLinea);
            MesaCommand = new Command<string>(AbrirOrden);
            DetalleSeleccionadoCommand = new Command<string>(DetalleSeleccionado);
            SeleccionaMenuCommand = new Command<int>(SeleccionaMenu);
            OrdenSeleccionadaCommand = new Command<string>(OrdenSeleccionada);
            CancelarCommand = new Command<string>(CancelarOrden);
            ArticuloSeleccionadoCommand = new Command(ArticuloSeleccionado);
            DetalleArticuloSeleccionadoCommand = new Command<string>(DetalleArticuloSeleccionado);
        }

        private void AbrirOrden(string pStrNroMesa)
        {
            MostrarDatosMesa(pStrNroMesa);
            App.Current.MainPage = new TabView();
        }

        private void MostrarDatosMesa(string pStrNroMesa)
        {
            string strArticulo = "";
            double dblCant = 0;
            OrdenModel miOrden = new OrdenModel();

            Mesa = pStrNroMesa;
            Fecha = DateTime.Now.ToString();
            NombreCliente = "SIN NOMBRE";
            lstOrden.Clear();

            if (lstOrdenOriginal != null)
            {
                foreach (var item in lstOrdenOriginal)
                {
                    if (item.NroMesa.Trim() == Mesa && item.Articulo != strArticulo)
                    {
                        miOrden = new OrdenModel();
                        dblCant = 0;
                        strArticulo = item.Articulo;
                        foreach (var item2 in lstOrdenOriginal)
                        {
                            if (item2.NroMesa == item.NroMesa && item2.Articulo == item.Articulo)
                            {
                                dblCant += item2.Cantidad;
                            }
                        }
                        miOrden.NroMesa = item.NroMesa;
                        miOrden.Fecha = item.Fecha;
                        miOrden.Articulo = item.Articulo;
                        miOrden.Descrip = item.Descrip;
                        miOrden.Cantidad = dblCant;
                        miOrden.NomCliente = item.NomCliente;
                        miOrden.Observacion = item.Observacion;
                        miOrden.CantidadArticulo = dblCant.ToString() + " " + item.Articulo;
                        lstOrden.Add(miOrden);
                    }
                }
            }
        }

        public async void SeleccionaMenu(int pIntId)
        {
            switch (pIntId)
            {
                case 1:
                    await InitClass();
                    break;
                case 2:
                    App.Current.MainPage = new NavigationPage(new ImagenView());
                    break;
                case 3:
                    var varRes = await App.Current.MainPage.DisplayAlert("Aviso", "Desea cerrar la sesion?", "Si", "No");
                    if (varRes == true)
                    {
                        UsuarioModel.OlvidarUsuario();
                        App.Current.MainPage = new NavigationPage(new LoginView());
                    }
                    break;
            }
        }

        public void FiltrarArticulo()
        {
            Inventa1Model miArticulo = new Inventa1Model();

            if (clsAyuda.EsNumero(Articulo))
            {
                Articulo = clsAyuda.RellenaCeros(Articulo, 8);
                foreach (var item in lstOriginalArticulo)
                {
                    if (item.CodArticulo == Articulo)
                    {
                        Descripcion = item.Descripcion;
                        Gravado = item.ImpVentas.Replace("S", " * ").Replace("N", "   "); ;
                    }
                }
                if (string.IsNullOrEmpty(Descripcion))
                {
                    App.Current.MainPage.DisplayAlert("Error", "No se encontro el codigo digitado.", "Ok");
                }
            }
            else
            {
                PopArticulo = Articulo;
                App.Current.MainPage.Navigation.PushModalAsync(new PopArticuloView());
            }
        }

        private async void AgregaLinea()
        {
            if (string.IsNullOrEmpty(Cantidad))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe de digitar una cantidad.", "Ok");
                return;
            }

            if (Convert.ToDouble(Cantidad) == 0 && string.IsNullOrEmpty(strNroLinea))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe de digitar una cantidad para la nueva linea.", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(Descripcion))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe de ingresar un articulo.", "Ok");
                return;
            }

            OrdenModel miOrden = new OrdenModel();

            if (Convert.ToDouble(Cantidad) > 0)
            {
                if (!string.IsNullOrEmpty(strNroLinea))
                {
                    miOrden = lstDetalle.Where(x => x.NroLinOrden == strNroLinea).FirstOrDefault();
                    lstDetalle.Remove(miOrden);
                }

                miOrden.NroMesa = Mesa;
                miOrden.Fecha = Convert.ToDateTime(Fecha);
                miOrden.Fcia43 = clsAyuda.Usuario.Usuario;
                miOrden.Fcia44 = DateTime.Now;
                miOrden.Codigo_Empresa = 1;
                miOrden.TipoAsiento = "2";
                miOrden.Lugar = "2";
                miOrden.NroFactura = "";
                miOrden.Anulada = "N";
                miOrden.EnvCocina = "N";
                miOrden.NomCliente = NombreCliente;
                miOrden.Cantidad = Convert.ToDouble(Cantidad);
                miOrden.CantidadArticulo = Cantidad + " " + clsAyuda.RellenaCeros(Articulo, 8);
                miOrden.Articulo = clsAyuda.RellenaCeros(Articulo, 8);
                miOrden.Descrip = Descripcion;
                miOrden.Observacion = Observacion;

                if (string.IsNullOrEmpty(strNroLinea))
                {
                    miOrden.NroLinOrden = (lstDetalle.Count + 1).ToString();
                }

                lstDetalle.Add(miOrden);
                CancelarOrden();
            }
            else if (true)
            {
                miOrden = lstDetalle.Where(x => x.NroLinOrden == strNroLinea).FirstOrDefault();
                lstDetalle.Remove(miOrden);
                CancelarOrden();
            }
        }

        private void OrdenSeleccionada(string pStrNroLinea)
        {
            OrdenModel miOrden = lstDetalle.Where(x => x.NroMesa == Mesa && x.NroLinOrden == pStrNroLinea).FirstOrDefault();

            Mesa = miOrden.NroMesa;
            Fecha = miOrden.Fecha.ToString();
            NombreCliente = miOrden.NomCliente;
            Cantidad = miOrden.Cantidad.ToString();
            CantidadArticulo = miOrden.Cantidad.ToString() + " " + miOrden.Articulo;
            Cantidad = miOrden.Cantidad.ToString();
            Articulo = miOrden.Articulo;
            Descripcion = miOrden.Descrip;
            Observacion = miOrden.Observacion;
            strNroLinea = miOrden.NroLinOrden;
        }

        public async void SalirMesa()
        {
            var varRes = await App.Current.MainPage.DisplayAlert("Aviso", "Desea regresar al menu principal?", "Si", "No");
            if (varRes == true)
            {
                NavigationPage navigation = new NavigationPage(new PrincipalView());
                App.Current.MainPage = new MasterDetailPage
                {
                    Master = new MenuView(),
                    Detail = navigation
                };
            }
        }

        private async void GuardarOrden()
        {
            if (OrdenModel.GuardarOrden(lstDetalle.ToList()))
            {
                await App.Current.MainPage.DisplayAlert("Orden almacenada", "El registro de la orden fue almacenado correctamente", "Ok");
                InitClass();
                MostrarDatosMesa(Mesa);
                CancelarOrden("true");

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error al almacenar", "El registro de la orden posee un error.", "Ok");
            }

        }

        private void CancelarOrden(string pStrLimpiaGrid = "")
        {
            Fecha = DateTime.Now.ToString();
            Cantidad = string.Empty;
            Articulo = string.Empty;
            Gravado = "   ";
            Descripcion = string.Empty;
            Observacion = string.Empty;
            strNroLinea = string.Empty;
            if (pStrLimpiaGrid == "true")
            {
                lstDetalle.Clear();
            }
        }

        private void DetalleSeleccionado(string pStrArticulo)
        {
            OrdenModel miOrden = lstOrden.Where(x => x.Articulo == pStrArticulo).FirstOrDefault();

            App.Current.MainPage.DisplayAlert("Detalle de orden",
                                                    "Numero de mesa: " + miOrden.NroMesa + Environment.NewLine +
                                                    "Fecha: " + miOrden.Fecha.ToString("dd/MM/yyyy") + Environment.NewLine +
                                                    "Cliente: " + miOrden.NomCliente + Environment.NewLine +
                                                    "Cantidad: " + miOrden.Cantidad + Environment.NewLine +
                                                    "Codigo del articulo: " + miOrden.Articulo + Environment.NewLine +
                                                    "Descripcion del articulo: " + Environment.NewLine + miOrden.Descrip,
                                                    "Ok");
        }

        private async void DetalleArticuloSeleccionado(string pStrArticulo)
        {
            miArticuloSeleccionado = new Inventa1Model();
            miArticuloSeleccionado = lstOriginalArticulo.Where(x => x.CodArticulo == pStrArticulo).FirstOrDefault();

            await App.Current.MainPage.DisplayAlert("Detalle de articulo",
                                                    "Articulo: " + miArticuloSeleccionado.CodArticulo + Environment.NewLine +
                                                    "Gravado: " + miArticuloSeleccionado.ImpVentas.Replace("S", "Si").Replace("N", "No") + Environment.NewLine +
                                                    "Descripcion del articulo: " + Environment.NewLine + miArticuloSeleccionado.Descripcion,
                                                    "Ok");
        }

        private void FiltrarPopArticulo(string pStrArticulo)
        {
            lstArticulo.Clear();
            if (!string.IsNullOrEmpty(pStrArticulo))
            {
                foreach (var item in lstOriginalArticulo.Where(x => x.Descripcion.Contains(pStrArticulo.ToUpper())))
                {
                    lstArticulo.Add(item);
                }
            }
        }

        private async void ArticuloSeleccionado()
        {
            if (miArticuloSeleccionado.CodArticulo != null)
            {
                Articulo = miArticuloSeleccionado.CodArticulo;
                FiltrarArticulo();
                await App.Current.MainPage.Navigation.PopModalAsync();
                PopArticulo = "";
                miArticuloSeleccionado = new Inventa1Model();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe de seleccionar un articulo.", "Ok");
            }
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