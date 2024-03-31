import { Navigate, createBrowserRouter, useParams } from "react-router-dom";
import ProjectPage from "../Pages/ProjectPage/ProjectPage";
import DashboardPage from "../Pages/DashboardPage/DashboardPage";
import IssuesList from "../Pages/ProjectPage/Components/IssuesList/IssuesList";
import Board from "../Pages/ProjectPage/Components/Board/Board";
import LandingPage from "../Pages/LandingPage/LandingPage";
import App from "../App";
import CreateProject from "../Pages/DashboardPage/Components/CreateProject/CreateProject";

// eslint-disable-next-line react-refresh/only-export-components
const ProjectPageWrapper = () => {
  const { key } = useParams();
  return <ProjectPage projectKey={key ?? ""} />;
};

export const router = createBrowserRouter([
  {
    path: "/",
    element: <LandingPage />,
  },
  {
    path: "app",
    element: <App />,
    children: [
      {
        path: "dashboard",
        element: <DashboardPage />,
        children: [
          { path: "create-project", element: <CreateProject /> },
          {},
          {},
        ],
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
    ],
  },
  {
    path: "*",
    element: <Navigate to="app/dashboard" />,
  },
]);
