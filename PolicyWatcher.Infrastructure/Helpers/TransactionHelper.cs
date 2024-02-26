using PolicyWatcher.Domain.Interfaces.Repository;
using PolicyWatcher.Domain.Models;

namespace PolicyWatcher.Infrastructure.Helpers
{
    public static class TransactionHelper
    {
        public static List<Dictionary<string, int>> GetIntervalsLessThanOneMinute(List<Transaction> totalTransactionsList, IRepositoryManager repositoryManager)
        {
            List<Dictionary<string, int>> transactions = new List<Dictionary<string, int>>();
            
            totalTransactionsList.Sort((t1, t2) => t1.CreatedAt.CompareTo(t2.CreatedAt));

            var groupTransactionBySender = totalTransactionsList.GroupBy(x => x.SenderId).ToList();

            groupTransactionBySender.ForEach(transactionsForEachUser =>
            {
                var transactionsList = transactionsForEachUser.OrderBy(t => t.CreatedAt).ToList();

                //  check if the transaction is not up to two so we can update the policy checked status because only one element wont enter the loop as it is starting from number 1
                if(transactionsList.Count == 1)
                    transactionsList[0].IsPolicyChecked = true;

                for (int i = 1; i < transactionsList.Count; i++)
                {
                    var currentTransaction = transactionsList[i];
                    var previousTransaction = transactionsList[i - 1];

                    if ((currentTransaction.CreatedAt - previousTransaction.CreatedAt) < TimeSpan.FromMinutes(1))
                    {
                        var previousAndCurrentTransaction = new Dictionary<string, int>
                        {
                            { "previousTransaction", previousTransaction.TransactionId },
                            { "currentTransaction", currentTransaction.TransactionId }
                        };
                        transactions.Add(previousAndCurrentTransaction);
                    }


                    //  this is to ensure that the last element is not updated so that it can be compared with the first transaction of the next batch for each user
                    transactionsList[i-1].IsIntervalChecked = true;//do this for the previous last element
                    transactionsList[i].IsIntervalChecked = true;
                    transactionsList[i].IsPolicyChecked = true;
                    if(i+1 == transactionsList.Count)
                    {
                        transactionsList[i].IsIntervalChecked = false;
                    }
                }
                repositoryManager.TransactionRepository.UpdateBulkTransaction(transactionsList);
            });

            return transactions;

        }
    }
}
