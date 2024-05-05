using IssueOtter.Core.Entities;
using IssueOtter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IssueOtter.Infrastructure.Repositories;

public class IssueRepository : IIssueRepository
{
  private readonly ApplicationDbContext _context;
  private readonly ILogger<IssueRepository> _logger;

  public IssueRepository(ApplicationDbContext context, ILogger<IssueRepository> logger)
  {
    _context = context;
    _logger = logger;
  }

  public async Task<Issue?> CreateAsync(Issue Issue)
  {
    try
    {
      await _context.Issue!.AddAsync(Issue);
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateException e)
    {
      _logger.LogError(e, "An error occurred while creating the issue in the database.");
      throw;
    }

    return Issue;
  }

  public async Task<Issue?> DeleteAsync(int id)
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

  public async Task<Issue?> DeleteByKeyAsync(string key)
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

  public async Task<List<Issue>> GetAllAsync()
  {
    var issues = await _context.Issue.ToListAsync();

    return issues;
  }

  public async Task<List<Issue>> GetAllByProjectIdAsync(int id)
  {
    var issues = await _context.Issue.Where(x => x.ProjectId == id).ToListAsync();

    return issues;
  }

  public async Task<List<Issue>> GetAllByProjectKeyAsync(string key)
  {
    var project = await _context.Project.FirstOrDefaultAsync(x => x.Key == key);

    if (project is null)
    {
      return new List<Issue>();
    }

    var issues = await _context.Issue.Include(x => x.Assignee).Where(x => x.ProjectId == project.Id).ToListAsync();

    return issues;
  }

  public async Task<Issue?> GetByIdAsync(int id)
  {
    var issue = await _context.Issue.FirstOrDefaultAsync(x => x.Id == id);

    return issue;
  }

  public async Task<Issue?> GetByKeyAsync(string key)
  {
    var issue = await _context.Issue.Include(x => x.Assignee).Include(x => x.Comments).FirstOrDefaultAsync(x => x.Key == key);

    return issue;
  }

  public Task<Issue?> UpdateAsync(int id, Issue Issue)
  {
    throw new NotImplementedException();
  }
}