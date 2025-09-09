import { useAuth } from "../../../../hooks/useAuth";
import { useEffect, useState } from "react";
import { deleteIssue, getIssueByKey, updateIssue } from "../../../../Api/IssueApi";
import { useNavigate } from "react-router-dom";
import IssueSkeleton from "./IssueSkeleton";
import { IssueGet, IssueType, IssueStatus, IssuePriority } from "../../../../Models/Issue";
import { FaCaretLeft, FaEllipsis } from "react-icons/fa6";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as Yup from "yup";
import { postComment, updateComment, deleteComment } from "../../../../Api/CommentApi";
import LabelBadge from "../../../../Components/Label/LabelBadge";

type Props = {
  issueKey: string;
};

type CreateFormsInputs = {
  comment: string;
};

type EditIssueInputs = {
  title: string;
  content: string;
  type: IssueType;
  status: IssueStatus;
  priority: IssuePriority;
  assigneeId: number;
};

const validation = Yup.object().shape({
  comment: Yup.string().required("Comment is required."),
});

const editValidation = Yup.object().shape({
  title: Yup.string().required("Title is required."),
  content: Yup.string(),
  type: Yup.number().required(),
  status: Yup.number().required(),
  priority: Yup.number().required(),
  assigneeId: Yup.number().required(),
});

