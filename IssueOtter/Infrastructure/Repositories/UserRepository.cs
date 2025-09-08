using IssueOtter.Core.Entities;
using IssueOtter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IssueOtter.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        try
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            // TODO Handle exception.
        }

        return user;
    }

    public async Task<bool> Exists(string authId)
    {
        return await _context.User.AnyAsync(u => u.AuthId == authId);
    }

    public async Task<User?> GetByAuthId(string authId)
    {
        return await _context.User.FirstOrDefaultAsync(x => x.AuthId == authId);
    }

    public async Task<User?> UpdateAsync(User user)
    {
        try
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (DbUpdateException)
        {
            // TODO Handle exception.
            return null;
        }
    }
}