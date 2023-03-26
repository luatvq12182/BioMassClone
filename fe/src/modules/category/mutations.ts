import { useMutation, useQueryClient } from "react-query";
import { CATEGORIES_QUERY_KEY } from "./queries";
import { createCategory, deleteCategory, updateCategory } from "./services";

type Props = {
    onSuccess?: Function;
    onError?: Function;
};

const useCreateCategory = (props?: Props) => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: createCategory,
        onSuccess: (data) => {
            queryClient.invalidateQueries(CATEGORIES_QUERY_KEY);
            if (props?.onSuccess) {
                props?.onSuccess(data);
            }
        },
        onError: (error) => {
            console.log("Error::", error);
            if (props?.onError) {
                props?.onError(error);
            }
        },
    });
};

const useUpdateCategory = (props?: Props) => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: updateCategory,
        onSuccess: (data) => {
            queryClient.invalidateQueries(CATEGORIES_QUERY_KEY);
            if (props?.onSuccess) {
                props?.onSuccess(data);
            }
        },
        onError: (error) => {
            console.log("Error::", error);
            if (props?.onError) {
                props?.onError(error);
            }
        },
    });
};

const useDeleteCategory = (props?: Props) => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: deleteCategory,
        onSuccess: (data) => {
            queryClient.invalidateQueries(CATEGORIES_QUERY_KEY);
            if (props?.onSuccess) {
                props?.onSuccess(data);
            }
        },
        onError: (error) => {
            console.log("Error::", error);
            if (props?.onError) {
                props?.onError(error);
            }
        },
    });
};

export default { useCreateCategory, useUpdateCategory, useDeleteCategory };
