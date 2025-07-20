import { QueryClient, useMutation } from "@tanstack/react-query";
import { likeToAddModel, postModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function deleteLike(model:likeToAddModel)
{
    return axiosInstance.delete<boolean>(`/like/delete`,{
        data:model
    }).then(res=>res.data);
}

export function useDeleteLike(queryClient:QueryClient , userId:string,targetUserId:string ,type:number )
{
    return useMutation({
        mutationFn:deleteLike,
        onMutate:(model:likeToAddModel)=>{
            const {postId}  =model;

             if(type===1)
            { 
                 // Cancel any outgoing refetches for this query
                queryClient.cancelQueries({ queryKey: ["posts",userId] });
                // Take a snapshot of the current post data
                const previousPosts = queryClient.getQueryData<postModel[]>(["posts",userId]);

                // Optimistically update the cache
                queryClient.setQueryData(["posts", userId], (old: postModel[] | undefined) => {
                    if (!old) return old;
                
                    return old.map((post) => {
                    if (post.postId !== postId) return post;
                
                    return {
                        ...post,
                        isLiked: post.isLiked === "yes" ? "no" : "yes",
                        likesCount: post.isLiked === "yes" ? post.likesCount - 1 : post.likesCount + 1,
                    };
                    });
                });

                return { previousPosts};
            }

             else if(type===2)
            {
                queryClient.cancelQueries({ queryKey: ["current-user-posts",targetUserId] });
                 //take a snapshot of the current user posts data
                 const previousCurrentUserPosts = queryClient.getQueryData<postModel[]>(["current-user-posts",targetUserId]);
            

                queryClient.setQueryData(["current-user-posts",targetUserId], (old: postModel[] | undefined) => {
                if (!old) return old;
                
                return old.map((post) => {
                    if (post.postId !== postId) return post;
                
                    return {
                    ...post,
                    isLiked: post.isLiked === "yes" ? "no" : "yes",
                    likesCount: post.isLiked === "yes" ? post.likesCount - 1 : post.likesCount + 1,
                    };
                });
                });

                // Return context to be used in onError
                 return { previousCurrentUserPosts };
            } 
              
          },
                  
                      onError: (_err, _userId, context) => {
                        if (context?.previousPosts) {
                          queryClient.setQueryData(["posts", userId], context.previousPosts);
                        }
        
                        if(context?.previousCurrentUserPosts)
                        {
                            queryClient.setQueryData(["current-user-posts",userId],context.previousCurrentUserPosts);
                        } 
                      },
    });
}