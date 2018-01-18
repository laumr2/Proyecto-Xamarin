using System;
using G1.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace G1.ViewModel
{
    public class Inventa1ViewModel : INotifyPropertyChanged
    {
        #region Variables

        Inventa1Model miArticuloSeleccionado = new Inventa1Model();

        #endregion Variables

        #region Singleton

        private static Inventa1ViewModel instance = null;

        private Inventa1ViewModel()
        {
            InitClass();
            InitCommands();
        }

        public static Inventa1ViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new Inventa1ViewModel();
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

        public ICommand DetalleArticuloSeleccionadoCommand { get; set; }
        public ICommand ArticuloSeleccionadoCommand { get; set; }

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
                //FiltrarPopArticulo(_PopArticulo);
                OnPropertyChanged("PopArticulo");
            }
        }

        #endregion Instances

        #region Metodos

        private void InitClass()
        {
        }

        private void InitCommands()
        {
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
                PopArticulo = "";
                miArticuloSeleccionado = new Inventa1Model();
                clsAyuda.strArticulo = miArticuloSeleccionado.CodArticulo;
                await App.Current.MainPage.Navigation.PopModalAsync();                
                DeleteInstance();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe de seleccionar un articulo.", "Ok");
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
