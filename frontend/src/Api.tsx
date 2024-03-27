import axios from "axios";
import { IssueGet, IssuePost } from "./Models/Issue";
import { ProjectGet } from "./Models/Project";

export const getAllIssues = async (accessToken: string) => {
  try {
    const data = await axios.get<IssueGet>("http://localhost:5285/api/issue", {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    });
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log(error.message);
    } else {
      console.log(error);
      return "An unexpected error occurred. Please try again later.";
    }
  }
};

export const postIssue = async (accessToken: string, issue: IssuePost) => {
  try {
    const data = await axios.post<IssuePost>(
      "http://localhost:5285/api/issue",
      {
        title: issue.title,
        content: issue.content,
        assigneeId: 1,
        createdById: 1,
        projectId: 1,
      },
      {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      }
    );
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log(error.message);
    } else {
      console.log(error);
      return "An unexpected error occurred. Please try again later.";
    }
  }
};

export const getAllProjects = async (accessToken: string) => {
  try {
    const data = await axios.get<ProjectGet>(
      "http://localhost:5285/api/project",
      { headers: { Authorization: `Bearer ${accessToken}` } }
    );
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log(error.message);
    } else {
      console.log(error);
      return "An unexpected error occurred. Please try again later.";
    }
  }
};

export const getProjectById = async (accessToken: string, key: string) => {
  try {
    const data = await axios.get<ProjectGet>(
      "http://localhost:5285/api/project",
      { headers: { Authorization: `Bearer ${accessToken}` } }
    );
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log(error.message);
    } else {
      console.log(error);
      return "An unexpected error occurred. Please try again later.";
    }
  }
};
