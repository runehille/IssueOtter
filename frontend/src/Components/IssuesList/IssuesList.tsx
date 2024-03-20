import { useEffect, useState } from "react";
import IssuesListSkeleton from "./IssuesListSkeleton";
import { getAllIssues } from "../../Api";
import { IssueGet } from "../../Models/Issue";

const IssuesList = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [issues, setIssues] = useState<IssueGet[]>([]);

  useEffect(() => {
    const fetchIssues = async () => {
      const result = await getAllIssues();
      if (result) {
        setIssues(result.data);
        setIsLoading(false);
      }
    };
    fetchIssues();
  }, []);

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
                <tr key={issue.id} className="hover">
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
