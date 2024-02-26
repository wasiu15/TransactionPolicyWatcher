using Microsoft.Extensions.Configuration;
using PolicyWatcher.Domain.Interfaces.Repository;
using PolicyWatcher.Domain.Interfaces.Service;

namespace PolicyWatcher.Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<ITransactionService> _transactionService;

        public ServiceManager(IRepositoryManager repositoryManager, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, configuration));
            _transactionService = new Lazy<ITransactionService>(() => new TransactionService(repositoryManager, serviceProvider));
        }

        
        public IUserService userService => _userService.Value;

        public ITransactionService transactionService => _transactionService.Value;

    }
}
