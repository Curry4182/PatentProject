namespace Sender.Models
{
    public class Message
    {
        public string Type { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string ImageId { get; set; }
    }
}
