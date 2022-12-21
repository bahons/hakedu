using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using telebot.Services;
using Telegram.Bot.Types;

namespace telebot.Controllers
{
/*    [Route("api/[controller]")]
    [ApiController]*/
    public class WebhookController : ControllerBase
    {
        public WebhookController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                      [FromBody] Update update)
        {
            await handleUpdateService.EchoAsync(update);
            return Ok();
        }


        [HttpGet]
        public IActionResult Get() => Content("ok get");
    }
}
