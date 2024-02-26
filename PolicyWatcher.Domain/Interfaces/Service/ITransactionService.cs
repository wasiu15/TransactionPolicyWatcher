using PolicyWatcher.Domain.Dtos.Request;
using PolicyWatcher.Domain.Dtos.Response;
using PolicyWatcher.Domain.Models;

namespace PolicyWatcher.Domain.Interfaces.Service
{
    public interface ITransactionService
    {
        Task<GenericResponse<IEnumerable<Transaction>>> GetTransactions();
        Task<GenericResponse<Transaction>> CreateTransaction(TransactionRequestDto transaction);
        Task<GenericResponse<Transaction>> GetTransactionById(int TransactionId);
        Task<GenericResponse<Transaction>> DeleteTransaction(int transactionId);
        Task TransactionWatcher();
        Task StartTransactionWatcher(int interval);
    }
}
