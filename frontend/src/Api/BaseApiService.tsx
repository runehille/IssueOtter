import axios from "axios";

const BaseApiService = axios.create({
  baseURL: "http://localhost:5285/api",
  headers: {
    "Content-type": "application/json",
  },
});

export default BaseApiService;
