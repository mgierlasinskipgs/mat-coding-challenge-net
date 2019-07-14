using System;
using System.Threading.Tasks;

namespace CodingChallenge
{
    public class MqttApp
    {
        public async Task RunAsync()
        {
            await Task.Delay(500);

            Console.WriteLine("App started");
        }
    }
}
