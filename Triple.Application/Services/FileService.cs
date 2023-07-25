using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Triple.Application.Dtos.Shared;

namespace Triple.Application.Services
{
	public class FileService : IFileService
	{
		public async Task<FileDto> ImportAsync(IFormFile file, string directory)
		{
			var mimeType = file.ContentType;
			var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

			using (var fileStream = new FileStream($"App_Data/{directory}/{uniqueFileName}", FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			return new FileDto
			{
				MimeType = mimeType,
				OriginalFileName = file.FileName,
				UniqueFileName = uniqueFileName,
				Path = $"App_Data/{directory}/{uniqueFileName}"
			};
		}
	}

	public interface IFileService
	{
		Task<FileDto> ImportAsync(IFormFile file, string directory);
	}
}
