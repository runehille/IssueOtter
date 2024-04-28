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

  public async Task<User> CreateAsync(User User)
  {
    await _context.User.AddAsync(User);

    await _context.SaveChangesAsync();

    return User;
  }

  public async Task<bool> Exists(string authId)
  {
    return await _context.User.AnyAsync(u => u.AuthId == authId);
  }

  public async Task<User?> GetByAuthId(string authId)
  {
    return await _context.User.FirstOrDefaultAsync(x => x.AuthId == authId);
  }
}