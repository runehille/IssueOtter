import { Link } from "react-router-dom";

const Default = () => {
  return (
    <div className="hero min-h-screen bg-base">
      <div className="hero-content text-center">
        <div className="max-w-md">
          <h1 className="text-5xl font-bold">Get started</h1>
          <p className="py-6">
            Create a new project or choose an existing one.
          </p>
          <Link to="create-project" className="text-lg btn btn-primary">
            Create new Project
          </Link>
        </div>
      </div>
    </div>
  );
};

export default Default;
