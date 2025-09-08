using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Interfaces;
public interface IUserRepository
{
  Task<User?> GetByAuthId(string authId);
  Task<bool> Exists(string authId);
  Task<User> CreateAsync(User User);
  Task<User?> UpdateAsync(User user);
}