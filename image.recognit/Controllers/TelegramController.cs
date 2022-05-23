using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace image.recognit.Controllers
{
    [Route("api/message/update")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        public TelegramController()
        {

        }


        [HttpPost]
        public IActionResult Update(Update update)
        {
            return Ok();
        }
    }
}
