import { useMutation, useQueryClient } from "react-query";
import { uploadFile } from "@/services/file";
import { FILES_QUERY_KEY } from "../queries/useFiles";

type Props = {
    onSuccess?: Function;
    onError?: Function;
};

const useUploadFile = ({ onSuccess, onError }: Props) => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: uploadFile,
        onSuccess: (data) => {
            queryClient.invalidateQueries(FILES_QUERY_KEY);
            if (onSuccess) {
                onSuccess(data);
            }
        },
        onError: (error) => {
            console.log("Error::", error);
            if (onError) {
                onError(error);
            }
        },
    });
};

export default useUploadFile;
