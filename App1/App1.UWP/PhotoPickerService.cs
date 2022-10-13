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
using System.Collections.Generic;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace App1.UWP
{
    
    public class PhotoPickerService : IPhotoPickerService
    {
        
        public async Task<Stream> GetImageStreamAsync()
        {
           FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
            };

            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile storageFile = storageFile = await openPicker.PickSingleFileAsync();
           

            if (storageFile == null)
            {
                return null;
            }

            IRandomAccessStreamWithContentType raStream = await storageFile.OpenReadAsync();
            return raStream.AsStream();
        }
        public async Task<Stream> SaveFileAsync(string text)
        {
            FileSavePicker SavePicker = new FileSavePicker()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "New Document",
            }; SavePicker.FileTypeChoices.Add("save", new List<string>() { $".{text.ToLower()}" });

            StorageFile storageFile = storageFile = await SavePicker.PickSaveFileAsync();


            if (storageFile == null)
            {
                return null;
            }
            var result = storageFile.OpenStreamForWriteAsync();

            return result.Result; 
        }
    }
}
