import { Link } from "react-router-dom";
import Navbar from "../../Components/Navbar/Navbar";
import { FaAngleDoubleRight } from "react-icons/fa";

const DashboardPage = () => {
  return (
    <>
      <Navbar />
      <div className="container flex">
        <label htmlFor="my-drawer-2" className="btn btn-sm md:hidden ">
          <FaAngleDoubleRight />
        </label>
        <div className="drawer md:drawer-open">
          <input id="my-drawer-2" type="checkbox" className="drawer-toggle" />
          <div className="drawer-content flex items-start justify-start sm:pl-32 sm:pt-12">
            <div className="hero min-h-screen bg-base">
              <div className="hero-content text-center">
                <div className="max-w-md">
                  <h1 className="text-5xl font-bold">Get started</h1>
                  <p className="py-6">
                    Create a new project or choose an existing one.
                  </p>
                </div>
              </div>
            </div>
          </div>
          <div className="drawer-side rounded-e-xl border-x-2">
            <label
              htmlFor="my-drawer-2"
              aria-label="close sidebar"
              className="drawer-overlay"
            ></label>
            <ul className="menu p-4 w-72 min-h-full text-base-content bg-base-200">
              <li>
                <Link to="#" className="text-lg">
                  Create New Project
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </>
  );
};

export default DashboardPage;
