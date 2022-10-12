using App1.UWP;
using App1.Services;
using System.IO;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Xamarin.Forms;
using Windows.Storage.Streams;
using System.Diagnostics;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace App1.UWP
{
    
    public class PhotoPickerService : IPhotoPickerService
    {
        
        public async Task<Stream> GetImageStreamAsync()
        {
            // Create and initialize the FileOpenPicker
           FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
            };

            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile storageFile = storageFile = await openPicker.PickSingleFileAsync();
            // Get a file and return a Stream
           

            if (storageFile == null)
            {
                return null;
            }

            IRandomAccessStreamWithContentType raStream = await storageFile.OpenReadAsync();
            //(RTE_Localization.App.Current.BindingContext as ViewModel).Stream1 = raStream.AsStream();
            return raStream.AsStream();
        }
    }
}
