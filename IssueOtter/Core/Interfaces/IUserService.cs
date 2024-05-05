using IssueOtter.Core.Dtos.User;

namespace IssueOtter.Core.Interfaces;

public interface IUserService
{
  Task<UserResponse?> CreateUserAync(CreateUserRequest createUserRequest, string userAuthId);
}
