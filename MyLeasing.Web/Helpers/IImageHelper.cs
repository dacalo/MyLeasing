namespace MyLeasing.Web.Helpers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}
