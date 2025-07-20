import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './LoginScreen.css'
import './create-account.css'
import './forgot-password.css'
import './reset-password.css'
import './home-page.css'
import './nav-bar.css'
import './home-page-body.css'
import './home-page-side-bar.css'
import './posts.css'
import './change-password.css'
import './close-account.css'
import './conversations.css'
import './user-profile.css';
import './single-conversation.css';

import {QueryClient ,QueryClientProvider } from '@tanstack/react-query'
//import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import { RouterProvider } from 'react-router-dom'
import router from './routing/routes.tsx'
import { NotificationProvider } from './contexts/NotificationsContext.tsx'
import { userCurrentUserStore } from './stores/useCurrentUserStore.ts'


const client = new QueryClient();
const userId = userCurrentUserStore.getState().user.userId;
createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <QueryClientProvider client={client}>
      <NotificationProvider queryClient={client} userId={userId} url="ws://127.0.0.1:7890/notifications" >
    <RouterProvider router={router} />
     {/* <ReactQueryDevtools/> */} 
     </NotificationProvider>
    </QueryClientProvider>
  </StrictMode>,
)
