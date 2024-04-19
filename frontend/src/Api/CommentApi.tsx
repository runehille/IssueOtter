import axios from "axios";
import { CommentPost } from "../Models/Comment";
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
    } else {
      console.log(error);
      return "An unexpected error occurred. Please try again later.";
    }
  }
};
