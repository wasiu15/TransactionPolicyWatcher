using PolicyWatcher.Domain.Interfaces.Repository;
using PolicyWatcher.Infrastructure.Data;

namespace PolicyWatcher.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly PolicyWatcherDbContext _repositoryContext;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<ITransactionRepository> _transactionRepository;
        public RepositoryManager(PolicyWatcherDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));

            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
            _transactionRepository = new Lazy<ITransactionRepository>(() => new TransactionRepository(repositoryContext));
        }

        public IUserRepository UserRepository => _userRepository.Value;
        public ITransactionRepository TransactionRepository => _transactionRepository.Value;
        public async Task SaveAsync()
        {
            try
            {
                await _repositoryContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                throw;
            }
        }
    }
}
