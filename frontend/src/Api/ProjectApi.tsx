import axios from "axios";
import { ProjectGet, ProjectPost, ProjectUpdate } from "../Models/Project";
import BaseApiService from "./BaseApiService";

export const getAllProjects = async (accessToken: string) => {
  try {
    const data = await BaseApiService.get<ProjectGet[]>("/project", {
      headers: { Authorization: `Bearer ${accessToken}` },
    });
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log(error.message);
    }
  }
};

export const getProjectByKey = async (accessToken: string, key: string) => {
  try {
    const data = await BaseApiService.get<ProjectGet>(`/project/${key}`, {
      headers: { Authorization: `Bearer ${accessToken}` },
    });
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log(error.message);
    }
  }
};

export const postProject = async (
  accessToken: string,
  project: ProjectPost
) => {
  try {
    const data = await BaseApiService.post<ProjectPost>(
      "/project",
      {
        key: project.key,
        title: project.title,
        description: project.description,
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
    }
  }
};

export const updateProject = async (
  accessToken: string,
  key: string,
  project: ProjectUpdate
) => {
  try {
    const data = await BaseApiService.put<ProjectGet>(
      `/project/${key}`,
      {
        title: project.title,
        description: project.description,
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
    }
  }
};

export const deleteProject = async (accessToken: string, key: string) => {
  try {
    const data = await BaseApiService.delete(`/project/${key}`, {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    });
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log(error.message);
    }
  }
};
