using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kubex.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Kubex.BLL.Services
{
    public class FileService : IFileService
    {
        public async Task<string> UploadImage(IFormFile file) 
        {
            if (file == null || file.Length <= 0)
                throw new ApplicationException("Invalid image. Retry or choose a different image.");
            
            var validImageExtensions = new string[] {".png", ".jpg", ".jpeg", ".bmp"};
            var extension = Path.GetExtension(file.FileName);
            if (! validImageExtensions.Contains(extension))
                throw new ApplicationException("You are only allowed to use .jpg, .jpeg, .png and .bmp extensions!");


            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fileLocation = pathToSave + $@"\{file.FileName}";

            using FileStream fs = new FileStream(
                fileLocation,
                FileMode.Create,
                FileAccess.ReadWrite,
                FileShare.ReadWrite,
                bufferSize: 4096,
                useAsync: true
            );

            await file.CopyToAsync(fs);

            return fileLocation;
        }
    }
}