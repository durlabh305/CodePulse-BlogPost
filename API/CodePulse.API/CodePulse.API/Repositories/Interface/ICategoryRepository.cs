using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<BlogPostCategory> CreateAsync(BlogPostCategory blogPostCategory);
        Task<IEnumerable<BlogPostCategory>> GetAllAsync();
        Task<BlogPostCategory?> GetByIdAsync(Guid id);
        Task<BlogPostCategory?> UpdateAsync(BlogPostCategory blogPostCategory);
        Task<BlogPostCategory?> DeleteAsync(Guid id);
    }
}
