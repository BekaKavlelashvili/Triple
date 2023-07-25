using Triple.Application.Dtos.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostingEnv;

        public ImageService(IWebHostEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        public async Task<ImageDto> ImportAsync(IFormFile file, string directory)
        {
            if (!Directory.Exists("Images/PackPhotos"))
            {
                Directory.CreateDirectory("Images/PackPhotos");
            }

            //int imgWidth = 273, imgHeight = 175;

            using var image = Image.Load(file.OpenReadStream());

            //if (image.Width > imgWidth || image.Height > imgHeight)
            //{
            //    int resizeNumber = image.Width > image.Height ? image.Width / imgWidth : image.Height / imgHeight;

            //    imgWidth = image.Width / resizeNumber;
            //    imgHeight = image.Height / resizeNumber;
            //}

            //image.Mutate(x => x.Resize(imgWidth, imgHeight));

            var encoder = new JpegEncoder()
            {
                Quality = 100
            };

            var mimeType = file.ContentType;
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

            using (var fileStream = new FileStream($"Images/{directory}/{uniqueFileName}", FileMode.Create))
            {
                image.Save(fileStream, encoder);

                //await file.CopyToAsync(fileStream);
            }

            return new ImageDto
            {
                MimeType = mimeType,
                OriginalFileName = file.FileName,
                UniqueFileName = uniqueFileName,
                Path = $"Images/{directory}/{uniqueFileName}"
            };
        }
    }

    public interface IImageService
    {
        Task<ImageDto> ImportAsync(IFormFile file, string directory);
    }
}
