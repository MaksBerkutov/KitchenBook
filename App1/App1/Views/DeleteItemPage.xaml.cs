using App1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    public partial class DeleteItemPage : ContentPage
    {
        DeleteItemViewModel _viewModel;
        public DeleteItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new DeleteItemViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}