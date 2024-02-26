using Microsoft.Extensions.DependencyInjection;
using PolicyWatcher.Domain.Dtos;
using PolicyWatcher.Domain.Dtos.Request;
using PolicyWatcher.Domain.Dtos.Response;
using PolicyWatcher.Domain.Enums;
using PolicyWatcher.Domain.Errors;
using PolicyWatcher.Domain.Interfaces.Repository;
using PolicyWatcher.Domain.Interfaces.Service;
using PolicyWatcher.Domain.Models;
using PolicyWatcher.Infrastructure.Helpers;

namespace PolicyWatcher.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IServiceProvider _serviceProvider;
        private bool isProcessing = false; 
        private Timer _timer;

        public TransactionService(IRepositoryManager repositoryManager, IServiceProvider serviceProvider)
        {
            _repositoryManager = repositoryManager;
            _serviceProvider = serviceProvider;
        }
        public async Task<GenericResponse<Transaction>> CreateTransaction(TransactionRequestDto transactionDto)
        {
            if (transactionDto.Amount < 1) return TransactionErrors.InvalidAmount;
            
            if (transactionDto.SenderUserId < 1 || transactionDto.ReceiverUserId < 1) return TransactionErrors.InvalidUser;

            if(transactionDto.SenderUserId == transactionDto.ReceiverUserId) return TransactionErrors.InvalidUser;

            if (!Enum.IsDefined(typeof(TransactionStatus), transactionDto.Status)) return TransactionErrors.InvalidTransactionStatus;

            //  confirm if sender and receiver exist
            var getSenderandReceiver = await _repositoryManager.UserRepository.GetSenderAndReceiver(transactionDto.SenderUserId, transactionDto.ReceiverUserId, false);
            if(getSenderandReceiver.Count < 2)
            {
                return new GenericResponse<Transaction> { ResponseCode = "00", IsSuccessful = false, ResponseMessage = "Invalid user id detected", Data = null };
            }

            var transaction = new Transaction
            {
                Amount = transactionDto.Amount,
                SenderId = transactionDto.SenderUserId,
                ReceiverId = transactionDto.ReceiverUserId,
                Status = transactionDto.Status,
                CreatedAt = DateTime.Now
            };
            _repositoryManager.TransactionRepository.CreateTransaction(transaction);
            await _repositoryManager.SaveAsync();

            return new GenericResponse<Transaction> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = $"Transaction with ID = {transaction.TransactionId} was created successful", Data = null };
        }

        public async Task<GenericResponse<Transaction>> DeleteTransaction(int transactionId)
        {
            if (transactionId < 0) return TransactionErrors.InvalidTransactionStatus;

            var transactionToDelete = await _repositoryManager.TransactionRepository.GetTransactionId(transactionId, true);
            if (transactionToDelete != null) return TransactionErrors.TransactionNotFound(transactionId);

            _repositoryManager.TransactionRepository.DeleteTransaction(transactionToDelete);
            await _repositoryManager.SaveAsync();

            return new GenericResponse<Transaction> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = $"Transaction with ID = {transactionId} was deleted Successful", Data = null };
        }

        public async Task<GenericResponse<Transaction>> GetTransactionById(int transactionId)
        {
            var transaction = await _repositoryManager.TransactionRepository.GetTransactionId(transactionId, false);
            return new GenericResponse<Transaction> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = "Transaction fetched Successful", Data = transaction };
        }

        public async Task<GenericResponse<IEnumerable<Transaction>>> GetTransactions()
        {
            var transactions = await _repositoryManager.TransactionRepository.GetTransactions(false);
            return new GenericResponse<IEnumerable<Transaction>> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = transactions.Count() > 1 ? "Transactions fetched successful" : "No transaction found", Data = transactions };
        }

        public async Task TransactionWatcher()
        {
            if (isProcessing)
            {
                // Already processing, skip this iteration
                return;
            }

            try
            {
                isProcessing = true;

                var transactions = await _repositoryManager.TransactionRepository.GetWatchableTransactions(true);
                var policyResult = PolicyChecker.CheckPolicy(new PolicyDto
                {
                    TransactionsWithAmountViolations = transactions.Where(x => !x.IsPolicyChecked && x.Amount > 5000000).Select(x => x.TransactionId).ToList(),
                    TransactionsWithNewUserViolations = transactions.Where(x => !x.IsPolicyChecked && x.Sender.CreatedAt < DateTime.Now.AddDays(1) || !x.IsPolicyChecked && x.Receiver.CreatedAt < DateTime.Now.AddDays(1)).Select(q => q.TransactionId).ToList(),
                    TransactionsWithFlaggedUserViolations = transactions.Where(x => !x.IsPolicyChecked && x.Sender.IsFlagged || !x.IsPolicyChecked && x.Receiver.IsFlagged).Select(q => q.TransactionId).ToList(),
                    TransactionsWithTierUserViolations = transactions.Where(x => !x.IsPolicyChecked && x.Amount > (decimal)x.Sender.Tier).Select(q => q.TransactionId).ToList(),
                    TransactionsWithIntervalViolations = TransactionHelper.GetIntervalsLessThanOneMinute(transactions, _repositoryManager),
                });

                if (policyResult)
                {
                    await _repositoryManager.SaveAsync();
                }
            }
            finally
            {
                isProcessing = false;
            }
        }

        public async Task StartTransactionWatcher(int interval)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<ITransactionService>();
                _timer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(interval));
            }
        }

        private async void TimerCallback(object state)
        {
            try
            {
                // Assuming _repositoryManager.TransactionRepository returns an ITransactionRepository
                //var transactionRepository = _repositoryManager.TransactionRepository;

                // Assuming transactionRepository.DbContext returns the DbContext
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<ITransactionService>();
                    await scopedService.TransactionWatcher();
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions appropriately
            }
        }


    }
}
