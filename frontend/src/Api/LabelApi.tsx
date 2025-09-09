import axios from "axios";
import { Label, LabelCreate, LabelUpdate } from "../Models/Label";
import BaseApiService from "./BaseApiService";

export const getAllLabels = async (accessToken: string) => {
  try {
    const data = await BaseApiService.get<Label[]>("/label", {
      headers: { Authorization: `Bearer ${accessToken}` },
    });
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log(error.message);
    }
  }
};

export const getLabelsByProjectId = async (accessToken: string, projectId: number) => {
  try {
    const data = await BaseApiService.get<Label[]>(`/label/project/${projectId}`, {
      headers: { Authorization: `Bearer ${accessToken}` },
    });
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log(error.message);
    }
  }
};

export const getLabelById = async (accessToken: string, id: number) => {
  try {
    const data = await BaseApiService.get<Label>(`/label/${id}`, {
      headers: { Authorization: `Bearer ${accessToken}` },
    });
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log(error.message);
    }
  }
};

export const createLabel = async (accessToken: string, label: LabelCreate) => {
  try {
    const data = await BaseApiService.post<Label>(
      "/label",
      {
        name: label.name,
        color: label.color,
        description: label.description,
        projectId: label.projectId,
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

export const updateLabel = async (accessToken: string, id: number, label: LabelUpdate) => {
  try {
    const data = await BaseApiService.put<Label>(
      `/label/${id}`,
      {
        name: label.name,
        color: label.color,
        description: label.description,
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

export const deleteLabel = async (accessToken: string, id: number) => {
  try {
    const data = await BaseApiService.delete(`/label/${id}`, {
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