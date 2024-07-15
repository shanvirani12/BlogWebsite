using BlogWebsite.Data;
using BlogWebsite.Models.Domain;
using BlogWebsite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Controllers
{
    public class AdminTagsController : Controller
    {
        private BlogDbContext _blogDbContext;

        public AdminTagsController(BlogDbContext blogDbContext)
        {
            this._blogDbContext = blogDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagsRequest addTagsRequest)
        {
            var tag = new Tag
            {
                Name = addTagsRequest.Name,
                DisplayName = addTagsRequest.DisplayName
            };
            _blogDbContext.Tags.Add(tag);
            _blogDbContext.SaveChanges();
            return View("Add");
        }
    }
}
