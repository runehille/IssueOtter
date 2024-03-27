import { Link, Outlet } from "react-router-dom";
import Navbar from "../../Components/Navbar/Navbar";
import { FaAngleDoubleRight } from "react-icons/fa";
import Breadcrumbs from "../../Components/Breadcrumbs/Breadcrumbs";
import { useAuth0, withAuthenticationRequired } from "@auth0/auth0-react";

type Props = {
  projectKey: string;
};

const ProjectPage = ({ projectKey }: Props) => {
  const { isLoading } = useAuth0();

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-screen">
        <span className="loading loading-spinner text-primary"></span>
      </div>
    );
  }

  return (
    <>
      <Navbar />
      <div className="container flex">
        <label htmlFor="my-drawer-2" className="btn btn-sm md:hidden ">
          <FaAngleDoubleRight />
        </label>
        <div className="drawer md:drawer-open">
          <input id="my-drawer-2" type="checkbox" className="drawer-toggle" />
          <div className="drawer-content flex items-start justify-start sm:pl-32 sm:pt-12 border-w-2">
            <div className="space-y-12 ">
              <Breadcrumbs />
              <Outlet />
            </div>
          </div>
          <div className="drawer-side rounded-e-xl border-x-2">
            <label
              htmlFor="my-drawer-2"
              aria-label="close sidebar"
              className="drawer-overlay"
            ></label>
            <ul className="menu p-4 w-72 min-h-full bg-base-200">
              <li>
                <div className="card w-96 bg-base-100 shadow-xl">
                  <div className="card-body">
                    <h2 className="card-title">{projectKey}</h2>
                    <p>This is a test project {projectKey}</p>
                  </div>
                </div>
              </li>
              <li>
                <Link to="issues" className="text-lg">
                  Issues
                </Link>
              </li>
              <li>
                <Link to="board" className="text-lg">
                  Board
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </>
  );
};

const AuthenticatedProjectPage = withAuthenticationRequired(ProjectPage);

export default AuthenticatedProjectPage;
