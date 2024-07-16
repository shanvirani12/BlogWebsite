using BlogWebsite.Models.Domain;

namespace BlogWebsite.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?> GetAsync(Guid id);
        Task<BlogPost?> UpdateAsync(BlogPost post);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
