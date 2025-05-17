using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

        //Creating BlogPost
        [HttpPost]
        //[Authorize(Roles = "Writer")]
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

        //Getting All BlogPosts
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

        //Getting BlogPost by ID
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult>GetBlogPostById([FromRoute] Guid id)
        {
            //Getting blogpost from repository.
            var existingModel = await blogPostRepository.GetByIdAsync(id);
            if (existingModel == null)
            {
                return NotFound();
            }

            //Convert domain model to DTO

            var response = new BlogPostDto
            {
                Id = existingModel.Id,
                Title = existingModel.Title,
                ShortDescription = existingModel.ShortDescription,
                UrlHandle = existingModel.UrlHandle,
                Content = existingModel.Content,
                FeaturedImageUrl = existingModel.FeaturedImageUrl,
                PublishedDate = existingModel.PublishedDate,
                Author = existingModel.Author,
                IsVisible = existingModel.IsVisible,
                BlogPostCategories = existingModel.BlogPostCategories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);    
        }

        //Getting BlogPost by urlHandle
        [HttpGet]
        [Route("{urlHandle}")]

        public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            //Getting blogpost details from repository
            var blogpost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);

            //MAP DOMAIN TO DTO

            var response = new BlogPostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                UrlHandle = blogpost.UrlHandle,
                Content = blogpost.Content,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                PublishedDate = blogpost.PublishedDate,
                Author = blogpost.Author,
                IsVisible = blogpost.IsVisible,
                BlogPostCategories = blogpost.BlogPostCategories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList(),
            };

            return Ok(response);
        }


        //Updating BlogPost
        [HttpPut]
        [Route("{id:Guid}")]
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, [FromBody] UpdateBlogPostRequestDto request)
        {
            //Convert DTO TO DOMAIN MODEL
            var blogpost = new BlogPost
            {
                Id = id,
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

            foreach (var categoryGuid in request.BlogPostCategories)
            {
                var existingCategory = await categoryRepository.GetByIdAsync(categoryGuid);
                if (existingCategory != null)
                {
                   blogpost.BlogPostCategories.Add(existingCategory);
                }
            }

            // Call Repository to update blogpost domain model

            var updatedBlogPost = await blogPostRepository.UpdateAsync(blogpost);

            if (updatedBlogPost == null)
            {
                return NotFound();
            }

            //Convert domain model to DTO

            var response = new BlogPostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                UrlHandle = blogpost.UrlHandle,
                Content = blogpost.Content,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                PublishedDate = blogpost.PublishedDate,
                Author = blogpost.Author,
                IsVisible = blogpost.IsVisible,
                BlogPostCategories = blogpost.BlogPostCategories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute]Guid id)
        {
            var deletedblogpost = await blogPostRepository.DeleteAsync(id);
            if(deletedblogpost == null)
            {
                return NotFound();
            }
            //Convert Domain to DTO As a return object

            var response = new BlogPostDto
            {
                Id = deletedblogpost.Id,
                Title = deletedblogpost.Title,
                ShortDescription = deletedblogpost.ShortDescription,
                Author = deletedblogpost.Author,
                IsVisible = deletedblogpost.IsVisible,
                PublishedDate = deletedblogpost.PublishedDate,
                Content = deletedblogpost.Content,
                FeaturedImageUrl = deletedblogpost.FeaturedImageUrl,
            };
            return Ok(response);
        }
    }
}
