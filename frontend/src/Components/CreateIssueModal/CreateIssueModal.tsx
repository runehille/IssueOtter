import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";

type CreateFormsInputs = {
  title: string;
  project: string;
  description: string;
};

const validation = Yup.object().shape({
  project: Yup.string().notOneOf(["default"], "Project is required"),
  title: Yup.string().required("Title is required"),
  description: Yup.string(),
});

const CreateIssueModal = () => {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateFormsInputs>({ resolver: yupResolver(validation) });

  const handleFormSubmit = (form: CreateFormsInputs) => {
    (document.getElementById("my_modal_1") as HTMLDialogElement)!.close();
    resetForm();
    console.log(form.project, form.title, form.description);
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
            "my_modal_1"
          ) as HTMLDialogElement)!.showModal()
        }
      >
        Create Issue
      </button>
      <dialog id="my_modal_1" className="modal">
        <div className="modal-box">
          <form method="dialog">
            <button
              onClick={resetForm}
              className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
            >
              X
            </button>
          </form>
          <div className="w-full bg-base rounded-lg shadow ">
            <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
              <h1 className="text-xl font-bold ">Create new issue</h1>
              <form
                className="space-y-4 md:space-y-6"
                onSubmit={handleSubmit(handleFormSubmit)}
              >
                <div>
                  <select
                    {...register("project")}
                    className="select select-bordered w-full max-w-xs"
                    defaultValue="default"
                  >
                    <option disabled value="default">
                      Choose project
                    </option>
                    <option value="frontend">Frontend</option>
                    <option value="backend">Backend</option>
                  </select>
                  {errors.project ? (
                    <p className="text-red-600">{errors.project.message}</p>
                  ) : (
                    ""
                  )}
                </div>
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
                    {...register("description")}
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
