import { ReactNode } from "react";
import { GetPostsParams, IPost, PagingableResponse } from "./models";
import { usePosts } from "./queries";

type Props = {
    render: (
        posts: PagingableResponse<IPost> | null,
        isLoading?: boolean
    ) => ReactNode;
};

const PostProvider = (props: Props & GetPostsParams) => {
    const { data: posts, isFetching } = usePosts({
        lang: props.lang,
        pageSize: props.pageSize,
        pageNumber: props.pageNumber,
    });

    return <>{props.render(posts?.data || null, isFetching)}</>;
};

export { PostProvider };
