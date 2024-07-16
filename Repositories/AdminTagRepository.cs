using BlogWebsite.Data;
using BlogWebsite.Models.Domain;
using BlogWebsite.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Repositories
{
    public class AdminTagRepository : IAdminTagRepository
    {
        private BlogDbContext _blogDbContext;

        public AdminTagRepository(BlogDbContext dbContext)
        {
            this._blogDbContext = dbContext;
        }

        public async Task CreateAsync(AddTagsRequest addTagsRequest)
        {
            var tag = new Tag
            {
                Name = addTagsRequest.Name,
                DisplayName = addTagsRequest.DisplayName
            };
            _blogDbContext.Tags.Add(tag);
            await _blogDbContext.SaveChangesAsync();
 
        }

        public async Task DeleteAsync(Guid id)
        {
            var tag = _blogDbContext.Tags.Find(id);
            if (tag != null)
            {
                _blogDbContext.Tags.Remove(tag);
                await _blogDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _blogDbContext.Tags.ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(Guid id)
        {
            return await _blogDbContext.Tags.FindAsync(id);
        }

        public async Task UpdateAsync(Guid id, EditTagRequest editTagRequest)
        {
            var tag = _blogDbContext.Tags.Find(id);
            tag.Name = editTagRequest.Name;
            tag.DisplayName = editTagRequest.DisplayName;
            await _blogDbContext.SaveChangesAsync();
        }
    }
}
