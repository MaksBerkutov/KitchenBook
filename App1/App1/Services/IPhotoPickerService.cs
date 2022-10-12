using System.IO;
using System.Threading.Tasks;

namespace App1.Services
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
