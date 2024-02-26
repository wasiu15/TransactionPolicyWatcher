using PolicyWatcher.Domain.Models;

namespace PolicyWatcher.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        Task<IEnumerable<User>> GetUsers(bool trackChanges);
        Task<User> GetUserById(int userId, bool trackChanges);
        Task<List<User>> GetSenderAndReceiver(int senderId, int receiverId, bool trackChanges);
        
    }
}
