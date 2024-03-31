import { useContext, useEffect, useState } from "react";
import IssuesListSkeleton from "./IssuesListSkeleton";
import { getAllIssues } from "../../../../Api/IssueApi";
import { IssueGet } from "../../../../Models/Issue";
import { useAuth0 } from "@auth0/auth0-react";
import { ProjectContext } from "../../Context/Context";

const IssuesList = () => {
  const projectKey = useContext(ProjectContext);
  const { isAuthenticated, getAccessTokenSilently } = useAuth0();
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
          <table className="table">
            {/* head */}
            <thead>
              <tr>
                <th>Key</th>
                <th>Title</th>
                <th>Content</th>
                <th>Assignee</th>
                <th>Status</th>
              </tr>
            </thead>
            <tbody>
              {issues.map((issue) => (
                <tr key={issue.key} className="hover">
                  <td>{issue.key}</td>
                  <td>{issue.title}</td>
                  <td>{issue.content}</td>
                  <td>{issue.assigneeId}</td>
                  <td>{issue.status}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </>
  );
};

export default IssuesList;
