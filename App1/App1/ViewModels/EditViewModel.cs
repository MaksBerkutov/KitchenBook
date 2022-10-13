using App1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EditViewModel : BaseViewModel
    {
        public Command UpdateCommand { get; }
        private bool ValidateUpdate()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(category)
                && !String.IsNullOrWhiteSpace(nameKitchen)
                && !String.IsNullOrWhiteSpace(hTMLText)
                && !String.IsNullOrWhiteSpace(type);
        }
        private async void OnUpdate()
        {
            Item newItem = new Item()
            {
                Id = this.itemId,
                Name = Name,
                Type = Type,
                Category = Category,
                NameKitchen = NameKitchen,
                HTMLText = HTMLText
            };
            await DataStore.UpdateItemAsync(newItem);
            await Shell.Current.GoToAsync("..");
        }
        public EditViewModel()
        {
            UpdateCommand = new Command(OnUpdate,ValidateUpdate);
            this.PropertyChanged +=
               (_, __) => UpdateCommand.ChangeCanExecute();
        }
      

       
        private string itemId;
        private string name;
        private string category;
        private string type;
        private string nameKitchen;
        private string hTMLText;
        public string Id { get; set; }

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

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Name = item.Name;
                Category = item.Category;
                Type = item.Type;
                NameKitchen = item.NameKitchen;
                HTMLText = item.HTMLText;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
