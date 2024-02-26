using PolicyWatcher.Domain.Models;

namespace PolicyWatcher.Domain.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        void CreateTransaction(Transaction transaction);
        void DeleteTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void UpdateBulkTransaction(List<Transaction> transactions);
        Task<IEnumerable<Transaction>> GetTransactions(bool trackChanges);
        Task<Transaction> GetTransactionId(int transactionId, bool trackChanges);
        Task<List<Transaction>> GetWatchableTransactions(bool trackChanges);
    }
}
