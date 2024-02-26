using Microsoft.Extensions.Configuration;
using PolicyWatcher.Domain.Dtos.Request;
using PolicyWatcher.Domain.Dtos.Response;
using PolicyWatcher.Domain.Enums;
using PolicyWatcher.Domain.Errors;
using PolicyWatcher.Domain.Interfaces.Repository;
using PolicyWatcher.Domain.Interfaces.Service;
using PolicyWatcher.Domain.Models;

namespace PolicyWatcher.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _configuration;

        public UserService(IRepositoryManager repositoryManager, IConfiguration configuration)
        {
            _repositoryManager = repositoryManager;
            _configuration = configuration;
        }
        public async Task<GenericResponse<UserResponseDto>> CreateUser(UserRequestDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.FullName)) return UserErrors.EmptyName;

            if (!Enum.IsDefined(typeof(TierLevel), userDto.Tier)) return UserErrors.InvalidTier;
            //
            var getTierLimitsValues = _configuration.GetSection("Tier").GetChildren().Select(x => decimal.Parse(x.Value)).ToList();
            var user = new User
            {
                FullName = userDto.FullName,
                Tier = getTierLimitsValues[(int)userDto.Tier],
                CreatedAt = DateTime.Now
            };
            _repositoryManager.UserRepository.CreateUser(user);
            await _repositoryManager.SaveAsync();
            
            return new GenericResponse<UserResponseDto> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = $"User with ID = {user.UserId} was created successful", Data = null };
        }

        public async Task<GenericResponse<UserResponseDto>> DeleteUser(int userId)
        {
            if (userId < 0) return UserErrors.UserNotFound();

            var user = await _repositoryManager.UserRepository.GetUserById(userId, true);
            if (user == null)
                return UserErrors.UserNotFound();

            _repositoryManager.UserRepository.DeleteUser(user);
            await _repositoryManager.SaveAsync();

            return new GenericResponse<UserResponseDto> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = $"User with ID = {userId} was deleted successful", Data = null };
        }

        public async Task<GenericResponse<User>> GetUserById(int userId, bool trackChanges)
        {
            if (userId < 0) return UserErrors.UserNotFound();

            var user = await _repositoryManager.UserRepository.GetUserById(userId, trackChanges);
            return new GenericResponse<User> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = "User fetched successful", Data = user };
        }

        public async Task<GenericResponse<IEnumerable<User>>> GetUsers()
        {
            var users = await _repositoryManager.UserRepository.GetUsers(false);
            return new GenericResponse<IEnumerable<User>> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = users.Count() > 1 ? "Users fetched successful" : "No user found", Data = users };
        }

        public async Task<GenericResponse<UserResponseDto>> UserFlagger(int userId, bool flag)
        {
            var user = await GetUserById(userId, true);
            user.Data.IsFlagged = flag;
            _repositoryManager.UserRepository.UpdateUser(user.Data);
            await _repositoryManager.SaveAsync();

            return new GenericResponse<UserResponseDto> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = $"User with ID = {userId} has been {(flag ? "flagged" : "unflagged")} successfully.", Data = null };
        }
        public async Task<GenericResponse<UserResponseDto>> BackDateUserCreationDate(int userId, int numberOfDays)
        {
            var response = await GetUserById(userId, true);
            var user = response.Data;
            var updatedDate = user.CreatedAt.AddDays(-numberOfDays);
            user.CreatedAt = updatedDate;
            _repositoryManager.UserRepository.UpdateUser(user);
            await _repositoryManager.SaveAsync();

            return new GenericResponse<UserResponseDto> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = $"User creation date has been updated successful. The new date is = {user.CreatedAt}", Data = null };
        }
    }

}
