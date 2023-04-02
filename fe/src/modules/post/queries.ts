import { useQuery } from "react-query";
import { GetPostParams, GetPostsParams } from "./models";
import { getPost, getPosts } from "./services";

export const POSTS_QUERY_KEY = "POSTS_QUERY_KEY";
export const POST_QUERY_KEY = "POST_QUERY_KEY";

const usePosts = (props: GetPostsParams) => {
    return useQuery({
        queryKey: [
            POSTS_QUERY_KEY,
            props.lang,
            props.pageNumber,
            props.pageSize,
        ],
        queryFn: () => getPosts(props),
        refetchOnMount: false,
        refetchOnWindowFocus: false,
        refetchOnReconnect: true,
    });
};

const usePost = (props: GetPostParams) => {
    const { id, lang, onSuccess } = props;

    return useQuery({
        queryKey: [POST_QUERY_KEY, id, lang],
        queryFn: () => getPost(id, lang),
        refetchOnMount: false,
        refetchOnWindowFocus: false,
        refetchOnReconnect: true,
        onSuccess: (data) => {
            if (onSuccess) {
                onSuccess(data.data);
            }
        },
    });
};

export { usePosts, usePost };
