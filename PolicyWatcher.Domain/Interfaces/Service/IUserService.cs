using PolicyWatcher.Domain.Dtos.Request;
using PolicyWatcher.Domain.Dtos.Response;
using PolicyWatcher.Domain.Models;
namespace PolicyWatcher.Domain.Interfaces.Service
{
    public interface IUserService
    {
        Task<GenericResponse<IEnumerable<User>>> GetUsers();
        Task<GenericResponse<UserResponseDto>> CreateUser(UserRequestDto user);
        Task<GenericResponse<User>> GetUserById(int userId, bool trackChanges = false);
        Task<GenericResponse<UserResponseDto>> DeleteUser(int userId);
        Task<GenericResponse<UserResponseDto>> UserFlagger(int userId, bool flag);
        Task<GenericResponse<UserResponseDto>> BackDateUserCreationDate(int userId, int numberOfDays);
    }
}
