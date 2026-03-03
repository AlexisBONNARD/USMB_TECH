namespace USMB_TECH.Models
{
    public class EmailRequest
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Message { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
