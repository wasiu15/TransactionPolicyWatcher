using PolicyWatcher.Domain.Enums;

namespace PolicyWatcher.Domain.Dtos.Request
{
    public class TransactionRequestDto
    {
        public decimal Amount { get; set; }
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
