import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { RouterProvider } from "react-router-dom";
import { Auth0Provider } from "@auth0/auth0-react";
import { router } from "./Routes/routes";

const authDomain = import.meta.env.VITE_REACT_APP_AUTH0_DOMAIN;
const authClientId = import.meta.env.VITE_REACT_APP_AUTH0_CLIENT_ID;
const authAudience = import.meta.env.VITE_REACT_APP_AUTH0_AUDIENCE;

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <Auth0Provider
      domain={authDomain}
      clientId={authClientId}
      authorizationParams={{
        audience: authAudience,
        redirect_uri: "http://localhost:5173/app/dashboard",
      }}
    >
      <RouterProvider router={router} />
    </Auth0Provider>
  </React.StrictMode>
);
