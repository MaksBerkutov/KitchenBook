using App1.Models;
using App1.Services;
using Syncfusion.XForms.RichTextEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
   
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public BaseViewModel()
        {
            ImageInsertCommand = new Command<object>(Load);
        }
        public event EventHandler<ImageInsertedEventArgs> ImageInsert;
        void Load(object obj)
        {
            ImageInsertedEventArgs imageInsertedEventArgs = (obj as ImageInsertedEventArgs);
            this.GetImage(imageInsertedEventArgs);
        }
        async void GetImage(ImageInsertedEventArgs imageInsertedEventArgs)
        {
            Stream imageStream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            Syncfusion.XForms.RichTextEditor.ImageSource imageSource = new Syncfusion.XForms.RichTextEditor.ImageSource();
            imageSource.ImageStream = imageStream;
            imageSource.SaveOption = ImageSaveOption.Base64;
            imageInsertedEventArgs.ImageSourceCollection.Add(imageSource);
        }
        public ICommand ImageInsertCommand { get; set; }
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
