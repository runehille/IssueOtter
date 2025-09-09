# Development Mode

This frontend can now run in development mode without requiring Auth0 authentication.

## How it works

1. **Environment Variables**: The `.env.development` file contains development-specific settings:
   - `VITE_REACT_APP_DEV_MODE=true` - Enables development mode
   - `VITE_REACT_APP_SKIP_AUTH=true` - Bypasses Auth0 authentication

2. **Dev Auth Provider**: A mock authentication provider (`DevAuthProvider`) simulates Auth0 functionality:
   - Provides a mock user (`dev@example.com`)
   - Returns mock tokens for API calls
   - Simulates authenticated state

3. **Routing Changes**: In development mode:
   - Landing page is bypassed
   - App routes are mounted at root (`/`) instead of `/app`
   - Direct access to dashboard and other features

4. **Custom Hook**: `useAuth` hook automatically uses either:
   - Real Auth0 in production
   - Mock auth provider in development

## Usage

### Development Mode (No Auth Required)
```bash
npm run dev
```
This will:
- Start the app directly at the dashboard
- Use mock authentication
- Allow full functionality without Auth0 setup

### Production Mode (Auth Required)
Set up environment variables for Auth0:
```env
VITE_REACT_APP_DEV_MODE=false
VITE_REACT_APP_SKIP_AUTH=false
VITE_REACT_APP_AUTH0_DOMAIN=your-auth0-domain
VITE_REACT_APP_AUTH0_CLIENT_ID=your-client-id
VITE_REACT_APP_AUTH0_AUDIENCE=your-audience
```

## Files Changed

- **New Files**:
  - `.env.development` - Development environment variables
  - `src/Components/DevAuthProvider/DevAuthProvider.tsx` - Mock auth provider
  - `src/Components/AuthProvider/AuthProvider.tsx` - Conditional auth wrapper
  - `src/hooks/useAuth.ts` - Custom authentication hook

- **Modified Files**:
  - `src/main.tsx` - Uses new AuthProvider
  - `src/Routes/routes.tsx` - Conditional routing logic
  - All components using `useAuth0` - Now use `useAuth`

## Mock User Data

In development mode, the following mock user is provided:
- Email: `dev@example.com`
- Picture: Placeholder image
- ID: `dev-user-id`
- Token: `dev-mock-token`

## API Integration

The mock token (`dev-mock-token`) will be sent to your backend. Make sure your backend development configuration accepts this token or implements a similar dev mode.