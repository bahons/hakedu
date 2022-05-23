using image.recognit.TelegramService;
using image.recognit.TelegramService.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace image.recognit.Middleware
{
    public static class TelegramDiServices
    {
        public static void TelegramDIService(this IServiceCollection services)
        {
            services.AddSingleton<TelegramBot>();
            services.AddSingleton<ICommandExecutor, CommandExecutor>();
            services.AddSingleton<BaseCommand, StartCommand>();
        }
    }
}
