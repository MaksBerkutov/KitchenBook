using App1.Models;
using App1.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ItemsViewModel : BaseViewModel

    {
        private Item _selectedItem;
        private string editT = "EditMode: OFF";
        public string EDIT { get => editT; set => SetProperty(ref editT, value); }
        private bool edit = false;
        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command DeleteItemCommand { get; }
        public Command EditModeCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            DeleteItemCommand = new Command(OnDeleteItem);
            EditModeCommand = new Command(OnEdit);
        }
        private async void OnEdit(object obj)
        {
            edit = !edit;
            if (edit)
                EDIT = "EditMode: ON";
            else
                EDIT = "EditMode: OFF";
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
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
            if(edit)
                await Shell.Current.GoToAsync($"{nameof(EditPage)}?{nameof(EditViewModel.ItemId)}={item.Id}");
            else
                await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");

        }
    }
}