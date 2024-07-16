using BlogWebsite.Data;
using BlogWebsite.Models.Domain;
using BlogWebsite.Models.ViewModels;
using BlogWebsite.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Controllers
{
    public class AdminTagsController : Controller
    {
        private IAdminTagRepository AdminTagRepository;

        public AdminTagsController(IAdminTagRepository AdminTagRepository)
        {
            this.AdminTagRepository = AdminTagRepository;
        }
        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var Tag = await AdminTagRepository.GetAllAsync();
            return View(Tag);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagsRequest addTagsRequest)
        {
            await AdminTagRepository.CreateAsync(addTagsRequest);
            var tags = await AdminTagRepository.GetAllAsync();
            return View("Index",tags);
        }

        public async Task<IActionResult> Edit(Guid id)
        {

            var tag = await AdminTagRepository.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EditTagRequest editTagRequest)
        {
            await AdminTagRepository.UpdateAsync(id, editTagRequest);
			var tags = await AdminTagRepository.GetAllAsync();
			return View("Index",tags);
        }



        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await AdminTagRepository.GetByIdAsync(id.Value);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await AdminTagRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
        
}
