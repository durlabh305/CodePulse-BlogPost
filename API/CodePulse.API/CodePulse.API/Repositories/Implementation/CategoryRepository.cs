using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPostCategory> CreateAsync(BlogPostCategory blogPostCategory)
        {
            await dbContext.BlogPostsCategories.AddAsync(blogPostCategory);
            await dbContext.SaveChangesAsync();
            return blogPostCategory;
        }

        public async Task<BlogPostCategory?> DeleteAsync(Guid id)
        {
            var existingModel = await dbContext.BlogPostsCategories.FirstOrDefaultAsync(x=> x.Id == id);
            if(existingModel == null)
            {
                return null;
            }

            dbContext.BlogPostsCategories.Remove(existingModel);
            await dbContext.SaveChangesAsync();
            return existingModel;
        }

        public async Task<IEnumerable<BlogPostCategory>> GetAllAsync()
        {
            return await dbContext.BlogPostsCategories.ToListAsync();
        }

        public async Task<BlogPostCategory?> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPostsCategories.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<BlogPostCategory?> UpdateAsync(BlogPostCategory blogPostCategory)
        {
            var existingCategory = await dbContext.BlogPostsCategories.FirstOrDefaultAsync(x=> x.Id == blogPostCategory.Id);
            if (existingCategory != null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(blogPostCategory);
                await dbContext.SaveChangesAsync();
                return blogPostCategory;
            }

            return null;

        }
    }
}
