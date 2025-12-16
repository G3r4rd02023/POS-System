
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace POS.Backend.Helpers
{
    public class PhotoHelper : IPhotoHelper
    {
        private readonly Cloudinary _cloudinary;

        public PhotoHelper(IConfiguration config)
        {
            var acc = new Account(
                 config["CloudinarySettings:CloudName"],
                 config["CloudinarySettings:ApiKey"],
                 config["CloudinarySettings:ApiSecret"]
             );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<string> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok" ? result.Result : null!;
        }

        public async Task<string> UploadImage(string imageBase64)
        {
            if (!string.IsNullOrEmpty(imageBase64))
            {
                var base64Data = imageBase64[(imageBase64.IndexOf(',') + 1)..];
                var imageBytes = Convert.FromBase64String(base64Data);

                using var stream = new MemoryStream(imageBytes);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription("foto.jpg", stream),
                    AssetFolder = "tecnologers"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception("Error al cargar la imagen: " + uploadResult.Error.Message);
                }

                var urlFoto = uploadResult.SecureUrl.ToString();
                return urlFoto;
            }
            return "Ocurrió un error al cargar la imagen";
        }
    }
}
