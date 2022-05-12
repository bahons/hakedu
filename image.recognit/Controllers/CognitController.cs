using image.recognit.Models;
using image.recognit.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace image.recognit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CognitController : ControllerBase
    {
        private RecognitService _recognit;
        private IWebHostEnvironment _appEnvironment;
        public CognitController(RecognitService recognitService, IWebHostEnvironment appEnvironment)
        {
            _recognit = recognitService;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        public async Task<JsonResult> Post(IFormFile image)
        {
            if (image != null)
            {
                // путь к папке Files
                string path = "/files/" + image.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                var result = _recognit.Recognit(_appEnvironment.WebRootPath + path);
                return new JsonResult(result);
            }
            return new JsonResult("no content");
        }
    }
}
