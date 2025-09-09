import React, { createContext, useContext, ReactNode } from 'react';

// Mock Auth0 context for development
interface DevAuth0Context {
  user: {
    email: string;
    picture: string;
    sub: string;
  } | undefined;
  isAuthenticated: boolean;
  isLoading: boolean;
  getAccessTokenSilently: () => Promise<string>;
  loginWithRedirect: (options?: any) => Promise<void>;
  logout: (options?: any) => void;
}

const DevAuth0Context = createContext<DevAuth0Context | undefined>(undefined);

interface DevAuthProviderProps {
  children: ReactNode;
}

export const DevAuthProvider: React.FC<DevAuthProviderProps> = ({ children }) => {
  const mockUser = {
    email: 'dev@example.com',
    picture: 'https://picsum.photos/200',
    sub: 'dev-user-id'
  };

  const mockAuth: DevAuth0Context = {
    user: mockUser,
    isAuthenticated: true,
    isLoading: false,
    getAccessTokenSilently: async () => {
      // Return a mock token for development
      return 'dev-mock-token';
    },
    loginWithRedirect: async () => {
      console.log('Mock login redirect');
    },
    logout: () => {
      console.log('Mock logout');
    }
  };

  return (
    <DevAuth0Context.Provider value={mockAuth}>
      {children}
    </DevAuth0Context.Provider>
  );
};

// Custom hook to use the dev auth context
export const useDevAuth0 = (): DevAuth0Context => {
  const context = useContext(DevAuth0Context);
  if (context === undefined) {
    throw new Error('useDevAuth0 must be used within a DevAuthProvider');
  }
  return context;
};