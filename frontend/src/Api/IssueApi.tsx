import axios from "axios";
import { IssueGet, IssuePost } from "../Models/Issue";
import BaseApiService from "./BaseApiService";

export const getAllIssues = async (accessToken: string, key: string) => {
  try {
    const data = await BaseApiService.get<IssueGet>(`/issue/${key}`, {
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
        projectKey: issue.projectKey,
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
