using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class CommentRepository : ICommentRepository
{
  private readonly ApplicationDbContext _context;

  public CommentRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<CommentModel?> CreateAsync(CommentModel comment)
  {
    await _context.Comment.AddAsync(comment);

    await _context.SaveChangesAsync();

    return comment;
  }

  public Task<CommentModel?> DeleteAsync(int id)
  {
    throw new NotImplementedException();
  }

  public async Task<List<CommentModel>> GetAllByIssueKeyAsync(string key)
  {
    var issue = await _context.Issue.FirstOrDefaultAsync(x => x.Key == key);

    if (issue is null)
    {
      return new List<CommentModel>();
    }

    var comments = await _context.Comment.Include(comment => comment.CreatedBy).Where(x => x.IssueId == issue.Id).ToListAsync();

    return comments;
  }
}
