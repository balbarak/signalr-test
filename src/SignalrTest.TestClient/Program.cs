using Balbarak.DataGenerator.Generators;
using SignalrTest.Common;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SignalrTest.TestClient
{
    class Program
    {
        private static Random _random = new Random();

        static async Task Main(string[] args)
        {
            AppClient client = new AppClient();

            client.OnConnected += OnConnected;
            client.OnDisconnected += OnDisconnected;

            await client.Connect();

            int count = 0;

            while (true)
            {
                await Task.Delay(400);

                var msg = GetRandomArabicMessage();

                Console.WriteLine($"Sending message {count} ...");

                count++;

                await client.SendGroupMessage(msg);
            }
            
        }

        private static void OnDisconnected(object sender, Exception e)
        {
            Console.WriteLine("Disconnected");
        }

        private static void OnConnected(object sender, EventArgs e)
        {
            Console.WriteLine("Connected");
        }

        private static ProtocolGroupMessage GetRandomArabicMessage()
        {

            return new ProtocolGroupMessage()
            {
                AffectedMember = null,
                Date = DateTime.Now,
                Body = WordGenerator.GenerateArabicParagraph(5,20),
                From = new ProtocolSender()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = NameGenerator.GenerateName().FirstNameAr,
                    Username = NameGenerator.GenerateUsername()
                },
                GroupId = AppClient.GROUP_ID,
                Id = Guid.NewGuid().ToString(),
                MessageType = ProtocolMessageType.Text
            };
        }

        private static ProtocolGroupMessage GetRandomEnglishMessage()
        {

            return new ProtocolGroupMessage()
            {
                AffectedMember = null,
                Date = DateTime.Now,
                Body = WordGenerator.GenerateEnglishcParagraph(5, 15),
                From = new ProtocolSender()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = NameGenerator.GenerateName().FirstNameEn,
                    Username = NameGenerator.GenerateUsername()
                },
                GroupId = AppClient.GROUP_ID,
                Id = Guid.NewGuid().ToString(),
                MessageType = ProtocolMessageType.Text
            };
        }


    }
}
