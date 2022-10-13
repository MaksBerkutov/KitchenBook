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
    class DeleteItemViewModel: BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command CancelCommand { get; }
        public DeleteItemViewModel()
        {
            Title = "Delete";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            CancelCommand = new Command(OnCancel);
            ItemTapped = new Command<Item>(OnItemSelected);

           
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
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
                if(Items.Count==0) await Shell.Current.GoToAsync(".."); 
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
       
        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;
            await DataStore.DeleteItemAsync(item.Id);
           await Shell.Current.GoToAsync("..");
        }
    }
}
