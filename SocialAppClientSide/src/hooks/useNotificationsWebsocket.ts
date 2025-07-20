
import { useCallback, useEffect, useRef } from "react";
import { QueryClient } from "@tanstack/react-query";
import {  notificationModel } from "../models/models";


export function useNotificationsWebSocket(url: string  , userId:string  ,queryClient:QueryClient) {
  const socketRef = useRef<WebSocket | null>(null);


  const sendNotification = useCallback((message: string) => {
    if (socketRef.current && socketRef.current.readyState === WebSocket.OPEN) {
      socketRef.current.send(message);
    } else {
      //console.warn("WebSocket is not connected");
    }
  }, []);

  useEffect(() => {
    const socket = new WebSocket(url);
    socketRef.current = socket;

    /* socket.onopen = () => {
      console.log("✅ WebSocket connected");
      //setIsConnected(true);
    }; */

    socket.onmessage = (event) => {
      //setMessages(prev => [...prev, event.data]);
      //setData([...data,JSON.parse(event.data)]);
      try {
        const newNotification = JSON.parse(event.data);
        
        // Update the React Query cache directly
        if(newNotification.notificationRecieverId===userId && newNotification.userId !==userId)
        {
           queryClient.setQueryData<notificationModel[]>(
            ["unread-notifications", userId],
            (old = []) => {
              // Avoid duplicates
              if (old.some(n => n.notificationId === newNotification.notificationId)) {
                return old;
              }
              return [...old, newNotification];
            }
       
        
        ); 
    }
      
       // queryClient.invalidateQueries({queryKey:["unread-notifications", userId]});
        
 
            }
       catch (error) {
        console.error("Error parsing WebSocket message", error);
      }
    };
      
    

/*     socket.onclose = () => {
      console.log("❌ WebSocket disconnected");
      setIsConnected(false);
    };
 */
    socket.onerror = (error) => {
      console.error("WebSocket error", error);
    };

    return () => {
        if (socket.readyState === WebSocket.OPEN || socket.readyState === WebSocket.CONNECTING) {
          socket.close();
        }
    };
  }, [url]);

  return {  sendNotification};
}

