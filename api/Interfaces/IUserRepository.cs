using api.Models;

namespace api.Interfaces;

public interface IUserRepository
{
  Task<UserModel?> GetByAuthId(string authId);
  Task<bool> Exists(string authId);
  Task<UserModel> CreateAsync(UserModel userModel);
}