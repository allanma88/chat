namespace Chat.Client
{
    public class Message
    {
        public string Sender { get; set; }

        public string Recipient { get; set; }

        public DateTime Timestamp { get; set; }

        public object Content { get; set; }
    }
}
