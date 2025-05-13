using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  // Added to enable automatic model validation
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(  IFormFile file, [FromForm] string fileName,
             [FromForm] string title)
        {
            var validationResult = ValidateFileUpload(file);

            if (validationResult != null)
                return BadRequest(validationResult);

            try
            {
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.UtcNow
                };

                blogImage = await imageRepository.UploadAsync(file, blogImage);

                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Url = blogImage.Url
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        private IActionResult ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (file == null || file.Length == 0)
                return BadRequest("File cannot be empty.");

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                return BadRequest("Unsupported file type. Allowed types: jpg, jpeg, png.");

            if (file.Length > 10485760) // 10MB
                return BadRequest("File size cannot exceed 10MB.");

            return null; // Validation passed
        }

        [HttpGet]

        public async Task<IActionResult> GetAllImages()
        {
            //Call the imageRepository to get all the images

            var images = await imageRepository.GetAllAsync();

            // MAP DOMAIN MODEL TO DTO

            var response = new List<BlogImageDto>();
            foreach (var image in images)
            {
                response.Add(new BlogImageDto
                {
                    Id = image.Id,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Url = image.Url
                });
            }
            return Ok(response);
        }

    };

};
