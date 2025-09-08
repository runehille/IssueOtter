import { useContext } from "react";
import { deleteProject } from "../../../../Api/ProjectApi";
import { ProjectContext } from "../../Context/Context";
import { useAuth } from "../../../../hooks/useAuth";
import { useNavigate } from "react-router";

const Settings = () => {
  const projectKey = useContext(ProjectContext);
  const { getAccessTokenSilently } = useAuth();
  const navigate = useNavigate();

  const handleDelete = async () => {
    const token = await getAccessTokenSilently();
    await deleteProject(token, projectKey);
    navigate("/");
  };

  return (
    <>
      <div className="space-y-4">
        <div className="shadow p-8">
          <div className="mt-6 border-t border-gray-100">
            <dl className="divide-y divide-gray-100">
              <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
                <dt className="text-sm text-center">
                  Permanently delete the project.
                </dt>
                <dd className=" text-sm">
                  <button
                    className="btn btn-error"
                    onClick={() =>
                      (document.getElementById(
                        "delete_project_modal"
                      ) as HTMLDialogElement)!.showModal()
                    }
                  >
                    Delete
                  </button>
                </dd>
              </div>
            </dl>
          </div>
        </div>
      </div>

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
