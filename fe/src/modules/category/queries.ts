import { useQuery } from "react-query";
import { getCategories } from "./services";

export const CATEGORIES_QUERY_KEY = "CATEGORIES_QUERY_KEY";

const useCategories = (langCode?: string) => {
    return useQuery({
        queryKey: [CATEGORIES_QUERY_KEY, langCode],
        queryFn: () => getCategories(langCode),
        refetchOnMount: false,
        refetchOnWindowFocus: false,
        refetchOnReconnect: true,
    });
};

export { useCategories };
