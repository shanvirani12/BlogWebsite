using BlogWebsite.Models.ViewModels;
using BlogWebsite.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogWebsite.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private IAdminTagRepository _adminTagRepository;
        public AdminBlogPostsController(IAdminTagRepository adminTagRepository)
        {
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
            return View(addBlogPostRequest);
        }
    }
}
