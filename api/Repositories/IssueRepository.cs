using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class IssueRepository : IIssueRepository
{
    private readonly ApplicationDbContext _context;

    public IssueRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Issue?> CreateAsync(Issue issueModel)
    {
        throw new NotImplementedException();
    }

    public Task<Issue?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Issue>> GetAllAsync()
    {
        var issues = await _context.Issue!.ToListAsync();

        return issues;
    }

    public Task<Issue?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Issue?> UpdateAsync(int id, Issue issueModel)
    {
        throw new NotImplementedException();
    }
}