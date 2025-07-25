import { useMutation } from "@tanstack/react-query";
import axiosInstance from "../services/apiClient";

function saveMessageMedia(image:File)
{
    const data = new FormData();
    data.append('Image' ,image);
    return axiosInstance.post<string>(`Messages/save-image`,data).then(res=>res.data);
}

export function useSaveImageMedia()
{
    return useMutation({
        mutationFn:saveMessageMedia
    });
}