﻿using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<BlogImage>UploadAsync(IFormFile formFile, BlogImage blogImage);
        Task<IEnumerable<BlogImage>> GetAllAsync();
    }
}
