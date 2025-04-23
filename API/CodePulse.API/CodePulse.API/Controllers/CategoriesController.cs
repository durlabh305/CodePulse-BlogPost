using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpPost]

        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {

            //MAP DTO TO DOMAIN MODEL
            var category = new BlogPostCategory
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            await categoryRepository.CreateAsync(category);

            //MAP DOMAIN TO DTO

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(categoryDto);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            //MAP DOMAIN TO DTO

            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }
            return Ok(response);
        }

        //https://localhost:7132/api/Categories/4E0F01E7-4F70-4269-E5A3-08DD7DAAA8A4
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid id) { 
            var categoryDomain = await categoryRepository.GetByIdAsync(id);

            if(categoryDomain == null)
            {
                return NotFound();
            }

            //MAP DOMAIN TO DTO

            var response = new CategoryDto
            {
                Id = categoryDomain.Id,
                Name = categoryDomain.Name,
                UrlHandle = categoryDomain.UrlHandle,
            };
            return Ok(response);
           }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            //CONVERT DTO TO DOMAIN-MODEL
            var category = new BlogPostCategory
            {
                Id = id,
                Name = updateCategoryRequestDto.Name,
                UrlHandle = updateCategoryRequestDto.UrlHandle,
            };
            
            category = await categoryRepository.UpdateAsync(category);

            if(category == null)
            {
                return NotFound();
            }

            //CONVERT DOMAIN TO DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid id)
        {
            var categoryDomain = await categoryRepository.DeleteAsync(id);
            if(categoryDomain == null)
            {
                return NotFound();
            }

            //DOMAIN MODEL TO DTO

            var response = new CategoryDto
            {
                Id = categoryDomain.Id,
                Name = categoryDomain.Name,
                UrlHandle = categoryDomain.UrlHandle,
            };

            return Ok(response);
        }
    }
}
