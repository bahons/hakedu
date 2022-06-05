using System.Threading.Tasks;

namespace telebot.Models
{
    public class CognitService
    {
        public async Task TaskAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
            }
        }
    }
}
