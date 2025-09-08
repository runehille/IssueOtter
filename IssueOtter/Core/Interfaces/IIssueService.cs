using IssueOtter.Core.Dtos.Issue;

namespace IssueOtter.Core.Interfaces;

public interface IIssueService
{
  Task<List<IssueResponse>> GetAllIssuesAsync();
  Task<IssueResponse?> GetIssueByIdAsync(int id);
  Task<IssueResponse?> GetIssueByKeyAsync(string key);
  Task<List<IssueResponse>> GetIssuesByProjectIdAsync(int id);
  Task<List<IssueResponse>> GetIssuesByProjectKeyAsync(string key);
  Task<IssueResponse?> CreateIssueAsync(CreateIssueRequest createIssueRequest, string userAuthId);
  Task<IssueResponse?> UpdateIssueAsync(int id, UpdateIssueRequest updateIssueRequest);
  Task<IssueResponse?> UpdateIssueStatusAsync(int id, UpdateIssueStatusRequest updateStatusRequest, string userAuthId);
  Task<IssueResponse?> UpdateIssueStatusByKeyAsync(string key, UpdateIssueStatusRequest updateStatusRequest, string userAuthId);
  Task<IssueResponse?> DeleteIssueByIdAsync(int id);
  Task<IssueResponse?> DeleteIssueByKeyAsync(string key);
}