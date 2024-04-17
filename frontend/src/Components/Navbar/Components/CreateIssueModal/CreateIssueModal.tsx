import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { ProjectGet } from "../../../../Models/Project";
import { postIssue } from "../../../../Api/IssueApi";
import { useAuth0 } from "@auth0/auth0-react";

type Props = {
  projects: ProjectGet[];
};

type CreateFormsInputs = {
  projectKey: string;
  title: string;
  content: string;
  status: string;
};

const validation = Yup.object().shape({
  projectKey: Yup.string()
    .notOneOf(["default"], "Project is required")
    .required("Project is required"),
  title: Yup.string().required("Title is required"),
  description: Yup.string(),
  status: Yup.string(),
});

const CreateIssueModal = ({ projects }: Props) => {
  const { getAccessTokenSilently } = useAuth0();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateFormsInputs>({
    resolver: yupResolver(validation) as never,
  });

  const handleFormSubmit = async (form: CreateFormsInputs) => {
    (document.getElementById(
      "create_issue_modal"
    ) as HTMLDialogElement)!.close();

    const token = await getAccessTokenSilently();
    await postIssue(token, {
      title: form.title,
      content: form.content,
      projectKey: form.projectKey,
      status: form.status,
    });
    resetForm();
  };

  const resetForm = () => {
    reset();
  };
  return (
    <>
      <button
        className="btn btn-accent"
        onClick={() =>
          (document.getElementById(
            "create_issue_modal"
          ) as HTMLDialogElement)!.showModal()
        }
      >
        Create <br /> Issue
      </button>
      <dialog id="create_issue_modal" className="modal px-20 md:px-0">
        <div className="modal-box">
          <form method="dialog">
            <button
              onClick={resetForm}
              className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
            >
              X
            </button>
          </form>
          <div className="w-full">
            <div className="p-6 space-y-4">
              <h1 className="text-xl font-bold ">Create new issue</h1>
              <form
                className="space-y-4 md:space-y-6"
                onSubmit={handleSubmit(handleFormSubmit)}
              >
                <div className="space-y-4">
                  <select
                    {...register("projectKey")}
                    className="select select-bordered w-full max-w-xs"
                    defaultValue="default"
                  >
                    <option disabled value="default">
                      Choose project
                    </option>
                    {projects.map((project) => (
                      <option key={project.key} value={project.key}>
                        {project.title}
                      </option>
                    ))}
                  </select>
                  {errors.projectKey ? (
                    <p className="text-red-600">{errors.projectKey.message}</p>
                  ) : (
                    ""
                  )}
                  <label className="form-control w-full max-w-xs">
                    <div className="label">
                      <span className="label-text">Status</span>
                    </div>
                    <select
                      {...register("status")}
                      className="select select-bordered max-w-xs "
                    >
                      <option selected value="To Do">
                        To Do
                      </option>
                      <option value="In Progress">In Progress</option>
                      <option value="Done">Done</option>
                    </select>
                  </label>
                  <label className="form-control w-full max-w-xs">
                    <div className="label">
                      <span className="label-text">Assignee</span>
                    </div>
                    <select className="select select-bordered max-w-xs "></select>
                  </label>
                </div>
                <hr />
                <div>
                  <input
                    {...register("title")}
                    type="text"
                    id="username"
                    className="input input-bordered w-full max-w-xs"
                    placeholder="Title"
                  />
                  {errors.title ? (
                    <p className="text-red-600">{errors.title.message}</p>
                  ) : (
                    ""
                  )}
                </div>
                <div>
                  <textarea
                    {...register("content")}
                    id="description"
                    className="textarea textarea-bordered h-24 w-full"
                    placeholder="Description"
                  />
                </div>
                <button type="submit" className="btn btn-success w-full">
                  Create
                </button>
              </form>
            </div>
          </div>
        </div>
      </dialog>
    </>
  );
};

export default CreateIssueModal;
