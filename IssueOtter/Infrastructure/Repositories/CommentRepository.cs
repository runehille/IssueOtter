using IssueOtter.Core.Entities;
using IssueOtter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IssueOtter.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
  private readonly ApplicationDbContext _context;

  public CommentRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Comment?> CreateAsync(Comment comment)
  {
    await _context.Comment.AddAsync(comment);

    await _context.SaveChangesAsync();

    return comment;
  }

  public Task<Comment?> DeleteAsync(int id)
  {
    throw new NotImplementedException();
  }

  public async Task<List<Comment>> GetAllByIssueKeyAsync(string key)
  {
    var issue = await _context.Issue.FirstOrDefaultAsync(x => x.Key == key);

    if (issue is null)
    {
      return new List<Comment>();
    }

    var comments = await _context.Comment.Include(comment => comment.CreatedBy).Where(x => x.IssueId == issue.Id).ToListAsync();

    return comments;
  }
}
