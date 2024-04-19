import { useContext, useEffect, useState } from "react";
import BoardSkeleton from "./BoardSkeleton";
import { useAuth0 } from "@auth0/auth0-react";
import { getAllIssues } from "../../../../Api/IssueApi";
import { ProjectContext } from "../../Context/Context";
import { IssueGet } from "../../../../Models/Issue";

const Board = () => {
  const projectKey = useContext(ProjectContext);
  const [isLoading, setIsLoading] = useState(false);
  const [issues, setIssues] = useState<IssueGet[]>([]);
  const { getAccessTokenSilently } = useAuth0();

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
  }, []);

  return (
    <>
      {isLoading ? (
        <BoardSkeleton />
      ) : (
        <div className="flex flex-wrap justify-between">
          <div className="flex flex-wrap">
            <div className="bg-base-300 m-2 p-2 rounded-xl min-w-80">
              <p className="font-bold text-xl text-center m-2">To Do</p>
              {issues
                .filter((issue) => issue.status === "To Do")
                .map((issue) => (
                  <div
                    key={issue.key}
                    className="card w-80 bg-base-100 shadow-xl mb-2"
                  >
                    <div className="card-body">
                      <h2 className="">{issue.title}</h2>
                    </div>
                  </div>
                ))}
            </div>
          </div>
          <div className="flex flex-wrap">
            <div className="bg-base-300 m-2 p-2 rounded-xl min-w-80">
              <p className="font-bold text-xl text-center m-2">In Progress</p>
              {issues
                .filter((issue) => issue.status === "In Progress")
                .map((issue) => (
                  <div
                    key={issue.key}
                    className="card w-80 bg-base-100 shadow-xl mb-2"
                  >
                    <div className="card-body">
                      <h2 className="">{issue.title}</h2>
                    </div>
                  </div>
                ))}
            </div>
          </div>
          <div className="flex flex-wrap">
            <div className="bg-base-300 m-2 p-2 rounded-xl min-w-80">
              <p className="font-bold text-xl text-center m-2">Done</p>
              {issues
                .filter((issue) => issue.status === "Done")
                .map((issue) => (
                  <div
                    key={issue.key}
                    className="card w-80 bg-base-100 shadow-xl mb-2"
                  >
                    <div className="card-body">
                      <h2 className="">{issue.title}</h2>
                    </div>
                  </div>
                ))}
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default Board;
