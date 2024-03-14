import { Link } from "react-router-dom";

const LandingPage = () => {
  return (
    <>
      <div className="sm:mx-48">
        <div className="navbar justify-between">
          <p className="font-bold text-3xl">IssueOtter</p>
          <div className="flex space-x-8">
            <button className="btn btn-info">Signup</button>
            <Link to="/dashboard" className="btn btn-primary">
              Login
            </Link>
          </div>
        </div>
      </div>
      <div className="hero min-h-screen">
        <div className="hero-content flex-col-reverse lg:flex-row-reverse lg:mb-32">
          <img src="/otter.svg" className="m-10" />
          <div>
            <h1 className="text-5xl font-bold">
              The best way <br /> to manage <br /> your projects.
            </h1>
            <p className="py-6">
              IssueOtter lets you keep track of issues, <br /> manage projects
              and much more! <br /> Click the button to get started.
            </p>
            <button className="btn btn-info">Get Started</button>
          </div>
        </div>
      </div>

      <footer className="footer p-10 bg-neutral text-neutral-content">
        <aside>
          <p>IssueOtter 2024</p>
        </aside>
      </footer>
    </>
  );
};

export default LandingPage;
