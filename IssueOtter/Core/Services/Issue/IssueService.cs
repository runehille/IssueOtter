using IssueOtter.Core.Dtos.Issue;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Mappers;
using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Services.Issue;

public class IssueService(
    IIssueRepository issueRepository,
    ICommentRepository commentRepository,
    IProjectRepository projectRepository,
    IUserRepository userRepository,
    ILabelRepository labelRepository)
    : IIssueService
{
    public async Task<IssueResponse?> CreateIssueAsync(CreateIssueRequest createIssueRequest, string userAuthId)
    {
        var issueToCreate = createIssueRequest.MapCreateIssueRequestToIssueModel();
        var project = await projectRepository.GetByKeyAsync(createIssueRequest.ProjectKey);
        var user = await userRepository.GetByAuthId(userAuthId);

        if (user is null) return null;

        issueToCreate.ProjectId = project.Id;
        issueToCreate.CreatedById = user.Id;
        issueToCreate.AssigneeId = user.Id;
        issueToCreate.LastUpdatedById = user.Id;
        project.IssueCount++;
        issueToCreate.Key = $"{project.Key}-{project.IssueCount}";

        // Handle labels if provided
        if (createIssueRequest.LabelIds?.Any() == true)
        {
            var labels = await labelRepository.GetByIdsAsync(createIssueRequest.LabelIds);
            issueToCreate.Labels = labels.ToList();
        }

        await issueRepository.CreateAsync(issueToCreate);
        await projectRepository.UpdateIssueCountAsync(project);

        var issue = await issueRepository.GetByIdAsync(issueToCreate.Id);

        return issue?.MapIssueModelToIssueResponse();
    }

    public async Task<IssueResponse?> DeleteIssueByIdAsync(int id)
    {
        var deletedIssue = await issueRepository.DeleteAsync(id);

        return deletedIssue?.MapIssueModelToIssueResponse();
    }

    public async Task<IssueResponse?> DeleteIssueByKeyAsync(string key)
    {
        var deletedIssue = await issueRepository.DeleteByKeyAsync(key);

        return deletedIssue?.MapIssueModelToIssueResponse();
    }

    public async Task<List<IssueResponse>> GetAllIssuesAsync()
    {
        var issues = await issueRepository.GetAllAsync();

        var issuesResponse = issues.Select(x => x.MapIssueModelToIssueResponse()).ToList();

        return issuesResponse;
    }

    public async Task<IssueResponse?> GetIssueByIdAsync(int id)
    {
        var issue = await issueRepository.GetByIdAsync(id);

        var issueResponse = issue?.MapIssueModelToIssueResponse();

        return issueResponse;
    }

    public async Task<IssueResponse?> GetIssueByKeyAsync(string key)
    {
        var issue = await issueRepository.GetByKeyAsync(key);

        if (issue is null) return null;
        var issueResponse = issue.MapIssueModelToIssueResponse();
        var commentModels = await commentRepository.GetAllByIssueKeyAsync(key);
        issueResponse.Comments = commentModels.Select(x => x.MapCommentToCommentResponse()).ToList();

        return issueResponse;
    }

    public async Task<IssueResponse?> UpdateIssueAsync(int id, UpdateIssueRequest updateIssueRequest)
    {
        var issueToUpdate = updateIssueRequest.MapUpdateIssueRequestToIssueModel();
        var updatedIssue = await issueRepository.UpdateAsync(id, issueToUpdate);

        return updatedIssue?.MapIssueModelToIssueResponse();
    }

    public async Task<List<IssueResponse>> GetIssuesByProjectIdAsync(int id)
    {
        var issues = await issueRepository.GetAllByProjectIdAsync(id);
        var issueResponses = issues.Select(x => x.MapIssueModelToIssueResponse()).ToList();

        return issueResponses;
    }

    public async Task<List<IssueResponse>> GetIssuesByProjectKeyAsync(string key)
    {
        var issues = await issueRepository.GetAllByProjectKeyAsync(key);
        var issueResponses = issues.Select(x => x.MapIssueModelToIssueResponse()).ToList();

        return issueResponses;
    }

    public async Task<IssueResponse?> UpdateIssueStatusAsync(int id, UpdateIssueStatusRequest updateStatusRequest, string userAuthId)
    {
        var user = await userRepository.GetByAuthId(userAuthId);
        if (user is null) return null;

        var existingIssue = await issueRepository.GetByIdAsync(id);
        if (existingIssue is null) return null;

        if (!IssueStatusService.IsValidTransition(existingIssue.Status, updateStatusRequest.Status))
        {
            throw new InvalidOperationException(
                IssueStatusService.GetStatusTransitionError(existingIssue.Status, updateStatusRequest.Status));
        }

        var updatedIssue = await issueRepository.UpdateStatusAsync(id, updateStatusRequest.Status, user.Id);

        return updatedIssue?.MapIssueModelToIssueResponse();
    }

    public async Task<IssueResponse?> UpdateIssueStatusByKeyAsync(string key, UpdateIssueStatusRequest updateStatusRequest, string userAuthId)
    {
        var user = await userRepository.GetByAuthId(userAuthId);
        if (user is null) return null;

        var existingIssue = await issueRepository.GetByKeyAsync(key);
        if (existingIssue is null) return null;

        if (!IssueStatusService.IsValidTransition(existingIssue.Status, updateStatusRequest.Status))
        {
            throw new InvalidOperationException(
                IssueStatusService.GetStatusTransitionError(existingIssue.Status, updateStatusRequest.Status));
        }

        var updatedIssue = await issueRepository.UpdateStatusByKeyAsync(key, updateStatusRequest.Status, user.Id);

        return updatedIssue?.MapIssueModelToIssueResponse();
    }
}