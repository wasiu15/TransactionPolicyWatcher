using System.ComponentModel.DataAnnotations;

namespace PolicyWatcher.Domain.Models
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public bool IsSent { get; set; }
        public DateTime SentTime { get; set; }
    }
}
