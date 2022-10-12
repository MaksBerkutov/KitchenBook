using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System.IO;
using System.Threading.Tasks;

using Xamarin.Forms;
using Sample.Droid;
using App1.Services;
using App1.Droid;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace Sample.Droid
{
    public class PhotoPickerService : IPhotoPickerService
    {
        public Task<Stream> GetImageStreamAsync()
        {
            try
            {
                Intent intent = new Intent();
                intent.SetType("image/*");
                intent.SetAction(Intent.ActionGetContent);

                // Start the picture-picker activity (resumes in MainActivity.cs)

                MainActivity i = (MainActivity.Instance as MainActivity); i.StartActivityForResult(
                    Intent.CreateChooser(intent, "Select Picture"),
                    MainActivity.PickImageIds);

                // Save the TaskCompletionSource object as a MainActivity property
                i.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

                // Return Task object
                return i.PickImageTaskCompletionSource.Task;
            }
            catch (System.Exception)
            {

                
            }
            return null;
            // Define the Intent for getting images
           
        }
    }
}