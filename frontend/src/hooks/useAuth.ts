import { useAuth0 } from '@auth0/auth0-react';
import { useDevAuth0 } from '../Components/DevAuthProvider/DevAuthProvider';

export const useAuth = () => {
  const isDevMode = import.meta.env.VITE_REACT_APP_DEV_MODE === 'true';
  const skipAuth = import.meta.env.VITE_REACT_APP_SKIP_AUTH === 'true';

  if (isDevMode && skipAuth) {
    return useDevAuth0();
  }

  return useAuth0();
};