import { Link } from "react-router-dom";

const LandingPage = () => {
  return (
    <>
      <div className="bg-gradient-to-r from-base-300 to-60%">
        <div className="sm:mx-32 lg:mx-48 sticky top-0 bg-transparent">
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
              <h1 className="text-5xl font-bold tracking-wide">
                The best way <br /> to manage <br /> your projects.
              </h1>
              <p className="py-6 tracking-wide">
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
      </div>
    </>
  );
};

export default LandingPage;
