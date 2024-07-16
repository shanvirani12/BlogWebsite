using BlogWebsite.Models.Domain;
using BlogWebsite.Models.ViewModels;
using BlogWebsite.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogWebsite.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly IAdminTagRepository _adminTagRepository;
        private readonly IBlogPostRepository blogPostRepository;
        public AdminBlogPostsController(IAdminTagRepository adminTagRepository, IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this._adminTagRepository = adminTagRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await _adminTagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };
            var selectedTags = new List<Tag>();
            foreach(var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagsIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await _adminTagRepository.GetByIdAsync(selectedTagsIdAsGuid);
                if (existingTag != null) 
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogPost.Tags = selectedTags; 
            await blogPostRepository.AddAsync(blogPost);
            return View(addBlogPostRequest);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await _adminTagRepository.GetAllAsync();
            if(blogPost != null)
            {
				var model = new EditBlogRequest
				{
					id = blogPost.id,
					Heading = blogPost.Heading,
					PageTitle = blogPost.PageTitle,
					Content = blogPost.Content,
					Author = blogPost.Author,
					PublishedDate = blogPost.PublishedDate,
					UrlHandle = blogPost.UrlHandle,
					ShortDescription = blogPost.ShortDescription,
					Visible = blogPost.Visible,
					Tags = tagsDomainModel.Select(x => new SelectListItem
					{
						Text = x.Name,
						Value = x.id.ToString()
					}),
					SelectedTags = blogPost.Tags.Select(x => x.id.ToString()).ToArray()
				};
                return View(model);
			}
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogRequest editBlogRequest)
        {
            var blogPostDomainModel = new BlogPost
            {
                id = editBlogRequest.id,
                Heading = editBlogRequest.Heading,
                PageTitle = editBlogRequest.PageTitle,
                Content = editBlogRequest.Content,
                UrlHandle = editBlogRequest.UrlHandle,
                Author = editBlogRequest.Author,
                PublishedDate = editBlogRequest.PublishedDate,
                ShortDescription = editBlogRequest.ShortDescription,
                Visible = editBlogRequest.Visible
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await _adminTagRepository.GetByIdAsync(tag);
                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }
            blogPostDomainModel.Tags = selectedTags;

            var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);
            if (updatedBlog != null)
            {
                return RedirectToAction("Edit");
            }
			return RedirectToAction("Edit");
		}
    }
}
