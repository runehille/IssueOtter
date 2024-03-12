import { useEffect, useState } from "react";
import IssuesListSkeleton from "./IssuesListSkeleton";

const IssuesList = () => {
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    setInterval(() => {
      setIsLoading(false);
    }, 1000);
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
                <th></th>
                <th>Title</th>
                <th>Status</th>
                <th>Assignee</th>
              </tr>
            </thead>
            <tbody>
              {/* row 1 */}
              <tr className="hover">
                <th>1</th>
                <td>Fix bug in backend</td>
                <td>In Progress</td>
                <td>Cy Ganderton</td>
              </tr>
              {/* row 2 */}
              <tr className="hover">
                <th>2</th>
                <td>Review code</td>
                <td>To Do</td>
                <td>Hart Hagerty</td>
              </tr>
              {/* row 3 */}
              <tr className="hover">
                <th>3</th>
                <td>Change layout on Dashboard</td>
                <td>In Progress</td>
                <td>Brice Swyre</td>
              </tr>
            </tbody>
          </table>
        </div>
      )}
    </>
  );
};

export default IssuesList;
