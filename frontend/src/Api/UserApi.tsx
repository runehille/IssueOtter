import axios from "axios";
import { UserPost } from "../Models/User";
import BaseApiService from "./BaseApiService";

export const postUser = async (accessToken: string, user: UserPost) => {
  try {
    const data = await BaseApiService.post<UserPost>(
      "/user",
      {
        email: user.email,
        firstname: user.firstname,
        lastname: user.lastname,
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
