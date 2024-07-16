using BlogWebsite.Models.Domain;
using BlogWebsite.Models.ViewModels;

namespace BlogWebsite.Repositories
{
    public interface IAdminTagRepository
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(Guid id);
        Task CreateAsync(AddTagsRequest addTagsRequest);
        Task UpdateAsync(Guid id, EditTagRequest editTagRequest);
        Task DeleteAsync(Guid id);
    }
}
