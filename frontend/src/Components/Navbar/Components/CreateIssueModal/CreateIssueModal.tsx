import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import { ProjectGet } from "../../../../Models/Project";
import { postIssue } from "../../../../Api/IssueApi";
import { useAuth } from "../../../../hooks/useAuth";
import { IssueStatus, IssueType, IssuePriority } from "../../../../Models/Issue";
import { Label } from "../../../../Models/Label";
import { getLabelsByProjectId } from "../../../../Api/LabelApi";
import LabelBadge from "../../../../Components/Label/LabelBadge";

type Props = {
  projects: ProjectGet[];
};

type CreateFormsInputs = {
  projectKey: string;
  title: string;
  content: string;
  status: IssueStatus;
  type: IssueType;
  priority: IssuePriority;
  labelIds: number[];
};

const validation = Yup.object().shape({
  projectKey: Yup.string()
    .notOneOf(["default"], "Project is required")
    .required("Project is required"),
  title: Yup.string().required("Title is required"),
  content: Yup.string(),
  status: Yup.number(),
  type: Yup.number(),
  priority: Yup.number(),
  labelIds: Yup.array().of(Yup.number()),
});

const CreateIssueModal = ({ projects }: Props) => {
  const { getAccessTokenSilently } = useAuth();
  const [availableLabels, setAvailableLabels] = useState<Label[]>([]);
  const [selectedLabels, setSelectedLabels] = useState<number[]>([]);

  const {
    register,
    handleSubmit,
    reset,
    setValue,
    watch,
    formState: { errors },
  } = useForm<CreateFormsInputs>({
    resolver: yupResolver(validation) as never,
    defaultValues: {
      labelIds: []
    }
  });

  const watchedProjectKey = watch("projectKey");

  useEffect(() => {
    if (watchedProjectKey && watchedProjectKey !== "default") {
      const project = projects.find(p => p.key === watchedProjectKey);
      if (project) {
        fetchLabelsForProject(project.id);
      }
    } else {
      setAvailableLabels([]);
      setSelectedLabels([]);
      setValue("labelIds", []);
    }
  }, [watchedProjectKey, projects]);

  const fetchLabelsForProject = async (projectId: number) => {
    try {
      const token = await getAccessTokenSilently();
      const response = await getLabelsByProjectId(token, projectId);
      if (response?.data) {
        setAvailableLabels(response.data);
      }
    } catch (error) {
      console.error("Failed to fetch labels:", error);
    }
  };

  const toggleLabel = (labelId: number) => {
    const newSelectedLabels = selectedLabels.includes(labelId)
      ? selectedLabels.filter(id => id !== labelId)
      : [...selectedLabels, labelId];
    
    setSelectedLabels(newSelectedLabels);
    setValue("labelIds", newSelectedLabels);
  };

  const handleFormSubmit = async (form: CreateFormsInputs) => {
    (document.getElementById(
      "create_issue_modal"
    ) as HTMLDialogElement)!.close();

    const token = await getAccessTokenSilently();
    await postIssue(token, {
      title: form.title,
      content: form.content,
      type: form.type,
      priority: form.priority,
      projectKey: form.projectKey,
      status: form.status,
      labelIds: form.labelIds,
    });
    resetForm();
  };

  const resetForm = () => {
    reset();
    setSelectedLabels([]);
    setAvailableLabels([]);
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
                      <span className="label-text">Type</span>
                    </div>
                    <select
                      {...register("type")}
                      id="type"
                      className="select select-bordered max-w-xs "
                    >
                      <option defaultValue="true" value="Task">
                        Task
                      </option>
                      <option value="Bug">Bug</option>
                    </select>
                  </label>
                  <label className="form-control w-full max-w-xs">
                    <div className="label">
                      <span className="label-text">Status</span>
                    </div>
                    <select
                      {...register("status")}
                      id="status"
                      className="select select-bordered max-w-xs "
                    >
                      <option defaultValue="true" value="To Do">
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
                    <select
                      id="assignee"
                      className="select select-bordered max-w-xs "
                    ></select>
                  </label>
                  {availableLabels.length > 0 && (
                    <label className="form-control w-full">
                      <div className="label">
                        <span className="label-text">Labels</span>
                      </div>
                      <div className="border border-gray-300 rounded-lg p-3 min-h-[60px]">
                        <div className="flex flex-wrap gap-2 mb-2">
                          {selectedLabels.map(labelId => {
                            const label = availableLabels.find(l => l.id === labelId);
                            return label ? (
                              <LabelBadge 
                                key={label.id} 
                                label={label} 
                                removable={true}
                                onRemove={() => toggleLabel(label.id)}
                              />
                            ) : null;
                          })}
                          {selectedLabels.length === 0 && (
                            <span className="text-gray-400 text-sm">No labels selected</span>
                          )}
                        </div>
                        <div className="border-t pt-2">
                          <div className="text-xs text-gray-500 mb-1">Available labels:</div>
                          <div className="flex flex-wrap gap-1">
                            {availableLabels
                              .filter(label => !selectedLabels.includes(label.id))
                              .map(label => (
                                <button
                                  key={label.id}
                                  type="button"
                                  className="text-xs px-2 py-1 rounded border hover:bg-gray-50"
                                  onClick={() => toggleLabel(label.id)}
                                >
                                  + {label.name}
                                </button>
                              ))
                            }
                            {availableLabels.filter(label => !selectedLabels.includes(label.id)).length === 0 && (
                              <span className="text-xs text-gray-400">All labels selected</span>
                            )}
                          </div>
                        </div>
                      </div>
                    </label>
                  )}
                </div>
                <hr />
                <div>
                  <input
                    {...register("title")}
                    type="text"
                    id="title"
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
                    id="content"
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
