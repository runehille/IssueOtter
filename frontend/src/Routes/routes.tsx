/* eslint-disable react-refresh/only-export-components */
import { Navigate, createBrowserRouter, useParams } from "react-router-dom";
import ProjectPage from "../Pages/ProjectPage/ProjectPage";
import DashboardPage from "../Pages/DashboardPage/DashboardPage";
import IssuesList from "../Pages/ProjectPage/Components/IssuesList/IssuesList";
import Board from "../Pages/ProjectPage/Components/Board/Board";
import LandingPage from "../Pages/LandingPage/LandingPage";
import App from "../App";
import CreateProject from "../Pages/DashboardPage/Components/CreateProject/CreateProject";
import Default from "../Pages/DashboardPage/Components/Default/Default";
import Issue from "../Pages/ProjectPage/Components/Issue/Issue";
import Settings from "../Pages/ProjectPage/Components/Settings/Settings";

// eslint-disable react-refresh/only-export-components
const ProjectPageWrapper = () => {
  const { key } = useParams();
  return <ProjectPage projectKey={key ?? ""} />;
};

const IssueWrapper = () => {
  const { key } = useParams();
  return <Issue issueKey={key ?? ""} />;
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
          { path: "", element: <Default /> },
          { path: "create-project", element: <CreateProject /> },
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
            path: "issues/:key",
            element: <IssueWrapper />,
          },
          {
            path: "board",
            element: <Board />,
          },
          {
            path: "settings",
            element: <Settings />,
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
