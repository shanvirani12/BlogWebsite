using BlogWebsite.Data;
using BlogWebsite.Models.Domain;
using BlogWebsite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            List<Tag> tags = _blogDbContext.Tags.ToList();
            return View("Index",tags);
        }

        public IActionResult Edit(Guid id)
        {

            var tag = _blogDbContext.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, EditTagRequest editTagRequest)
        {
            var tag = _blogDbContext.Tags.Find(id);
            tag.Name = editTagRequest.Name;
            tag.DisplayName = editTagRequest.DisplayName;
            _blogDbContext.SaveChanges();
            List<Tag> tags = _blogDbContext.Tags.ToList();
            return View("Index", tags);
        }

        public IActionResult Index()
        {
            List<Tag> tags = _blogDbContext.Tags.ToList();
            return View(tags);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _blogDbContext.Tags.FirstOrDefault(t => t.id == id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var tag = _blogDbContext.Tags.Find(id);
            _blogDbContext.Tags.Remove(tag);
            _blogDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
        
}
