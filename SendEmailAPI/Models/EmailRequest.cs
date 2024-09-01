namespace SendEmailAPI.Models
{
    public class EmailRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ToEmail { get; set; }
    }
}
