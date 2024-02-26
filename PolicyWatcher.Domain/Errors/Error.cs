using PolicyWatcher.Domain.Dtos.Response;
using PolicyWatcher.Domain.Models;

namespace PolicyWatcher.Domain.Errors
{
    public record Error(string code, string description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");

        public static implicit operator GenericResponse<UserResponseDto>(Error error) => new GenericResponse<UserResponseDto> { 
            ResponseCode = error.code,
            IsSuccessful = false,
            ResponseMessage = error.description,
            Data = null 
        };

        public static implicit operator GenericResponse<User>(Error error) => new GenericResponse<User>
        {
            ResponseCode = error.code,
            IsSuccessful = false,
            ResponseMessage = error.description,
            Data = null
        };

        public static implicit operator GenericResponse<Transaction>(Error error) => new GenericResponse<Transaction>
        {
            ResponseCode = error.code,
            IsSuccessful = false,
            ResponseMessage = error.description,
            Data = null
        };
    }
}
                                                                                                                                                                            