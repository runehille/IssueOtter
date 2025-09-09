import { useContext, useEffect, useState } from "react";
import { deleteProject, getProjectByKey, updateProject } from "../../../../Api/ProjectApi";
import { ProjectContext } from "../../Context/Context";
import { useAuth } from "../../../../hooks/useAuth";
import { useNavigate } from "react-router";
import { ProjectGet, ProjectUpdate } from "../../../../Models/Project";
import { useForm } from "react-hook-form";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

type EditProjectForm = {
  title: string;
  description: string;
};

const validation = Yup.object().shape({
  title: Yup.string().required("Title is required"),
  description: Yup.string(),
});

const Settings = () => {
  const projectKey = useContext(ProjectContext);
  const { getAccessTokenSilently } = useAuth();
  const navigate = useNavigate();
  const [project, setProject] = useState<ProjectGet | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [isUpdating, setIsUpdating] = useState(false);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<EditProjectForm>({
    resolver: yupResolver(validation) as never,
  });

  useEffect(() => {
    const fetchProject = async () => {
      const token = await getAccessTokenSilently();
      const result = await getProjectByKey(token, projectKey);
      if (result) {
        setProject(result.data);
        reset({
          title: result.data.title,
          description: result.data.description,
        });
      }
      setIsLoading(false);
    };
    fetchProject();
  }, [projectKey, getAccessTokenSilently, reset]);

  const handleDelete = async () => {
    const token = await getAccessTokenSilently();
    await deleteProject(token, projectKey);
    navigate("/");
  };

  const handleUpdate = async (data: EditProjectForm) => {
    if (!project) return;
    
    setIsUpdating(true);
    try {
      const token = await getAccessTokenSilently();
      const projectUpdate: ProjectUpdate = {
        title: data.title,
        description: data.description,
      };
      
      const result = await updateProject(token, projectKey, projectUpdate);
      if (result) {
        setProject(result.data);
        (document.getElementById(
          "edit_project_modal"
        ) as HTMLDialogElement)!.close();
      }
    } catch (error) {
      console.error("Failed to update project:", error);
    } finally {
      setIsUpdating(false);
    }
  };

  if (isLoading) {
    return (
      <div className="flex justify-center items-center h-64">
        <span className="loading loading-spinner loading-lg"></span>
      </div>
    );
  }

  if (!project) {
    return (
      <div className="text-center text-gray-500">
        Project not found
      </div>
    );
  }

  return (
    <>
      <div className="space-y-4">
        <div className="shadow p-8">
          <div className="space-y-6">
            <div className="border-b border-gray-200 pb-4">
              <h2 className="text-lg font-medium text-gray-900">Project Details</h2>
              <p className="mt-1 text-sm text-gray-600">
                Manage your project information and settings.
              </p>
            </div>
            
            <div className="grid grid-cols-1 gap-y-6 gap-x-4 sm:grid-cols-6">
              <div className="sm:col-span-3">
                <label className="block text-sm font-medium text-gray-700">
                  Project Key
                </label>
                <div className="mt-1">
                  <input
                    type="text"
                    value={project.key}
                    disabled
                    className="input input-bordered w-full bg-gray-50"
                  />
                </div>
                <p className="mt-2 text-sm text-gray-500">
                  Project key cannot be changed after creation.
                </p>
              </div>

              <div className="sm:col-span-3">
                <label className="block text-sm font-medium text-gray-700">
                  Title
                </label>
                <div className="mt-1">
                  <input
                    type="text"
                    value={project.title}
                    disabled
                    className="input input-bordered w-full bg-gray-50"
                  />
                </div>
              </div>

              <div className="sm:col-span-6">
                <label className="block text-sm font-medium text-gray-700">
                  Description
                </label>
                <div className="mt-1">
                  <textarea
                    value={project.description}
                    disabled
                    className="textarea textarea-bordered w-full bg-gray-50 h-20"
                  />
                </div>
              </div>
            </div>
            
            <div className="flex justify-start">
              <button
                className="btn btn-primary"
                onClick={() =>
                  (document.getElementById(
                    "edit_project_modal"
                  ) as HTMLDialogElement)!.showModal()
                }
              >
                Edit Project
              </button>
            </div>
          </div>
          
          <div className="mt-8 border-t border-gray-100 pt-6">
            <div className="flex justify-between items-center">
              <div>
                <h3 className="text-sm font-medium text-gray-900">Danger Zone</h3>
                <p className="mt-1 text-sm text-gray-600">
                  Permanently delete this project and all its data.
                </p>
              </div>
              <button
                className="btn btn-error"
                onClick={() =>
                  (document.getElementById(
                    "delete_project_modal"
                  ) as HTMLDialogElement)!.showModal()
                }
              >
                Delete Project
              </button>
            </div>
          </div>
        </div>
      </div>

      <dialog id="edit_project_modal" className="modal px-20 md:px-0">
        <div className="modal-box">
          <form method="dialog">
            <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
              ✕
            </button>
          </form>
          <div className="w-full">
            <div className="p-6 space-y-4">
              <h1 className="text-xl font-bold">Edit Project</h1>
              <form
                className="space-y-4"
                onSubmit={handleSubmit(handleUpdate)}
              >
                <div>
                  <label className="form-control w-full">
                    <div className="label">
                      <span className="label-text">Title</span>
                    </div>
                    <input
                      {...register("title")}
                      type="text"
                      className="input input-bordered w-full"
                      placeholder="Project title"
                    />
                    {errors.title && (
                      <div className="label">
                        <span className="label-text-alt text-error">
                          {errors.title.message}
                        </span>
                      </div>
                    )}
                  </label>
                </div>
                
                <div>
                  <label className="form-control w-full">
                    <div className="label">
                      <span className="label-text">Description</span>
                    </div>
                    <textarea
                      {...register("description")}
                      className="textarea textarea-bordered w-full h-24"
                      placeholder="Project description"
                    />
                    {errors.description && (
                      <div className="label">
                        <span className="label-text-alt text-error">
                          {errors.description.message}
                        </span>
                      </div>
                    )}
                  </label>
                </div>
                
                <div className="flex gap-2">
                  <button
                    type="submit"
                    className="btn btn-primary flex-1"
                    disabled={isUpdating}
                  >
                    {isUpdating ? (
                      <span className="loading loading-spinner loading-sm"></span>
                    ) : (
                      "Update Project"
                    )}
                  </button>
                  <button
                    type="button"
                    className="btn btn-ghost"
                    onClick={() => {
                      (document.getElementById(
                        "edit_project_modal"
                      ) as HTMLDialogElement)!.close();
                    }}
                  >
                    Cancel
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </dialog>

      <dialog id="delete_project_modal" className="modal px-20 md:px-0">
        <div className="modal-box">
          <form method="dialog">
            <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
              X
            </button>
          </form>
          <div className="w-full">
            <div className="p-6 space-y-4">
              <h1 className="text-xl font-bold ">
                Are you sure you want to delete {projectKey}?
              </h1>
              <form>
                <button className="btn btn-error" onClick={handleDelete}>
                  Yes, delete the project
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
  );
};

export default Settings;
