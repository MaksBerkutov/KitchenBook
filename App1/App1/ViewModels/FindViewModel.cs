using App1.Models;
using App1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    class FindViewModel:BaseViewModel
    {
        private Item _selectedItem;
        private object _selectedType = null;
        private List<string> typeSelected = new List<string>()
        {
            "Имени",
            "Категории",
            "Типу",
            "Кухни",
            "Рецепту"

        };
        private Dictionary<string, Func<Item,string, bool>> GetFunc = new Dictionary<string, Func<Item, string, bool>>()
        {
            {"Имени",new Func<Item,string, bool>((item,sc)=>{return item.Name.ToLower().Contains(sc.ToLower()); }) },
            {"Категории",new Func<Item,string, bool>((item,sc)=>{return item.Category.ToLower().Contains(sc.ToLower()); }) },
            {"Типу",new Func<Item,string, bool>((item,sc)=>{return item.Type.ToLower().Contains(sc.ToLower()); }) },
            {"Кухни",new Func<Item,string, bool>((item,sc)=>{return item.NameKitchen.ToLower().Contains(sc.ToLower()); }) },
            {"Рецепту",new Func<Item,string, bool>((item,sc)=>{return item.HTMLText.ToLower().Contains(sc.ToLower()); }) }
        };
        public List<string> TypeSelected => typeSelected;
        private string findText = string.Empty;
        public string FindText { get { return findText; } set { SetProperty(ref findText, value); } }
        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command FindItemsCommand { get; }
        public Command<Item> ItemTapped { get; }

        public FindViewModel()
        {
            Title = "Find";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            FindItemsCommand = new Command(OnFind, ValidFind);
            ItemTapped = new Command<Item>(OnItemSelected);
            this.PropertyChanged +=
                (_, __) => FindItemsCommand.ChangeCanExecute();


        }
        private bool ValidFind()=>_selectedType!=null&&(_selectedType as string).Length!=0&&findText!=string.Empty;
        private async void OnFind()
        {
            await ExecuteLoadItemsCommand();
        }
         IEnumerable<Item> Find(Func<Item,string,bool> cheker)
        {
            List<Item> Result = new List<Item>();
            IEnumerable<Item> all =  DataStore.GetItemsAsync(true).Result;
            foreach (var item in all)
            {
                if(cheker?.Invoke(item,findText) == true)
                    Result.Add(item);
            }
            return Result;
              
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                IEnumerable<Item> items = null;
                if (findText == string.Empty)
                    items = await DataStore.GetItemsAsync(true);
                else
                    items = Find(GetFunc[_selectedType as string]);

                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        public object SelectedType
        {
            get => _selectedType;
            set
            {
                SetProperty(ref _selectedType, value);
            }
        }


        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPages));
        }
        private async void OnDeleteItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(DeleteItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
