using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Services.Issue;

public class IssueStatusService
{
    private static readonly Dictionary<IssueStatus, List<IssueStatus>> _allowedTransitions = new()
    {
        [IssueStatus.ToDo] = [IssueStatus.InProgress, IssueStatus.Closed],
        [IssueStatus.InProgress] = [IssueStatus.InReview, IssueStatus.ToDo, IssueStatus.Closed],
        [IssueStatus.InReview] = [IssueStatus.Done, IssueStatus.InProgress, IssueStatus.Closed],
        [IssueStatus.Done] = [IssueStatus.InReview, IssueStatus.Closed],
        [IssueStatus.Closed] = [IssueStatus.ToDo]
    };

    public static bool IsValidTransition(IssueStatus currentStatus, IssueStatus newStatus)
    {
        if (currentStatus == newStatus) return true;
        
        return _allowedTransitions.ContainsKey(currentStatus) &&
               _allowedTransitions[currentStatus].Contains(newStatus);
    }

    public static List<IssueStatus> GetAllowedTransitions(IssueStatus currentStatus)
    {
        return _allowedTransitions.ContainsKey(currentStatus) 
            ? _allowedTransitions[currentStatus] 
            : [];
    }

    public static string GetStatusTransitionError(IssueStatus currentStatus, IssueStatus newStatus)
    {
        return $"Invalid status transition from {currentStatus} to {newStatus}. " +
               $"Allowed transitions from {currentStatus}: {string.Join(", ", GetAllowedTransitions(currentStatus))}";
    }
}