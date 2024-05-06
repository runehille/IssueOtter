import axios from "axios";

const baseUrl = import.meta.env.VITE_REACT_APP_API_BASEURL;

const BaseApiService = axios.create({
  baseURL: `${baseUrl}`,
  headers: {
    "Content-type": "application/json",
  },
});

export default BaseApiService;
