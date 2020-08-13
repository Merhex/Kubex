using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Kubex.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Kubex.BLL.Services
{
    public class CloudinaryService : IFileService
    {
        private readonly IConfiguration _configuration;

        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var account = new Account(
                _configuration.GetSection("AppSettings:CloudinaryName").Value,
                _configuration.GetSection("AppSettings:CloudinaryKey").Value,
                _configuration.GetSection("AppSettings:CloudinarySecret").Value
            );

            var cloudinary = new Cloudinary(account);

            var imageUploadParams = new ImageUploadParams 
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            };

            var result = await cloudinary.UploadAsync(imageUploadParams);

            if (result.Error != null)
                throw new ApplicationException(result.Error.Message);
            
            return result.Url.AbsoluteUri;
        }
    }
}