const Issue = ({ issueKey }: Props) => {
  const { getAccessTokenSilently } = useAuth();
  const [issue, setIssue] = useState<IssueGet | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [editingCommentId, setEditingCommentId] = useState<number | null>(null);
  const [editingCommentContent, setEditingCommentContent] = useState<string>("");
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

  const {
    register: registerEdit,
    handleSubmit: handleSubmitEdit,
    setValue,
    formState: { errors: editErrors },
  } = useForm<EditIssueInputs>({
    resolver: yupResolver(editValidation) as never,
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

  const handleEditClick = (commentId: number, currentContent: string) => {
    setEditingCommentId(commentId);
    setEditingCommentContent(currentContent);
  };

  const handleEditCancel = () => {
    setEditingCommentId(null);
    setEditingCommentContent("");
  };

  const handleEditSave = async (commentId: number) => {
    const token = await getAccessTokenSilently();
    await updateComment(token, commentId, {
      content: editingCommentContent,
    });
    setEditingCommentId(null);
    setEditingCommentContent("");
    setIsLoading(true);
  };

  const handleEditIssue = () => {
    if (issue) {
      setValue("title", issue.title);
      setValue("content", issue.content);
      setValue("type", issue.type);
      setValue("status", issue.status);
      setValue("priority", issue.priority);
      setValue("assigneeId", issue.assigneeId || 1); // Default to user ID 1 if null
      (document.getElementById("edit_issue_modal") as HTMLDialogElement)!.showModal();
    }
  };

  const handleEditSubmit = async (form: EditIssueInputs) => {
    if (issue) {
      const token = await getAccessTokenSilently();
      await updateIssue(token, issue.id, {
        title: form.title,
        content: form.content,
        type: form.type,
        status: form.status,
        priority: form.priority,
        assigneeId: form.assigneeId,
      });
      (document.getElementById("edit_issue_modal") as HTMLDialogElement)!.close();
      setIsLoading(true);
    }
  };

  const handleDeleteComment = async (commentId: number) => {
    if (window.confirm("Are you sure you want to delete this comment?")) {
      const token = await getAccessTokenSilently();
      await deleteComment(token, commentId);
      setIsLoading(true);
    }
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
                {issue?.labels && issue.labels.length > 0 && (
                  <div className="flex flex-wrap gap-2 mt-3">
                    {issue.labels.map((label) => (
                      <LabelBadge key={label.id} label={label} size="md" />
                    ))}
                  </div>
                )}
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
                  <li>
                    <button
                      className="btn btn-primary"
                      onClick={handleEditIssue}
                    >
                      Edit
                    </button>
                  </li>
                  <li>
                    <button
                      className="btn btn-error"
                      onClick={() =>
                        (document.getElementById(
                          "delete_issue_modal"
                        ) as HTMLDialogElement)!.showModal()
                      }
                    >
                      Delete
                    </button>
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
                        {issue?.assignee?.email || 'Unassigned'}
                      </dd>
                    </div>
                    <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
                      <dt className="text-sm">Reporter</dt>
                      <dd className="mt-1 text-sm  sm:col-span-2 sm:mt-0">
                        {issue?.createdBy?.email || 'Unknown'}
                      </dd>
                    </div>
                    <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
                      <dt className="text-sm font-medium">Type</dt>
                      <dd className="mt-1 text-sm col-span-2 sm:mt-0">
                        {issue?.type}
                      </dd>
                    </div>
                    <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
                      <dt className="text-sm font-medium">Status</dt>
                      <dd className="mt-1 text-sm col-span-2 sm:mt-0">
                        {issue?.status}
                      </dd>
                    </div>
                    <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
                      <dt className="text-sm font-medium">Priority</dt>
                      <dd className="mt-1 text-sm col-span-2 sm:mt-0">
                        <span className={`badge ${
                          issue?.priority === IssuePriority.Low ? "badge-ghost" :
                          issue?.priority === IssuePriority.Medium ? "badge-info" :
                          issue?.priority === IssuePriority.High ? "badge-warning" :
                          issue?.priority === IssuePriority.Critical ? "badge-error" : ""
                        }`}>
                          {issue?.priority === IssuePriority.Low ? "Low" :
                           issue?.priority === IssuePriority.Medium ? "Medium" :
                           issue?.priority === IssuePriority.High ? "High" :
                           issue?.priority === IssuePriority.Critical ? "Critical" : ""}
                        </span>
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
          <div className="space-y-6 pb-20">
            {issue?.comments.map((comment) => (
              <div className="chat chat-start" key={comment.id}>
                <div className="chat-image avatar">
                  <div className="w-10 rounded-full">
                    <img
                      alt="Tailwind CSS chat bubble component"
                      src="https://picsum.photos/200"
                    />
                  </div>
                </div>
                <div className="chat-header text-sm">
                  {comment.createdBy?.email}
                  <time className="text-xs opacity-50 mx-2">
                    {comment.createdOn}
                  </time>
                </div>
                <div className="flex space-x-6 items-end">
                  {editingCommentId === comment.id ? (
                    <div className="flex flex-col space-y-2">
                      <textarea
                        value={editingCommentContent}
                        onChange={(e) => setEditingCommentContent(e.target.value)}
                        className="textarea textarea-bordered textarea-sm w-full max-w-xs"
                        rows={3}
                      />
                      <div className="flex space-x-2">
                        <button
                          className="btn btn-success btn-xs"
                          onClick={() => handleEditSave(comment.id)}
                        >
                          Save
                        </button>
                        <button
                          className="btn btn-outline btn-xs"
                          onClick={handleEditCancel}
                        >
                          Cancel
                        </button>
                      </div>
                    </div>
                  ) : (
                    <>
                      <div className="chat-bubble chat-bubble-primary">
                        {comment.content}
                      </div>
                      <div className="flex space-x-2">
                        <button
                          className="btn btn-outline btn-xs"
                          onClick={() => handleEditClick(comment.id, comment.content)}
                        >
                          Edit
                        </button>
                        <button 
                          className="btn btn-error btn-xs"
                          onClick={() => handleDeleteComment(comment.id)}
                        >
                          Delete
                        </button>
                      </div>
                    </>
                  )}
                </div>
              </div>
            ))}
          </div>

          <dialog id="edit_issue_modal" className="modal px-20 md:px-0">
            <div className="modal-box max-w-2xl">
              <form method="dialog">
                <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
                  X
                </button>
              </form>
              <div className="w-full">
                <div className="p-6 space-y-4">
                  <h1 className="text-xl font-bold">Edit Issue</h1>
                  <form onSubmit={handleSubmitEdit(handleEditSubmit)} className="space-y-4">
                    <div>
                      <label className="block text-sm font-medium mb-1">Title</label>
                      <input
                        {...registerEdit("title")}
                        type="text"
                        className="input input-bordered w-full"
                      />
                      {editErrors.title && (
                        <p className="text-red-600 text-sm">{editErrors.title.message}</p>
                      )}
                    </div>

                    <div>
                      <label className="block text-sm font-medium mb-1">Content</label>
                      <textarea
                        {...registerEdit("content")}
                        className="textarea textarea-bordered w-full h-32"
                      />
                      {editErrors.content && (
                        <p className="text-red-600 text-sm">{editErrors.content.message}</p>
                      )}
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                      <div>
                        <label className="block text-sm font-medium mb-1">Type</label>
                        <select {...registerEdit("type")} className="select select-bordered w-full">
                          <option value={IssueType.Task}>Task</option>
                          <option value={IssueType.Bug}>Bug</option>
                        </select>
                      </div>

                      <div>
                        <label className="block text-sm font-medium mb-1">Status</label>
                        <select {...registerEdit("status")} className="select select-bordered w-full">
                          <option value={IssueStatus.ToDo}>To Do</option>
                          <option value={IssueStatus.InProgress}>In Progress</option>
                          <option value={IssueStatus.InReview}>In Review</option>
                          <option value={IssueStatus.Done}>Done</option>
                          <option value={IssueStatus.Closed}>Closed</option>
                        </select>
                      </div>
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                      <div>
                        <label className="block text-sm font-medium mb-1">Priority</label>
                        <select {...registerEdit("priority")} className="select select-bordered w-full">
                          <option value={IssuePriority.Low}>Low</option>
                          <option value={IssuePriority.Medium}>Medium</option>
                          <option value={IssuePriority.High}>High</option>
                          <option value={IssuePriority.Critical}>Critical</option>
                        </select>
                      </div>

                      <div>
                        <label className="block text-sm font-medium mb-1">Assignee ID</label>
                        <input
                          {...registerEdit("assigneeId")}
                          type="number"
                          className="input input-bordered w-full"
                        />
                        {editErrors.assigneeId && (
                          <p className="text-red-600 text-sm">{editErrors.assigneeId.message}</p>
                        )}
                      </div>
                    </div>

                    <div className="flex space-x-2">
                      <button type="submit" className="btn btn-primary">
                        Save Changes
                      </button>
                      <button 
                        type="button" 
                        className="btn" 
                        onClick={() => (document.getElementById("edit_issue_modal") as HTMLDialogElement)!.close()}
                      >
                        Cancel
                      </button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </dialog>

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
