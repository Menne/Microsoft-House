﻿using MicrosoftHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MicrosoftHouse.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomListPage : ContentPage
    {
        public RoomListPage()
        {
            InitializeComponent();
            BindingContext = new RoomListViewModel();
        }
        
    }
}
