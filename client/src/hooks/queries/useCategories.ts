import { useQuery } from "react-query";
import { getCategories } from "@/services/category";

export const CATEGORIES_QUERY_KEY = "CATEGORIES_QUERY_KEY";

const useCategories = () => {
    return useQuery({
        queryKey: CATEGORIES_QUERY_KEY,
        queryFn: getCategories,
        refetchOnMount: false,
        refetchOnWindowFocus: false,
        refetchOnReconnect: true,
    });
};

export default useCategories;
