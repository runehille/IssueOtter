import { useContext, useEffect, useState } from "react";
import IssuesListSkeleton from "./IssuesListSkeleton";
import { getAllIssues } from "../../../../Api/IssueApi";
import { IssueGet, IssueStatus, IssuePriority } from "../../../../Models/Issue";
import { useAuth } from "../../../../hooks/useAuth";
import { ProjectContext } from "../../Context/Context";
import { Link } from "react-router-dom";
import LabelBadge from "../../../../Components/Label/LabelBadge";

const IssuesList = () => {
  const projectKey = useContext(ProjectContext);
  const { isAuthenticated, getAccessTokenSilently } = useAuth();
  const [isLoading, setIsLoading] = useState(true);
  const [issues, setIssues] = useState<IssueGet[]>([]);

  useEffect(() => {
    const fetchIssues = async () => {
      const token = await getAccessTokenSilently();
      const result = await getAllIssues(token, projectKey);
      if (result) {
        setIssues(result.data);
        setIsLoading(false);
      }
    };
    fetchIssues();
  }, [isAuthenticated]);

  return (
    <>
      {isLoading ? (
        <IssuesListSkeleton />
      ) : (
        <div className="overflow-x-auto">
          {issues.length === 0 ? (
            <>
              <p>There are no issues here.</p>
              <p>
                Get started by clicking the <b>Create Issue</b> button
              </p>
            </>
          ) : (
            <table className="table">
              <thead>
                <tr>
                  <th>Key</th>
                  <th>Type</th>
                  <th>Priority</th>
                  <th>Status</th>
                  <th>Title</th>
                  <th>Labels</th>
                  <th>Assignee</th>
                  <th>Created</th>
                </tr>
              </thead>
              <tbody>
                {issues.map((issue) => (
                  <tr key={issue.key} className="hover">
                    <td>
                      <Link to={`${issue.key}`}>{issue.key}</Link>
                    </td>
                    <td>{issue.type}</td>
                    <td>
                      <p
                        className={`${
                          issue.priority === IssuePriority.Low
                            ? "badge badge-ghost"
                            : issue.priority === IssuePriority.Medium
                            ? "badge badge-info"
                            : issue.priority === IssuePriority.High
                            ? "badge badge-warning"
                            : issue.priority === IssuePriority.Critical
                            ? "badge badge-error"
                            : ""
                        } font-semibold`}
                      >
                        {issue.priority === IssuePriority.Low ? "Low" :
                         issue.priority === IssuePriority.Medium ? "Medium" :
                         issue.priority === IssuePriority.High ? "High" :
                         issue.priority === IssuePriority.Critical ? "Critical" : ""}
                      </p>
                    </td>
                    <td>
                      <p
                        className={`${
                          issue.status === IssueStatus.ToDo
                            ? "btn"
                            : issue.status === IssueStatus.InProgress
                            ? "btn btn-info"
                            : issue.status === IssueStatus.Done
                            ? "btn btn-success"
                            : ""
                        } rounded-lg py-2 px-3 text-center font-semibold min-w-28`}
                      >
                        {issue.status}
                      </p>
                    </td>
                    <td>
                      <Link to={`${issue.key}`}>
                        <b>{issue.title}</b>
                      </Link>
                    </td>
                    <td>
                      <div className="flex flex-wrap gap-1">
                        {issue.labels?.map((label) => (
                          <LabelBadge key={label.id} label={label} size="sm" />
                        ))}
                        {(!issue.labels || issue.labels.length === 0) && (
                          <span className="text-gray-400 text-sm">No labels</span>
                        )}
                      </div>
                    </td>
                    <td>{issue.assignee?.firstName || 'Unassigned'}</td>
                    <td>{issue.createdOn}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </div>
      )}
    </>
  );
};

export default IssuesList;
