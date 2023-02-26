import { useMutation, useQueryClient } from "react-query";
import { updateCategory } from "@/services/category";
import { CATEGORIES_QUERY_KEY } from "../queries/useCategories";

type Props = {
    onSuccess?: Function;
    onError?: Function;
};

const useUpdateCategory = ({ onSuccess, onError }: Props) => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: updateCategory,
        onSuccess: (data) => {
            queryClient.invalidateQueries(CATEGORIES_QUERY_KEY);
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

export default useUpdateCategory;
