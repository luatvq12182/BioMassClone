export interface IPost {
    id: number;
    postId: number;
    languageId: number | null;
    categoryId: number | null;
    thumbnail: string;
    title: string;
    body: string;
    shortDescription: string;
    createdDate: string;
    views: number;
    author: string | null;
}

export interface PagingableResponse<T> {
    hasNextPage: boolean;
    hasPreviousPage: boolean;
    items: T[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
}

export type GetPostsParams = {
    lang?: string;
    pageSize?: number;
    pageNumber?: number;
};
