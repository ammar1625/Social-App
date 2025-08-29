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

import {QueryClient ,QueryClientProvider } from '@tanstack/react-query';
import {PersistedClient, Persister, persistQueryClient} from '@tanstack/react-query-persist-client';
import * as idbKeyval from 'idb-keyval';
//import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import { RouterProvider } from 'react-router-dom'
import router from './routing/routes.tsx'
import { NotificationProvider } from './contexts/NotificationsContext.tsx'
import { userCurrentUserStore } from './stores/useCurrentUserStore.ts'


const client = new QueryClient(/* {
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60 * 5,     // 5 minutes
      gcTime: 1000 * 60 * 30,    // 30 minutes
      retry: false,                 // avoid retries when offline
    },
  },
} */);

// Define IndexedDB persister
/* const indexedDBPersister: Persister = {
  persistClient: async (client: PersistedClient): Promise<void> => {
    await idbKeyval.set('react-query-offline-cache', client);
  },
  restoreClient: async (): Promise<PersistedClient | undefined> => {
    return await idbKeyval.get('react-query-offline-cache');
  },
  removeClient: async (): Promise<void> => {
    await idbKeyval.del('react-query-offline-cache');
  },
}; */

// Persist the client to IndexedDB
/* persistQueryClient({
  queryClient:client,
  persister: indexedDBPersister,
  maxAge: 1000 * 60 * 60 * 24, // 24 hours
  dehydrateOptions: {
    shouldDehydrateQuery: (query) =>{

        const queryKey = query.queryKey[0] ; 

        if(queryKey==='2falogin')
        {
           return false;
        }
        return query.gcTime !== 0 && query.state.status === 'success'
      },
  },
}); */
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
