import { useQuery } from "react-query";
import { GetPostsParams } from "./models";
import { getPosts } from "./services";

export const POSTS_QUERY_KEY = "POSTS_QUERY_KEY";

const usePosts = (props: GetPostsParams) => {
    return useQuery({
        queryKey: [POSTS_QUERY_KEY, props.lang, props.pageNumber, props.pageSize],
        queryFn: () => getPosts(props),
        refetchOnMount: false,
        refetchOnWindowFocus: false,
        refetchOnReconnect: true,
    });
};

export { usePosts };
