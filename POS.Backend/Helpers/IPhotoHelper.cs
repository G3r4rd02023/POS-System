namespace POS.Backend.Helpers
{
    public interface IPhotoHelper
    {
        Task<string> UploadImage(string imageBase64);

        Task<string> DeletePhotoAsync(string publicId);
    }
}
