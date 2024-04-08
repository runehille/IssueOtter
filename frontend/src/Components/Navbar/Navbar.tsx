import { Link, useLocation } from "react-router-dom";
import CreateIssueModal from "./Components/CreateIssueModal/CreateIssueModal";
import { useEffect, useLayoutEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { FaBell } from "react-icons/fa6";
import { getAllProjects } from "../../Api/ProjectApi";
import { postUser } from "../../Api/UserApi";
import { ProjectGet } from "../../Models/Project";
import { UserPost } from "../../Models/User";

const Navbar = () => {
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const location = useLocation();
  const paths = location.pathname.split("/").filter((path) => path !== "");
  const { logout, user, getAccessTokenSilently } = useAuth0();
  const [projects, setProjects] = useState<ProjectGet[]>([]);
  const [isFetchingProjects, setIsFetchingProjects] = useState(true);
  const [isDarkMode, setIsDarkMode] = useState(
    JSON.parse(localStorage.getItem("isDarkMode") as string)
  );

  useLayoutEffect(() => {
    localStorage.setItem("isDarkMode", JSON.stringify(isDarkMode));
  }, [isDarkMode]);

  useEffect(() => {
    const syncUserData = async () => {
      if (!user) {
        return;
      }
      const token = await getAccessTokenSilently();

      const userToPost: UserPost = {
        email: user?.email || "",
        firstname: user?.email || "",
        lastname: user?.email || "",
      };
      await postUser(token, userToPost);
    };
    syncUserData();
  }, [user]);

  useEffect(() => {
    const fetchProjects = async () => {
      const token = await getAccessTokenSilently();
      const result = await getAllProjects(token);
      if (result) {
        setProjects(result.data);
        setIsFetchingProjects(false);
      }
    };
    fetchProjects();
  }, []);

  return (
    <div className="navbar bg-base-100 shadow">
      <div className="flex-1 md:space-x-5">
        <Link
          to="/app/dashboard"
          className="btn btn-ghost md:mb-2 hover:bg-transparent"
        >
          <img
            src={isDarkMode ? "/otter-dark.svg" : "/otter.svg"}
            alt=""
            className="md:size-14 size-10"
          />
          <p className="text-lg hidden md:flex">IssueOtter</p>
        </Link>
        <div
          className={`dropdown ${
            paths.includes("project") ? "border-b-2 border-b-base-content" : ""
          }`}
        >
          <div
            tabIndex={0}
            role="button"
            className="btn"
            onClick={() => setDropdownOpen(!dropdownOpen)}
          >
            Projects
          </div>
          {dropdownOpen && (
            <ul
              tabIndex={0}
              className="dropdown-content z-[1] menu p-2 mt-1 shadow bg-base-100 rounded-box w-52"
            >
              {projects.map((project) => (
                <li key={project.key}>
                  <Link
                    to={`/app/project/${project.key}`}
                    onClick={() => setDropdownOpen(false)}
                  >
                    {project.title} ({project.key})
                  </Link>
                </li>
              ))}
            </ul>
          )}
        </div>
        <CreateIssueModal projects={projects} />
      </div>

      <div className="flex-none gap-2">
        {/* Light/Dark Mode */}
        <label className="swap swap-rotate">
          <input
            type="checkbox"
            value={isDarkMode ? "dark" : "light"}
            checked={isDarkMode}
            onChange={() => setIsDarkMode(!isDarkMode)}
            className=" theme-controller"
          />
          <svg
            className="swap-off fill-current w-8 h-8"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
          >
            <path d="M5.64,17l-.71.71a1,1,0,0,0,0,1.41,1,1,0,0,0,1.41,0l.71-.71A1,1,0,0,0,5.64,17ZM5,12a1,1,0,0,0-1-1H3a1,1,0,0,0,0,2H4A1,1,0,0,0,5,12Zm7-7a1,1,0,0,0,1-1V3a1,1,0,0,0-2,0V4A1,1,0,0,0,12,5ZM5.64,7.05a1,1,0,0,0,.7.29,1,1,0,0,0,.71-.29,1,1,0,0,0,0-1.41l-.71-.71A1,1,0,0,0,4.93,6.34Zm12,.29a1,1,0,0,0,.7-.29l.71-.71a1,1,0,1,0-1.41-1.41L17,5.64a1,1,0,0,0,0,1.41A1,1,0,0,0,17.66,7.34ZM21,11H20a1,1,0,0,0,0,2h1a1,1,0,0,0,0-2Zm-9,8a1,1,0,0,0-1,1v1a1,1,0,0,0,2,0V20A1,1,0,0,0,12,19ZM18.36,17A1,1,0,0,0,17,18.36l.71.71a1,1,0,0,0,1.41,0,1,1,0,0,0,0-1.41ZM12,6.5A5.5,5.5,0,1,0,17.5,12,5.51,5.51,0,0,0,12,6.5Zm0,9A3.5,3.5,0,1,1,15.5,12,3.5,3.5,0,0,1,12,15.5Z" />
          </svg>
          <svg
            className="swap-on fill-current w-8 h-8"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
          >
            <path d="M21.64,13a1,1,0,0,0-1.05-.14,8.05,8.05,0,0,1-3.37.73A8.15,8.15,0,0,1,9.08,5.49a8.59,8.59,0,0,1,.25-2A1,1,0,0,0,8,2.36,10.14,10.14,0,1,0,22,14.05,1,1,0,0,0,21.64,13Zm-9.5,6.69A8.14,8.14,0,0,1,7.08,5.22v.27A10.15,10.15,0,0,0,17.22,15.63a9.79,9.79,0,0,0,2.1-.22A8.11,8.11,0,0,1,12.14,19.73Z" />
          </svg>
        </label>
        {/* Search */}
        <div className="form-control">
          <input
            type="text"
            placeholder="Search"
            className="input input-bordered w-24 md:w-auto"
          />
        </div>
        <button className="btn btn-circle btn-ghost">
          <div className="indicator">
            <FaBell className="size-5" />
            <span className="badge badge-xs badge-primary indicator-item"></span>
          </div>
        </button>
        <div className="dropdown dropdown-end">
          <div
            tabIndex={0}
            role="button"
            className="btn btn-ghost btn-circle avatar"
          >
            <div className="w-10 rounded-full">
              <img src={user?.picture} alt="" />
            </div>
          </div>
          <ul
            tabIndex={0}
            className="mt-3 z-[1] p-2 shadow menu menu-sm dropdown-content bg-base-100 rounded-box w-52"
          >
            <li>
              <a>{user?.email}</a>
            </li>
            <li>
              <a className="justify-between">
                Profile
                <span className="badge">New</span>
              </a>
            </li>
            <li>
              <a>Settings</a>
            </li>
            <li>
              <a
                onClick={() =>
                  logout({
                    logoutParams: { returnTo: "http://localhost:5173" },
                  })
                }
              >
                Logout
              </a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Navbar;
