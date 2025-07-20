// contexts/NotificationContext.tsx
import { createContext, useContext } from "react";
import { useNotificationsWebSocket } from "../hooks/useNotificationsWebsocket";
import { QueryClient } from "@tanstack/react-query";

type NotificationContextType = {
  sendNotification: (message: string) => void;
};

type NotificationProviderProps = {
    children: React.ReactNode;
    userId: string;
    queryClient: QueryClient;
    url: string;
  };

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

export const useNotification = () => {
  const context = useContext(NotificationContext);
  if (!context) {
    throw new Error("useNotification must be used within a NotificationProvider");
  }
  return context;
};

export const NotificationProvider: React.FC<NotificationProviderProps> = ({ children , userId , queryClient,url }) => {
  const { sendNotification } = useNotificationsWebSocket(url, userId, queryClient);

  return (
    <NotificationContext.Provider value={{ sendNotification }}>
      {children}
    </NotificationContext.Provider>
  );
};