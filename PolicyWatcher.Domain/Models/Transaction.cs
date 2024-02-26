using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PolicyWatcher.Domain.Enums;

namespace PolicyWatcher.Domain.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }

        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; } // Navigation property for the sender user
        
        public int ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; } // Navigation property for the receiver user
        
        public TransactionStatus Status { get; set; }
        public bool IsPolicyChecked { get; set; }
        public bool IsIntervalChecked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set;}

    }
}
