import { useContext, useEffect, useState } from "react";
import IssuesListSkeleton from "./IssuesListSkeleton";
import { getAllIssues } from "../../../../Api/IssueApi";
import { IssueGet } from "../../../../Models/Issue";
import { useAuth0 } from "@auth0/auth0-react";
import { ProjectContext } from "../../Context/Context";
import { Link } from "react-router-dom";

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
                  <th>Title</th>
                  <th>Assignee</th>
                  <th>Status</th>
                </tr>
              </thead>
              <tbody>
                {issues.map((issue) => (
                  <tr key={issue.key} className="hover">
                    <td>
                      <Link to={`${issue.key}`}>{issue.key}</Link>
                    </td>
                    <td>
                      <Link to={`${issue.key}`}>
                        <b>{issue.title}</b>
                      </Link>
                    </td>
                    <td>{issue.assignee.firstName}</td>
                    <td>{issue.status}</td>
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
