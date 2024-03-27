using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class IssueRepository : IIssueRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<IssueRepository> _logger;

    public IssueRepository(ApplicationDbContext context, ILogger<IssueRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IssueModel?> CreateAsync(IssueModel issueModel)
    {
        try
        {
            await _context.Issue!.AddAsync(issueModel);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "An error occurred while creating the issue in the database.");
            throw;
        }

        return issueModel;
    }

    public Task<IssueModel?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<IssueModel>> GetAllAsync()
    {
        var issues = await _context.Issue.Include(i => i.Comments).ToListAsync();

        return issues;
    }

    public Task<IssueModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IssueModel?> UpdateAsync(int id, IssueModel issueModel)
    {
        throw new NotImplementedException();
    }
}