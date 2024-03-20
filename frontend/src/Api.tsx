import axios from "axios";
import { IssueGet } from "./Models/Issue";
import { ProjectGet } from "./Models/Project";

export const getAllIssues = async () => {
  try {
    const data = await axios.get<IssueGet>("http://localhost:3000/issue");
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

export const getAllProjects = async () => {
  try {
    const data = await axios.get<ProjectGet>("http://localhost:3000/project");
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
