import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import { deleteIssue, getIssueByKey } from "../../../../Api/IssueApi";
import { useNavigate } from "react-router-dom";
import IssueSkeleton from "./IssueSkeleton";
import { IssueGet } from "../../../../Models/Issue";
import { FaCaretLeft, FaEllipsis } from "react-icons/fa6";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as Yup from "yup";
import { postComment } from "../../../../Api/CommentApi";

type Props = {
  issueKey: string;
};

type CreateFormsInputs = {
  comment: string;
};

const validation = Yup.object().shape({
  comment: Yup.string().required("Comment is required."),
});

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
  }, [isLoading]);

  const handleDelete = async (event: { preventDefault: () => void }) => {
    event.preventDefault();
    const token = await getAccessTokenSilently();
    await deleteIssue(token, issueKey);

    navigate(-1);
  };

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateFormsInputs>({
    resolver: yupResolver(validation) as never,
  });

  const handleFormSubmit = async (form: CreateFormsInputs) => {
    const token = await getAccessTokenSilently();
    await postComment(token, {
      comment: form.comment,
      issueKey: issueKey,
    });
    resetForm();
    setIsLoading(true);
  };

  const resetForm = () => {
    reset();
  };

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

          <div className="flex justify-evenly">
            <div className="max-w-96 lg:min-w-96">
              <div className="px-4 sm:px-0">
                <h3 className="text-3xl font-semibold tracking-wider">
                  {issue?.title}
                </h3>
              </div>
              <div className="mt-6">{issue?.content}</div>
            </div>

            <div className="space-y-4">
              <div className="dropdown">
                <div tabIndex={0} role="button" className="btn m-1">
                  <FaEllipsis />
                </div>
                <ul
                  tabIndex={0}
                  className="dropdown-content z-[1] menu p-2 shadow bg-base-100 rounded-box w-52"
                >
                  <li
                    className="btn btn-error"
                    onClick={() =>
                      (document.getElementById(
                        "delete_issue_modal"
                      ) as HTMLDialogElement)!.showModal()
                    }
                  >
                    Delete
                  </li>
                </ul>
              </div>
              <div className="shadow p-8 min-w-80">
                <div className="px-4 sm:px-0">
                  <h3 className="text-base font-semibold">Details</h3>
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
                      <dt className="text-sm">Reporter</dt>
                      <dd className="mt-1 text-sm  sm:col-span-2 sm:mt-0">
                        {issue?.createdBy.email}
                      </dd>
                    </div>
                    <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
                      <dt className="text-sm font-medium">Status</dt>
                      <dd className="mt-1 text-sm col-span-2 sm:mt-0">
                        {issue?.status}
                      </dd>
                    </div>
                  </dl>
                </div>
              </div>
              <div>
                <p className="text-sm">Created {issue?.createdOn}</p>
                <p className="text-sm">Updated {issue?.lastUpdatedOn}</p>
              </div>
            </div>
          </div>

          <hr />
          <form>
            <div>
              <textarea
                {...register("comment")}
                id="comment"
                placeholder="Post Comment"
                className="peer textarea textarea-bordered textarea-md w-full max-w-xs"
              />
              {errors.comment ? (
                <p className="text-red-600">{errors.comment.message}</p>
              ) : (
                ""
              )}
            </div>
            <button
              className="btn btn-info"
              type="submit"
              onClick={handleSubmit(handleFormSubmit)}
            >
              Post
            </button>
          </form>
          <div>
            {issue?.comments.map((comment) => (
              <div className="chat chat-start" key={comment.id}>
                <div className="chat-image avatar">
                  <div className="w-10 rounded-full">
                    <img
                      alt="Tailwind CSS chat bubble component"
                      src="https://daisyui.com/images/stock/photo-1534528741775-53994a69daeb.jpg"
                    />
                  </div>
                </div>
                <div className="chat-header">
                  {comment.createdBy.email}
                  <time className="text-xs opacity-50 mx-2">
                    {comment.createdOn}
                  </time>
                </div>
                <div className="chat-bubble">{comment.content}</div>
              </div>
            ))}
          </div>

          <dialog id="delete_issue_modal" className="modal px-20 md:px-0">
            <div className="modal-box">
              <form method="dialog">
                <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
                  X
                </button>
              </form>
              <div className="w-full">
                <div className="p-6 space-y-4">
                  <h1 className="text-xl font-bold ">
                    Are you sure you want to delete {issueKey}?
                  </h1>
                  <form>
                    <button className="btn btn-error" onClick={handleDelete}>
                      Yes, delete the issue
                    </button>
                  </form>
                  <form method="dialog">
                    <button className="btn">Cancel</button>
                  </form>
                </div>
              </div>
            </div>
          </dialog>
        </>
      )}
    </>
  );
};

export default Issue;
