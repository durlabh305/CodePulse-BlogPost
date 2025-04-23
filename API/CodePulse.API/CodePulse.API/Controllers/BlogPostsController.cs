using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        [HttpPost]

        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            //CONVERT DTO TO DOMAIN MODEL
            var domain = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                BlogPostCategories = new List<BlogPostCategory>()
            };

            foreach(var categoryGuid in request.BlogPostCategories)
            {
                var existingCategory = await categoryRepository.GetByIdAsync(categoryGuid);
                if(existingCategory != null)
                {
                    domain.BlogPostCategories.Add(existingCategory);
                }
            }

            domain = await blogPostRepository.CreateAsync(domain);

            //MAP DOMAIN TO DTO
            var response = new BlogPostDto
            {
                Id = domain.Id,
                Title = domain.Title,
                ShortDescription = domain.ShortDescription,
                UrlHandle = domain.UrlHandle,
                Content = domain.Content,
                FeaturedImageUrl = domain.FeaturedImageUrl,
                PublishedDate = domain.PublishedDate,
                Author = domain.Author,
                IsVisible = domain.IsVisible,
                BlogPostCategories = domain.BlogPostCategories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllBlogPosts()
        {
            var domain = await blogPostRepository.GetAllAsync();

            //MAP DOMAIN TO DTO

            var response = new List<BlogPostDto>();
            foreach (var blogPost in domain)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    IsVisible = blogPost.IsVisible,
                    BlogPostCategories = blogPost.BlogPostCategories.Select(x => new CategoryDto
                    {
                        Id=x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList(),
                });
            }
            return Ok(response);
        }
    }
}
