
import { useCallback, useEffect, useRef } from "react";
import { QueryClient } from "@tanstack/react-query";
import { invitationModel } from "../models/models";


export function useInvitationsWebSocket(url: string  , userId:string  ,queryClient:QueryClient) {
  const socketRef = useRef<WebSocket | null>(null);
  //const [messages, setMessages] = useState<invitationModel[]|notificationModel[]>([]);
 
  //const [isConnected, setIsConnected] = useState(false);

  const send = useCallback((message: string) => {
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
        const newInvitation = JSON.parse(event.data);

        // Update the React Query cache directly

        if(newInvitation.invitationStatus===1)
        {
          //queryClient.invalidateQueries({queryKey:["isInvitationExists",userId,newInvitation.senderId]});
          //queryClient.invalidateQueries({queryKey:["isInvitationExists",newInvitation.senderId,userId]});

          if(newInvitation.recieverId===userId)
          {
          queryClient.invalidateQueries({queryKey:["isInvitationExists"]});

          queryClient.setQueryData<invitationModel[]>(
            ["pending-invitations", userId],
            (old = []) => {
              // Avoid duplicates
              if (old.some(inv => inv.invitationId === newInvitation.invitationId)) {
                return old;
              }
              return [...old, newInvitation];
            }
        
       
        
        );
      }
      }
      else if(newInvitation.invitationStatus===2)
        {
          if(newInvitation.senderId===userId || newInvitation.recieverId)
          {
            queryClient.invalidateQueries({queryKey:["isFriendship-exists",userId,newInvitation.senderId]});
            queryClient.invalidateQueries({queryKey:["isFriendship-exists",newInvitation.senderId,userId]});
            queryClient.invalidateQueries({queryKey:["friends-list"]});
          }
        }
        else if(newInvitation.invitationStatus===3)
          {
            //queryClient.invalidateQueries({queryKey:["isInvitationExists",userId,newInvitation.senderId]});
            //queryClient.invalidateQueries({queryKey:["isInvitationExists",newInvitation.senderId,userId]});
            if(newInvitation.senderId===userId || newInvitation.recieverId)
              {
                queryClient.invalidateQueries({queryKey:["isInvitationExists"]});
              }

          }
          else if(newInvitation.invitationStatus===4)
            {
              if(newInvitation.senderId===userId || newInvitation.recieverId)
                {
                  queryClient.invalidateQueries({queryKey:["isInvitationExists"]});
                  queryClient.invalidateQueries({queryKey: ["pending-invitations",userId]});
                }
              //queryClient.invalidateQueries({queryKey:["isInvitationExists",newInvitation.recieverId,userId]});

               // Remove from pending-invitations list directly
              /* queryClient.setQueryData<invitationModel[]>(
                ["pending-invitations", userId],
                (old = []) => old.filter(inv => inv.invitationId !== newInvitation.invitationId)
              ); */
          
            }
 
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

  return {  send};
}

