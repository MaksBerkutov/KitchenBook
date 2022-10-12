using App1.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            Title = "Login";
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
