using System.Net;
using BlogWebsite.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository)
        {
			this.imageRepository = imageRepository;
		}
        [HttpPost]
		public async Task<IActionResult> UploadImage(IFormFile file)
		{
			var imageuri = await imageRepository.UploadAsync(file);

			if (imageuri == null)
			{
				return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
			}
			return new JsonResult(new { Link = imageuri });
		}
	}
}
