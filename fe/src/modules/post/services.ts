import { httpClient } from "../httpClient";
import { GetPostsParams, IPost, PagingableResponse } from "./models";

const getPosts = ({ lang, pageSize, pageNumber }: GetPostsParams) => {
    return httpClient.get<PagingableResponse<IPost>>(
        `/posts${lang ? `?Lang=${lang}` : ""}${
            pageSize ? `&Pagesize=${pageSize}` : ""
        }${pageNumber ? `&PageNumber=${pageNumber}` : ""}`
    );
};

const getPost = (id: number, lang: string) => {
    return httpClient.get<IPost[]>(`/posts/${id}${lang ? `?lang=${lang}` : ''}`)
}

const createPost = (payload: IPost[]) => {
    return httpClient.post('/posts', payload)
}

export { getPosts, getPost, createPost };
