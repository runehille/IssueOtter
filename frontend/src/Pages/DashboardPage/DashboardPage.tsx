import { FaAngleDoubleRight } from "react-icons/fa";
import { useAuth0, withAuthenticationRequired } from "@auth0/auth0-react";
import { Link, Outlet } from "react-router-dom";

const DashboardPage = () => {
  return (
    <>
      <div className="container flex">
        <label htmlFor="my-drawer-2" className="btn btn-sm md:hidden ">
          <FaAngleDoubleRight />
        </label>
        <div className="drawer md:drawer-open">
          <input id="my-drawer-2" type="checkbox" className="drawer-toggle" />
          <div className="drawer-content flex items-center justify-center sm:pl-32 sm:pt-12">
            <Outlet />
          </div>
          <div className="drawer-side">
            <label
              htmlFor="my-drawer-2"
              aria-label="close sidebar"
              className="drawer-overlay"
            ></label>
            <ul className="menu p-4 w-72 min-h-full text-base-content bg-base-200"></ul>
          </div>
        </div>
      </div>
    </>
  );
};

const AuthenticatedDashboardPage = withAuthenticationRequired(DashboardPage);

export default AuthenticatedDashboardPage;
