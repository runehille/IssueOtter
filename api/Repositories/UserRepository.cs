using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class UserRepository : IUserRepository
{
  private readonly ApplicationDbContext _context;

  public UserRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<UserModel> CreateAsync(UserModel userModel)
  {
    await _context.User.AddAsync(userModel);

    await _context.SaveChangesAsync();

    return userModel;
  }

  public async Task<bool> Exists(string authId)
  {
    return await _context.User.AnyAsync(u => u.AuthId == authId);
  }

  public async Task<UserModel?> GetByAuthId(string authId)
  {
    return await _context.User.FirstOrDefaultAsync(x => x.AuthId == authId);
  }
}