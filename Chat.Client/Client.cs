using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Client
{
    internal class Client
    {
        private HubConnection _connection;
        private string _userId;

        public async Task ConnectAsync(string userId)
        {
            _userId = userId;
            _connection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:7291/chat?userid={userId}")
                .Build();

            _connection.Closed += async (error) =>
            {
                Console.WriteLine("reconnecting...");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
                Console.WriteLine("reconnected");
            };

            _connection.On<Message>("onReceive", Receive);

            await _connection.StartAsync();
            Console.WriteLine($"connected: {_connection.ConnectionId}");
        }

        public async Task SendAsync(string recipient, string body)
        {
            var msg = new Message
            {
                Sender = _userId,
                Recipient = recipient,
                Timestamp = DateTime.UtcNow,
                Content = body
            };
            await _connection.InvokeAsync("Send", msg);
            Console.WriteLine($"sent to {recipient} at {msg.Timestamp}");
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null;
            }
        }

        private void Receive(Message msg)
        {
            Console.WriteLine($"receive {msg.Content} from {msg.Sender} sent at {msg.Timestamp}");
        }
    }
}
