import { Link } from "react-router-dom";
import IssuesList from "../../Components/IssuesList/IssuesList";
import Navbar from "../../Components/Navbar/Navbar";
import Sidebar from "../../Components/Sidebar/Sidebar";

const sidebarContent = () => {
  return (
    <>
      <li>
        <Link to="#" className="text-lg">
          Issues
        </Link>
      </li>
      <li>
        <Link to="#" className="text-lg">
          Sprint
        </Link>
      </li>
    </>
  );
};

const ProjectPage = () => {
  return (
    <>
      <Navbar />
      <Sidebar mainContent={IssuesList} sidebarContent={sidebarContent} />
    </>
  );
};

export default ProjectPage;
