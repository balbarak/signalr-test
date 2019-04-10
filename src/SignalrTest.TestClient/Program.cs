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
                await Task.Delay(1000);

                var msg = GetRandomMessage();

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

        private static ProtocolGroupMessage GetRandomMessage()
        {
            return new ProtocolGroupMessage()
            {
                AffectedMember = null,
                Date = DateTime.Now,
                Body = GetRandomString(5,20),
                From = new ProtocolSender()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = GetRandomString(5,false),
                    Username = GetRandomString(3,false)
                },
                GroupId = ProtocolHelper.GROUP_ID,
                Id = Guid.NewGuid().ToString(),
                MessageType = ProtocolMessageType.Text
            };
        }

        private static string GetRandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
         
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private static string GetRandomString(int min,int max)
        {
            StringBuilder builder = new StringBuilder();
            
            int size = _random.Next(min, max);

            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString().ToLower();
            
        }

    }
}
