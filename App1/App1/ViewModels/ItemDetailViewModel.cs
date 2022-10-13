using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private object _selctedType;
        public List<string> types = new List<string>() { "DOC", "DOCX" } ;
        public List<string> Types { get => types; }
        public Command SaveCommand { get; }
        private bool ValidSave() => (_selctedType as string) != null&& (_selctedType as string).Length>0;
        public ItemDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidSave);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        public object SelectedType
        {
            get => _selctedType;
            set
            {
                SetProperty(ref _selctedType, value);
            }
        }

        private string BuildHtml() =>
            $"<p align=\"center \">Имя кухни: {name}</p>" +
            $"<p align=\"center \">Категроия: {category}</p>" +
            $"<p align=\"center \">Тип блюда: {type}</p>" +
            $"<p align=\"center \">Имя кухни: {nameKitchen}</p>" 
            +$"{hTMLText}";
        private async void OnSave()
        {
            if (_selctedType == null)
                return;
            string item = _selctedType as string;
            Stream SaveStream = await DependencyService.Get<IPhotoPickerService>().SaveFileAsync(item);
            switch (item)
            {
                case "DOC": Services.Converter.HtmlToWords(BuildHtml(), SaveStream); break;
                case "DOCX": Services.Converter.HtmlToWord(BuildHtml(), SaveStream); break;
            }
           
          

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
