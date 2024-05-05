using IssueOtter.Core.Dtos.Issue;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Mappers;

namespace IssueOtter.Core.Services.Issue;

public class IssueService : IIssueService
{
  private readonly IIssueRepository _issueRepository;
  private readonly ICommentRepository _commentRepository;
  private readonly IProjectRepository _projectRepository;
  private readonly IUserRepository _userRepository;

  public IssueService(IIssueRepository issueRepository, ICommentRepository commentRepository, IProjectRepository projectRepository, IUserRepository userRepository)
  {
    _issueRepository = issueRepository;
    _commentRepository = commentRepository;
    _projectRepository = projectRepository;
    _userRepository = userRepository;
  }

  public async Task<IssueResponse?> CreateIssueAsync(CreateIssueRequest createIssueRequest, string userAuthId)
  {
    var issueToCreate = createIssueRequest.MapCreateIssueRequestToIssueModel();
    var project = await _projectRepository.GetByKeyAsync(createIssueRequest.ProjectKey);
    var user = await _userRepository.GetByAuthId(userAuthId);

    if (user is null)
    {
      return null;
    }

    issueToCreate.ProjectId = project.Id;
    issueToCreate.CreatedById = user.Id;
    issueToCreate.AssigneeId = user.Id;
    issueToCreate.LastUpdatedById = user.Id;
    project.IssueCount++;
    issueToCreate.Key = $"{project.Key}-{project.IssueCount}";

    await _issueRepository.CreateAsync(issueToCreate);
    await _projectRepository.UpdateIssueCountAsync(project);

    var issue = await _issueRepository.GetByIdAsync(issueToCreate.Id);

    if (issue is null)
    {
      return null;
    }

    return issue.MapIssueModelToIssueResponse();
  }

  public async Task<IssueResponse?> DeleteIssueByIdAsync(int id)
  {
    var deletedIssue = await _issueRepository.DeleteAsync(id);

    if (deletedIssue is null)
    {
      return null;
    }

    return deletedIssue.MapIssueModelToIssueResponse();
  }

  public async Task<IssueResponse?> DeleteIssueByKeyAsync(string key)
  {
    var deletedIssue = await _issueRepository.DeleteByKeyAsync(key);

    if (deletedIssue is null)
    {
      return null;
    }

    return deletedIssue.MapIssueModelToIssueResponse();
  }

  public async Task<List<IssueResponse>> GetAllIssuesAsync()
  {
    var issues = await _issueRepository.GetAllAsync();

    var issuesResponse = issues.Select(x => x.MapIssueModelToIssueResponse()).ToList();

    return issuesResponse;
  }

  public async Task<IssueResponse?> GetIssueByIdAsync(int id)
  {
    var issue = await _issueRepository.GetByIdAsync(id);

    if (issue is null)
    {
      return null;
    }
    var issueResponse = issue.MapIssueModelToIssueResponse();

    return issueResponse;
  }

  public async Task<IssueResponse?> GetIssueByKeyAsync(string key)
  {
    var issue = await _issueRepository.GetByKeyAsync(key);

    if (issue is null)
    {
      return null;
    }
    var issueResponse = issue.MapIssueModelToIssueResponse();
    var commentModels = await _commentRepository.GetAllByIssueKeyAsync(key);
    issueResponse.Comments = commentModels.Select(x => x.MapCommentToCommentResponse()).ToList();

    return issueResponse;
  }

  public async Task<List<IssueResponse>> GetIssuesByProjectIdAsync(int id)
  {
    var issues = await _issueRepository.GetAllByProjectIdAsync(id);
    var issueResponses = issues.Select(x => x.MapIssueModelToIssueResponse()).ToList();

    return issueResponses;
  }

  public async Task<List<IssueResponse>> GetIssuesByProjectKeyAsync(string key)
  {
    var issues = await _issueRepository.GetAllByProjectKeyAsync(key);
    var issueResponses = issues.Select(x => x.MapIssueModelToIssueResponse()).ToList();

    return issueResponses;
  }
}
