import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import { getIssueByKey } from "../../../../Api/IssueApi";
import { useNavigate } from "react-router-dom";
import IssueSkeleton from "./IssueSkeleton";
import { IssueGet } from "../../../../Models/Issue";
import { FaCaretLeft } from "react-icons/fa6";

type Props = {
  issueKey: string;
};

const Issue = ({ issueKey }: Props) => {
  const { getAccessTokenSilently } = useAuth0();
  const [issue, setIssue] = useState<IssueGet | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchIssue = async () => {
      const token = await getAccessTokenSilently();
      const issue = await getIssueByKey(token, issueKey);

      if (issue) {
        setIssue(issue.data);
        setIsLoading(false);
      }
    };
    fetchIssue();
  }, []);

  return (
    <>
      {isLoading ? (
        <IssueSkeleton />
      ) : (
        <>
          <button className="btn btn-sm btn-ghost" onClick={() => navigate(-1)}>
            <FaCaretLeft />
            Back
          </button>

          <div className="flex  lg:space-x-64">
            <div>
              <div className="px-4 sm:px-0">
                <h3 className="text-3xl font-semibold tracking-wider">
                  {issue?.title}
                </h3>
              </div>
              <div className="mt-6">{issue?.content}</div>
            </div>

            <div className="space-y-4">
              <div className="shadow p-8">
                <div className="px-4 sm:px-0">
                  <h3 className="text-base font-semibold">Information</h3>
                </div>
                <div className="mt-6 border-t border-gray-100">
                  <dl className="divide-y divide-gray-100">
                    <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
                      <dt className="text-sm">Assignee</dt>
                      <dd className="mt-1 text-sm  sm:col-span-2 sm:mt-0">
                        {issue?.assignee.email}
                      </dd>
                    </div>
                    <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
                      <dt className="text-sm font-medium">Status</dt>
                      <dd className="mt-1 text-sm col-span-2 sm:mt-0">
                        In Progress
                      </dd>
                    </div>
                  </dl>
                </div>
              </div>
              <div>
                <div className="text-sm">Created {issue?.createdOn}</div>
                <div className="text-sm">Updated {issue?.lastUpdatedOn}</div>
              </div>
            </div>
          </div>
        </>
      )}
    </>
  );
};

export default Issue;
