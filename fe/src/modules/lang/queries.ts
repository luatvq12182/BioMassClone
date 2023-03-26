import { useQuery } from "react-query";
import { getLangs } from "./services";

export const LANGS_QUERY_KEY = "LANGS_QUERY_KEY";

const useLangs = () => {
    return useQuery({
        queryKey: LANGS_QUERY_KEY,
        queryFn: getLangs,
        refetchOnMount: false,
        refetchOnWindowFocus: false,
        refetchOnReconnect: true,
    });
};

export { useLangs };
