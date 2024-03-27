import { useEffect, useState } from "react";
import IssuesListSkeleton from "./IssuesListSkeleton";
import { getAllIssues } from "../../Api";
import { IssueGet } from "../../Models/Issue";
import { useAuth0 } from "@auth0/auth0-react";

const IssuesList = () => {
  const { isAuthenticated, getAccessTokenSilently } = useAuth0();
  const [isLoading, setIsLoading] = useState(true);
  const [issues, setIssues] = useState<IssueGet[]>([]);

  useEffect(() => {
    const fetchIssues = async () => {
      if (isAuthenticated) {
        const token = await getAccessTokenSilently();
        const result = await getAllIssues(token);
        if (result) {
          setIssues(result.data);
          setIsLoading(false);
        }
      }
    };
    fetchIssues();
  }, [getAccessTokenSilently, isAuthenticated]);

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
                <th>Assignee</th>
                <th>Status</th>
              </tr>
            </thead>
            <tbody>
              {issues.map((issue) => (
                <tr key={issue.key} className="hover">
                  <td>{issue.key}</td>
                  <td>{issue.title}</td>
                  <td>{issue.assignee}</td>
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
