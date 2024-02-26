using Microsoft.EntityFrameworkCore;
using PolicyWatcher.Domain.Interfaces.Repository;
using PolicyWatcher.Domain.Models;
using PolicyWatcher.Infrastructure.Data;

namespace PolicyWatcher.Infrastructure.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        private readonly PolicyWatcherDbContext _context;
        public TransactionRepository(PolicyWatcherDbContext dbContext) : base(dbContext)
        {
            
        }
        public void CreateTransaction(Transaction transaction) => Create(transaction);
        public void UpdateTransaction(Transaction transaction) => Update(transaction);
        public void UpdateBulkTransaction(List<Transaction> transactions) => UpdateBulk(transactions);
        public void DeleteTransaction(Transaction transaction) => Delete(transaction);

        public async Task<Transaction> GetTransactionId(int transactionId, bool trackChanges) => await FindByCondition(x => x.TransactionId.Equals(transactionId), trackChanges).FirstOrDefaultAsync();

        public async Task<IEnumerable<Transaction>> GetTransactions(bool trackChanges) => await FindAll(trackChanges).ToListAsync();

        public async Task<List<Transaction>> GetWatchableTransactions(bool trackChanges) => await FindByCondition(x => !x.IsIntervalChecked, trackChanges).Include(x => x.Sender).Include(x => x.Receiver).ToListAsync();

    }
}
