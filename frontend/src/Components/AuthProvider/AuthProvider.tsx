import React, { ReactNode } from 'react';
import { Auth0Provider } from '@auth0/auth0-react';
import { DevAuthProvider } from '../DevAuthProvider/DevAuthProvider';

interface AuthProviderProps {
  children: ReactNode;
}

const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const isDevMode = import.meta.env.VITE_REACT_APP_DEV_MODE === 'true';
  const skipAuth = import.meta.env.VITE_REACT_APP_SKIP_AUTH === 'true';

  if (isDevMode && skipAuth) {
    return <DevAuthProvider>{children}</DevAuthProvider>;
  }

  // Production Auth0 configuration
  const authDomain = import.meta.env.VITE_REACT_APP_AUTH0_DOMAIN;
  const authClientId = import.meta.env.VITE_REACT_APP_AUTH0_CLIENT_ID;
  const authAudience = import.meta.env.VITE_REACT_APP_AUTH0_AUDIENCE;
  const rootUrl = import.meta.env.VITE_REACT_APP_ROOTURL;

  return (
    <Auth0Provider
      domain={authDomain}
      clientId={authClientId}
      authorizationParams={{
        audience: authAudience,
        redirect_uri: `${rootUrl}/dashboard`,
      }}
    >
      {children}
    </Auth0Provider>
  );
};

export default AuthProvider;