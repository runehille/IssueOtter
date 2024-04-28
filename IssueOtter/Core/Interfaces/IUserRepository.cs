using api.Models;

namespace api.Interfaces;

public interface IUserRepository
{
  Task<User?> GetByAuthId(string authId);
  Task<bool> Exists(string authId);
  Task<User> CreateAsync(User User);
}