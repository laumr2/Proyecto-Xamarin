﻿using G1.ViewModel;
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
    public partial class PopArticuloView : ContentPage
    {
        public PopArticuloView()
        {
            InitializeComponent();

            BindingContext = OrdenViewModel.GetInstance();
        }
    }
}