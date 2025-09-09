using IssueOtter.Core.Entities;
using IssueOtter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IssueOtter.Infrastructure.Repositories;

public class LabelRepository(ApplicationDbContext context) : ILabelRepository
{
    public async Task<List<Label>> GetAllAsync()
    {
        return await context.Label
            .Include(l => l.CreatedBy)
            .Include(l => l.Project)
            .ToListAsync();
    }

    public async Task<List<Label>> GetAllByProjectIdAsync(int projectId)
    {
        return await context.Label
            .Where(l => l.ProjectId == projectId)
            .Include(l => l.CreatedBy)
            .Include(l => l.Project)
            .ToListAsync();
    }

    public async Task<Label?> GetByIdAsync(int id)
    {
        return await context.Label
            .Include(l => l.CreatedBy)
            .Include(l => l.Project)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List<Label>> GetByIdsAsync(List<int> ids)
    {
        return await context.Label
            .Where(l => ids.Contains(l.Id))
            .Include(l => l.CreatedBy)
            .Include(l => l.Project)
            .ToListAsync();
    }

    public async Task CreateAsync(Label label)
    {
        context.Label.Add(label);
        await context.SaveChangesAsync();
    }

    public async Task<Label?> UpdateAsync(int id, Label label)
    {
        var existingLabel = await context.Label.FirstOrDefaultAsync(l => l.Id == id);

        if (existingLabel != null)
        {
            existingLabel.Name = label.Name;
            existingLabel.Color = label.Color;
            existingLabel.Description = label.Description;

            await context.SaveChangesAsync();
        }

        return existingLabel;
    }

    public async Task<Label?> DeleteAsync(int id)
    {
        var label = await context.Label.FirstOrDefaultAsync(l => l.Id == id);

        if (label != null)
        {
            context.Label.Remove(label);
            await context.SaveChangesAsync();
        }

        return label;
    }
}