using CodingChallenge.Models;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge
{
    public class MqttApp
    {
        private readonly IMqttClient _client;

        public MqttApp()
        {
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            _client.UseConnectedHandler(async e =>
            {
                Console.WriteLine("### CONNECTED WITH SERVER ###");

                await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic("carCoordinates").Build());

                Console.WriteLine("### SUBSCRIBED ###");
            });

            _client.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await _client.ConnectAsync(GetOptions());
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });

            _client.UseApplicationMessageReceivedHandler(HandleMessage);
        }

        public Task RunAsync()
        {
            return _client.ConnectAsync(GetOptions());
        }

        private IMqttClientOptions GetOptions()
        {
            return new MqttClientOptionsBuilder()
                .WithTcpServer("127.0.0.1", 1883)
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(10))
                .WithCommunicationTimeout(TimeSpan.FromSeconds(10))
                .Build();
        }

        private void HandleMessage(MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");

            Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
            Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
            Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
            Console.WriteLine();

            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            var coordinates = JsonConvert.DeserializeObject<CarCoordinates>(payload);

            var car = GetCarByIndex(coordinates.CarIndex);
            var distance = ComputeDistance(car.LastLocation.Location, coordinates.Location);

            car.TotalDistance += distance;

            var speed = ComputeSpeed(DateTimeOffset.FromUnixTimeSeconds(car.LastLocation.Timestamp), 
                DateTimeOffset.FromUnixTimeSeconds(coordinates.Timestamp), distance);

            PublishStatus(coordinates.Timestamp, coordinates.CarIndex, "SPEED", speed);
        }

        private Car GetCarByIndex(int index)
        {
            return null;
        }

        private double ComputeDistance(Location lastLocation, Location currentLocation)
        {
            return 0;
        }

        private double ComputeSpeed(DateTimeOffset lastTime, DateTimeOffset currentTime, double distance)
        {
            var kph = (distance / 1000.0f) / ((currentTime - lastTime).TotalSeconds / 3600.0f);
            var speed = kph / 1.609f;

            return speed;
        }

        private void PublishStatus(long timestamp, int carIndex, string type, double value)
        {
            var json = JsonConvert.SerializeObject(new CarStatus
            {
                Timestamp = timestamp,
                CarIndex = carIndex,
                Type = type,
                Value = value
            });

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("carStatus")
                .WithPayload(json)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            Task.Run(() => _client.PublishAsync(message));
            //Task.Run(() => _client.PublishAsync("hello/world"));
        }
    }
}
