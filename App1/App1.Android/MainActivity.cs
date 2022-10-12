using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using App1.ViewModels;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.IO;
using Android.Content;
using App1.Droid;

namespace App1.Droid
{
  
    [Activity(Label = "App1", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static object Instance { get; internal set; }
        public static object PickImageId { get; internal set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Instance = this;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }
        public static readonly int PickImageIds = 1000;
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageIds)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}