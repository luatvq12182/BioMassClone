import { useQuery } from "react-query";
import { getCategories, getCategory } from "./services";

export const CATEGORIES_QUERY_KEY = "CATEGORIES_QUERY_KEY";
export const CATEGORY_QUERY_KEY = "CATEGORY_QUERY_KEY";

const useCategories = (langCode?: string) => {
    return useQuery({
        queryKey: [CATEGORIES_QUERY_KEY, langCode],
        queryFn: () => getCategories(langCode),
        refetchOnMount: false,
        refetchOnWindowFocus: false,
        refetchOnReconnect: true,
    });
};

const useCategory = (id: number) => {
    return useQuery({
        queryKey: [CATEGORY_QUERY_KEY, id],
        queryFn: () => getCategory(id),
        refetchOnMount: false,
        refetchOnWindowFocus: false,
        refetchOnReconnect: true,
        enabled: id > -1
    });
};

export { useCategories, useCategory };
