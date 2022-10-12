using App1.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
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
