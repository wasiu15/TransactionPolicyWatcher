namespace PolicyWatcher.Domain.Interfaces.Service
{
    public interface IServiceManager
    {
        IUserService userService { get; }
        ITransactionService transactionService { get; }
    }
}
