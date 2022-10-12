using App1.Models;
using Syncfusion.XForms.RichTextEditor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class NewItemViewModels : BaseViewModel
    {
        private string name;
        private string category;
        private string type;
        private string nameKitchen;
        private string hTMLText;
        public NewItemViewModels()
        {
           SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
      
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(category)
                && !String.IsNullOrWhiteSpace(nameKitchen)
                && !String.IsNullOrWhiteSpace(hTMLText)
                && !String.IsNullOrWhiteSpace(type);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }
        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }
        public string NameKitchen
        {
            get => nameKitchen;
            set => SetProperty(ref nameKitchen, value);
        }
        public string HTMLText
        {
            get => hTMLText;
            set => SetProperty(ref hTMLText, value);
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Type = Type,
                Category = Category,
                NameKitchen = NameKitchen,
                HTMLText = HTMLText
            };

            await DataStore.AddItemAsync(newItem);

            await Shell.Current.GoToAsync("..");
        }
    }
}
