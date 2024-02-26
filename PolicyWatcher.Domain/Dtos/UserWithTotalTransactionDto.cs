namespace PolicyWatcher.Domain.Dtos
{
    public class UserDetailDto
    {
        public DateTime UserCreatedAt { get; }
        public bool IsUserFlagged { get; }
        public decimal TodayTransactionsSum { get; }

        public UserDetailDto(decimal todayTransactionSum, bool isUserFlagged, DateTime userCreatedAt)
        {
            IsUserFlagged = isUserFlagged;
            TodayTransactionsSum = todayTransactionSum;
            UserCreatedAt = userCreatedAt;
        }
    }
}
