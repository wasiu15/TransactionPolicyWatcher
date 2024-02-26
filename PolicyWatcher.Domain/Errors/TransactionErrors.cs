namespace PolicyWatcher.Domain.Errors
{
    public class TransactionErrors
    {
        public static Error TransactionNotFound(int transactionId) => new(
        "Transactions.NotFound", $"The transaction with the Id = '{transactionId}' was not found");

        public static Error InvalidTransactionStatus => new(
            "Transactions.InvalidTier", $"The transaction status you selected is invalid");

        public static Error InvalidAmount => new(
            "Transactions.InvalidAmount", $"The amount you entered is invalid");

        public static Error InvalidUser => new(
            "Transactions.InvalidAmount", $"The user id you entered is invalid");
    }
}
