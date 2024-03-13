import { createBrowserRouter } from "react-router-dom";
import ProjectPage from "../Pages/ProjectPage/ProjectPage";
import DashboardPage from "../Pages/DashboardPage/DashboardPage";
import IssuesList from "../Components/IssuesList/IssuesList";
import Board from "../Components/Board/Board";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <DashboardPage />,
  },
  {
    path: "dashboard",
    element: <DashboardPage />,
  },
  {
    path: "project",
    element: <ProjectPage />,
    children: [
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
]);
