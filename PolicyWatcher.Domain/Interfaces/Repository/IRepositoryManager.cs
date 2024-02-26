namespace PolicyWatcher.Domain.Interfaces.Repository
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        Task SaveAsync();
    }
}
