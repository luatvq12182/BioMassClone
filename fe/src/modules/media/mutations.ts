import { useMutation, useQueryClient } from "react-query";
import { MEDIA_QUERY_KEY } from "./queries";
import { addToSlider, uploadFile } from "./services";

type Props = {
    onSuccess?: Function;
    onError?: Function;
};

const useUploadFile = (props?: Props) => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: uploadFile,
        onSuccess: (data) => {
            queryClient.invalidateQueries(MEDIA_QUERY_KEY);

            if (props?.onSuccess) {
                props.onSuccess(data);
            }
        },
        onError: (error) => {
            console.log("Error::", error);
            if (props?.onError) {
                props.onError(error);
            }
        },
    });
};

const useAddToSlider = (props?: Props) => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: addToSlider,
        onSuccess: (data) => {
            queryClient.invalidateQueries(MEDIA_QUERY_KEY);

            if (props?.onSuccess) {
                props.onSuccess(data);
            }
        },
        onError: (error) => {
            console.log("Error::", error);
            if (props?.onError) {
                props.onError(error);
            }
        },
    });
}

export { useUploadFile, useAddToSlider };
