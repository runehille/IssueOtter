import { Navigate, createBrowserRouter, useParams } from "react-router-dom";
import ProjectPage from "../Pages/ProjectPage/ProjectPage";
import DashboardPage from "../Pages/DashboardPage/DashboardPage";
import IssuesList from "../Components/IssuesList/IssuesList";
import Board from "../Components/Board/Board";
import LandingPage from "../Pages/LandingPage/LandingPage";

const ProjectPageWrapper = () => {
  const { key } = useParams();
  return <ProjectPage key={key} />;
};

export const router = createBrowserRouter([
  {
    path: "/",
    element: <LandingPage />,
  },
  {
    path: "dashboard",
    element: <DashboardPage />,
  },
  {
    path: "project/:key",
    element: <ProjectPageWrapper />,
    children: [
      {
        path: "",
        element: <Navigate to="issues" />,
      },
      {
        path: "issues",
        element: <IssuesList />,
      },
      {
        path: "board",
        element: <Board />,
      },
    ],
  },
  {
    path: "*",
    element: <Navigate to="/dashboard" />,
  },
]);
