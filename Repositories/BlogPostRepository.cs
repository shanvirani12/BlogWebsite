using BlogWebsite.Data;
using BlogWebsite.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BlogWebsite.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext dbContext;
        
        public BlogPostRepository(BlogDbContext blogDb)
        {
            this.dbContext = blogDb;  
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await dbContext.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task DeleteAsync(Guid id)
        {
			var blogPost = dbContext.BlogPosts.Find(id);
			if (blogPost != null)
			{
				dbContext.BlogPosts.Remove(blogPost);
				await dbContext.SaveChangesAsync();
			}
		}

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
            
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost post)
        {
            var existingBlog = await dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.id == post.id);

            if(existingBlog != null)
            {
                existingBlog.id = post.id;
                existingBlog.Heading = post.Heading;
                existingBlog.PageTitle = post.PageTitle;
                existingBlog.Content = post.Content;
                existingBlog.Author = post.Author;
                existingBlog.ShortDescription = post.ShortDescription;
                existingBlog.UrlHandle = post.UrlHandle;
                existingBlog.Visible = post.Visible;
                existingBlog.PublishedDate = post.PublishedDate;
                existingBlog.Tags = post.Tags;

                await dbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
