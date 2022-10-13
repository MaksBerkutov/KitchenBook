using App1.ViewModels;
using App1.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App1
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(EditPage), typeof(EditPage));
            Routing.RegisterRoute(nameof(NewItemPages), typeof(NewItemPages));
            Routing.RegisterRoute(nameof(DeleteItemPage), typeof(DeleteItemPage));
            Routing.RegisterRoute(nameof(FindPage), typeof(FindPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
        }
    }
}
