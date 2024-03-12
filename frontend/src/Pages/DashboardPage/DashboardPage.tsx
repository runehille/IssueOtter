import { Link } from "react-router-dom";
import Navbar from "../../Components/Navbar/Navbar";
import Sidebar from "../../Components/Sidebar/Sidebar";

const mainContent = () => {
  return (
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
  );
};

const sidebarContent = () => {
  return (
    <li>
      <Link to="#" className="text-xl">
        Create New Project
      </Link>
    </li>
  );
};

const DashboardPage = () => {
  return (
    <>
      <Navbar />
      <Sidebar sidebarContent={sidebarContent} mainContent={mainContent} />
    </>
  );
};

export default DashboardPage;
