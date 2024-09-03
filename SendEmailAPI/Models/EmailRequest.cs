namespace SendEmailAPI.Models
{
    public class EmailRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> ToEmails { get; set; }
    }
}
