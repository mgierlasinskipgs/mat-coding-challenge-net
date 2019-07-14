using System;
using System.Threading.Tasks;

namespace CodingChallenge
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var app = new MqttApp();
            await app.RunAsync();

            Console.ReadLine();
        }
    }
}
