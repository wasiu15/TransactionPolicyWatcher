using PolicyWatcher.Domain.Dtos;
using PolicyWatcher.Infrastructure.Email;
using System.Text;

namespace PolicyWatcher.Infrastructure.Helpers
{
    public static class PolicyChecker
    {
        public static bool CheckPolicy(PolicyDto policyDto)
        {
            var finalMessage = "";
            if (policyDto.TransactionsWithAmountViolations.Any())
            {
                var message = "The following transactions violated the policy that says transaction amount should not be greater than 5,000,000 \n";
                var transactionIds = string.Join(" , ", policyDto.TransactionsWithAmountViolations) + "\n \n";
                finalMessage = finalMessage + message + transactionIds;
            }
            if (policyDto.TransactionsWithIntervalViolations.Any())
            {
                var message = "The following transactions violated the policy that says transaction interval should not be greater than one (1) minute \n";
                var transactionIdsBuilder = new StringBuilder("{ \n");

                foreach (var transactionDictionary in policyDto.TransactionsWithIntervalViolations)
                {
                    transactionIdsBuilder.AppendLine($"\tTransaction {transactionDictionary["previousTransaction"]} and Transaction {transactionDictionary["currentTransaction"]},");
                }

                transactionIdsBuilder.Append("} \n\n");
                finalMessage += message + transactionIdsBuilder.ToString();
            }
            if (policyDto.TransactionsWithNewUserViolations.Any())
            {
                var message = "The following transactions are attributed to new user \n";
                var transactionIds = string.Join(" , ", policyDto.TransactionsWithNewUserViolations) + "\n \n";
                finalMessage = finalMessage + message + transactionIds;
            }
            if (policyDto.TransactionsWithFlaggedUserViolations.Any())
            {
                var message = "The following transactions involved flagged user \n";
                var transactionIds = string.Join(" , ", policyDto.TransactionsWithFlaggedUserViolations) + "\n \n";
                finalMessage = finalMessage + message + transactionIds;
            }
            if (policyDto.TransactionsWithTierUserViolations.Any())
            {
                var message = "The following transactions violated the policy that says transaction amount should not be greater than the amount for a given tier \n";
                var transactionIds = string.Join(" , ", policyDto.TransactionsWithTierUserViolations) + "\n \n";
                finalMessage = finalMessage + message + transactionIds;
            }

            // EMAIL SENDER
            if (finalMessage != "")
            {
                finalMessage = finalMessage + "\n \n \n Best Regards \n Abiola";
                var result = EmailSender.SendEmail(finalMessage);
                Console.WriteLine("Email sent status is " + result + "\nMessage:" + finalMessage);
                return result;
            }
            else
            {
                Console.WriteLine("Nothing to send!");
            }
            return false;
        }
    }
}
