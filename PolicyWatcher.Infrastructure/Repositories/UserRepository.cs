using Microsoft.EntityFrameworkCore;
using PolicyWatcher.Domain.Interfaces.Repository;
using PolicyWatcher.Domain.Models;
using PolicyWatcher.Infrastructure.Data;

namespace PolicyWatcher.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(PolicyWatcherDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateUser(User user) => Create(user);
        public void DeleteUser(User user) => Delete(user);
        public void UpdateUser(User user) => Update(user);
        public async Task<IEnumerable<User>> GetUsers(bool trackChanges) => await FindAll(trackChanges).ToListAsync();
        public async Task<User> GetUserById(int userId, bool trackChanges) => await FindByCondition(x => x.UserId.Equals(userId), trackChanges).FirstOrDefaultAsync();
        public async Task<List<User>> GetSenderAndReceiver(int senderId, int receiverId, bool trackChanges) => await FindByCondition(x => x.UserId.Equals(senderId) || x.UserId.Equals(receiverId), trackChanges).ToListAsync();
    }
}
