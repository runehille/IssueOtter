import axios from "axios";
import { CommentPost, CommentUpdate } from "../Models/Comment";
import BaseApiService from "./BaseApiService";

export const postComment = async (
  accessToken: string,
  comment: CommentPost
) => {
  try {
    const data = await BaseApiService.post<CommentPost>(
      "/comment",
      {
        content: comment.comment,
        issueKey: comment.issueKey,
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

export const updateComment = async (
  accessToken: string,
  commentId: number,
  comment: CommentUpdate
) => {
  try {
    const data = await BaseApiService.put<CommentUpdate>(
      `/comment/${commentId}`,
      {
        content: comment.content,
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

export const deleteComment = async (
  accessToken: string,
  commentId: number
) => {
  try {
    const data = await BaseApiService.delete(
      `/comment/${commentId}`,
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
