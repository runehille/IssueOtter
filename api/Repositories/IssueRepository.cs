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

  public async Task<IssueModel?> DeleteAsync(int id)
  {
    var issue = await _context.Issue.FirstOrDefaultAsync(x => x.Id == id);
    if (issue == null)
    {
      return null;
    }

    _context.Issue.Remove(issue);
    await _context.SaveChangesAsync();

    return issue;
  }

  public async Task<IssueModel?> DeleteByKeyAsync(string key)
  {
    var issue = await _context.Issue.FirstOrDefaultAsync(x => x.Key == key);
    if (issue == null)
    {
      return null;
    }

    _context.Issue.Remove(issue);
    await _context.SaveChangesAsync();

    return issue;
  }

  public async Task<List<IssueModel>> GetAllAsync()
  {
    var issues = await _context.Issue.ToListAsync();

    return issues;
  }

  public async Task<List<IssueModel>> GetAllByProjectIdAsync(int id)
  {
    var issues = await _context.Issue.Where(x => x.ProjectId == id).ToListAsync();

    return issues;
  }

  public async Task<List<IssueModel>> GetAllByProjectKeyAsync(string key)
  {
    var project = await _context.Project.FirstOrDefaultAsync(x => x.Key == key);

    if (project is null)
    {
      return new List<IssueModel>();
    }

    var issues = await _context.Issue.Include(x => x.Assignee).Where(x => x.ProjectId == project.Id).ToListAsync();

    return issues;
  }

  public async Task<IssueModel?> GetByIdAsync(int id)
  {
    var issue = await _context.Issue.FirstOrDefaultAsync(x => x.Id == id);

    return issue;
  }

  public async Task<IssueModel?> GetByKeyAsync(string key)
  {
    var issue = await _context.Issue.Include(x => x.Assignee).FirstOrDefaultAsync(x => x.Key == key);

    return issue;
  }

  public Task<IssueModel?> UpdateAsync(int id, IssueModel issueModel)
  {
    throw new NotImplementedException();
  }
}