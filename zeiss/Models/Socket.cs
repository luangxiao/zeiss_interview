namespace zeiss.Models
{
    public class Socket
    {
        public string Id { get; set; }= Guid.NewGuid().ToString();
        public string Topic { get; set; }
        public string? Ref { get; set; }
        public Machine Payload { get; set; }
        public string Event { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
