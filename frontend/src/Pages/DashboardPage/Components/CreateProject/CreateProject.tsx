import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { postProject } from "../../../../Api/ProjectApi";
import { useAuth } from "../../../../hooks/useAuth";
import { useNavigate } from "react-router";

type CreateFormsInputs = {
  title: string;
  key: string;
  description: string;
};

const validation = Yup.object().shape({
  title: Yup.string().required("Title is required"),
  key: Yup.string().required("A project key is required"),
  description: Yup.string(),
});

const CreateProjectModal = () => {
  const { getAccessTokenSilently } = useAuth();
  const navigate = useNavigate();

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
    await postProject(token, {
      title: form.title,
      key: form.key,
      description: form.description,
    });
    resetForm();
    navigate(`/app/project/${form.key}`);
  };

  const resetForm = () => {
    reset();
  };

  return (
    <>
      <div>
        <div className="w-full bg-base flex">
          <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
            <h1 className="text-xl font-bold ">Create new project</h1>
            <form
              className="space-y-4 md:space-y-6"
              onSubmit={handleSubmit(handleFormSubmit)}
            >
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
                <input
                  {...register("key")}
                  type="text"
                  id="key"
                  className="input input-bordered w-full max-w-xs"
                  placeholder="Key"
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
    </>
  );
};

export default CreateProjectModal;
