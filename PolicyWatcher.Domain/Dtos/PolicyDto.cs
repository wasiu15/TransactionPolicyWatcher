namespace PolicyWatcher.Domain.Dtos
{
    public class PolicyDto
    {
        public List<int> TransactionsWithAmountViolations { get; set; }
        public List<Dictionary<string, int>> TransactionsWithIntervalViolations { get; set; }
        public List<int> TransactionsWithNewUserViolations { get; set; }
        public List<int> TransactionsWithFlaggedUserViolations { get; set; }
        public List<int> TransactionsWithTierUserViolations { get; set; }

    }
}
