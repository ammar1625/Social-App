
import { useCallback, useEffect, useRef } from "react";
import { QueryClient } from "@tanstack/react-query";
import { webSocketMessageModel } from "../models/models";


export function useMessagesWebSocket(url: string  , userId:string  , conversationId:string ,queryClient:QueryClient) {
  const socketRef = useRef<WebSocket | null>(null);


  const sendMessage = useCallback((message: string) => {
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
        const newMessage:webSocketMessageModel = JSON.parse(event.data);
        
        // Update the React Query cache directly
        if(newMessage.senderId===userId || newMessage.recieverId === userId)
        {
           queryClient.setQueryData<webSocketMessageModel[]>(
            ['messages',conversationId],
            (old = []) => {
              // Avoid duplicates
              if (old.some(n => n.messageId === newMessage.messageId)) {
                return old;
              }
              return [...old, newMessage];
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
  }, [url , conversationId]);

  return {  sendMessage};
}

