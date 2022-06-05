using image.recognit.TelegramService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace image.recognit.Controllers
{
    
    [ApiController]
    [Route("api/message/update")]
    public class TelegramController : ControllerBase
    {
        private readonly ICommandExecutor _commandExecutor;

        public TelegramController(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        [HttpGet]
        public IActionResult Get() => Content("get success");


        [HttpPost]
        public async Task<IActionResult> Update([FromBody] object update)
        {
            // /start => register user

            var upd = JsonConvert.DeserializeObject<Update>(update.ToString());

            if (upd?.Message?.Chat == null && upd?.CallbackQuery == null)
            {
                return Ok();
            }

            try
            {
                await _commandExecutor.Execute(upd);
            }
            catch (Exception e)
            {
                return Ok();
            }

            return Ok();
        }
    }
}
