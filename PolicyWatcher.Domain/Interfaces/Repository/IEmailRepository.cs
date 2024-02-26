using PolicyWatcher.Domain.Models;

namespace PolicyWatcher.Domain.Interfaces.Repository
{
    public interface IEmailRepository
    {
        void CreateEmail(Email email);
        void DeleteEmail(Email email);
        void UpdateEmail(Email email);
        Task<IEnumerable<Email>> GetEmails(bool trackChanges);
        Task<Email> GetEmailId(string emailId, bool trackChanges);
    }
}
