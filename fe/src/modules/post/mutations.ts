import { useMutation } from "react-query";
import { createPost } from "./services";

type Props = {
    onSuccess?: Function;
    onError?: Function;
};

const useCreatePost = (props?: Props) => {
    return useMutation({
        mutationFn: createPost,
        onSuccess: (data) => {
            if (props?.onSuccess) {
                props?.onSuccess(data);
            }
        },
        onError: (error) => {
            console.log("Error::", { error });
            if (props?.onError) {
                props?.onError(error);
            }
        },
    });
};

export { useCreatePost }