import { Link, Outlet } from "react-router-dom";
import { FaAngleDoubleRight } from "react-icons/fa";
import Breadcrumbs from "../../Components/Breadcrumbs/Breadcrumbs";
import { useAuth0, withAuthenticationRequired } from "@auth0/auth0-react";
import { ProjectContext } from "./Context/Context";
import { useEffect, useState } from "react";
import { getProjectByKey } from "../../Api/ProjectApi";
import { ProjectGet } from "../../Models/Project";

type Props = {
  projectKey: string;
};

const ProjectPage = ({ projectKey }: Props) => {
  const { getAccessTokenSilently } = useAuth0();
  const [project, setProject] = useState<ProjectGet | null>(null);

  useEffect(() => {
    const fetchProject = async () => {
      const token = await getAccessTokenSilently();
      const fetchedProject = await getProjectByKey(token, projectKey);
      if (fetchedProject) {
        setProject(fetchedProject.data);
      }
    };
    fetchProject();
  }, [projectKey]);

  return (
    <>
      <div className="container flex">
        <label htmlFor="my-drawer-2" className="btn btn-sm md:hidden ">
          <FaAngleDoubleRight />
        </label>
        <div className="drawer md:drawer-open">
          <input id="my-drawer-2" type="checkbox" className="drawer-toggle" />
          <div className="drawer-content flex sm:pl-32 sm:pt-12 border-w-2">
            <div className="space-y-12">
              <Breadcrumbs />
              <ProjectContext.Provider value={projectKey}>
                <Outlet />
              </ProjectContext.Provider>
            </div>
          </div>
          <div className="drawer-side">
            <label
              htmlFor="my-drawer-2"
              aria-label="close sidebar"
              className="drawer-overlay"
            ></label>
            <ul className="menu p-4 w-72 min-h-full bg-base-200 justify-items-start">
              <div>
                <li>
                  <div className="card bg-base-100 shadow-xl">
                    <div className="card-body">
                      <h2 className="card-title">{projectKey}</h2>
                      <p>{project?.description}</p>
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
              </div>
              <div className="mt-20">
                <ul>
                  <li>
                    <Link to="settings">Project Settings</Link>
                  </li>
                </ul>
              </div>
            </ul>
          </div>
        </div>
      </div>
    </>
  );
};

const AuthenticatedProjectPage = withAuthenticationRequired(ProjectPage);

export default AuthenticatedProjectPage;
