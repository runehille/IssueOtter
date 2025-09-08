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

  public async Task<Comment?> UpdateAsync(int id, Comment comment)
  {
    var commentToUpdate = await _context.Comment.FindAsync(id);
    if (commentToUpdate is null)
      return null;
    
    commentToUpdate.Content = comment.Content;
    commentToUpdate.LastUpdatedOn = DateTime.Now;
    commentToUpdate.IsEdited = true;
    
    await _context.SaveChangesAsync();
    return commentToUpdate;
  }

  public async Task<Comment?> DeleteAsync(int id)
  {
    var commentToDelete = await _context.Comment.FindAsync(id);
    if (commentToDelete is null)
      return null;
    
    _context.Comment.Remove(commentToDelete);
    await _context.SaveChangesAsync();
    return commentToDelete;
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
