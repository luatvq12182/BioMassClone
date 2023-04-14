import { format } from "date-fns";
import { useNavigate } from "react-router-dom";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { Card } from "primereact/card";
import { TabPanel, TabView } from "primereact/tabview";

import {
    IPost,
    PagingableResponse,
    PostProvider,
    useDeletePost,
    // usePosts,
} from "@/modules/post";
import { useLangs } from "@/modules/lang";
import { showConfirm } from "@/utils";

const PostList = () => {
    const navigate = useNavigate();
    // const { refetch } = usePosts({ lang: "" });
    const { mutate: deletePost, isLoading: isDeleting } = useDeletePost({
        onSuccess: () => {
            window.location.reload();
        },
    });

    const handleClickDelete = (id: number) => {
        showConfirm(() => deletePost(id));
    };

    const columns = [
        {
            field: "index",
            header: "#",
            body: (_: IPost, e: any) => {
                return e.rowIndex + 1;
            },
        },
        {
            field: "thumbnail",
            header: "Thumbnail",
            style: { width: "170px" },
            body: (e: IPost) => {
                return (
                    <img
                        className='w-[150px]'
                        src={import.meta.env.VITE_SERVICE + e.thumbnail}
                        alt=''
                    />
                );
            },
        },
        {
            field: "title",
            header: "Title",
        },
        {
            field: "author",
            header: "Author",
        },
        {
            field: "createdDate",
            header: "Date created",
            body: (e: IPost) => {
                return format(new Date(e.createdDate), "dd/MM/yyyy hh:mm:ss");
            },
        },
        {
            field: "action",
            header: "#",
            style: { width: "150px", textAlign: "center" },
            body: (e: IPost) => {
                return (
                    <div>
                        <Button
                            icon='pi pi-pencil'
                            rounded
                            text
                            aria-label='Filter'
                            onClick={() => {
                                navigate(`/admin/post/edit/${e.id}`);
                            }}
                        />
                        <Button
                            disabled={isDeleting}
                            onClick={() => handleClickDelete(e.postId || e.id)}
                            icon='pi pi-trash'
                            rounded
                            text
                            severity='danger'
                            aria-label='Bookmark'
                        />
                    </div>
                );
            },
        },
    ];
    const { data: langs } = useLangs();

    const render = (
        posts: PagingableResponse<IPost> | null,
        isLoading?: boolean
    ) => {
        return (
            <DataTable
                value={posts?.items}
                loading={isLoading}
                size='small'
                showGridlines
                tableStyle={{ minWidth: "50rem" }}
            >
                {columns.map((col) => {
                    return (
                        <Column
                            key={col.field}
                            {...(col as any)}
                        />
                    );
                })}
            </DataTable>
        );
    };

    return (
        <Card
            title={
                <div className='flex justify-between'>
                    <h3>Post list</h3>

                    <Button
                        label='New post'
                        icon='pi pi-plus'
                        onClick={() => {
                            navigate("/post/new");
                        }}
                    />
                </div>
            }
        >
            <TabView>
                <TabPanel header='Standard'>
                    <PostProvider render={render} />
                </TabPanel>

                {langs?.data?.map((lang) => {
                    return (
                        <TabPanel
                            key={lang.id}
                            header={lang.name}
                        >
                            <PostProvider
                                render={render}
                                lang={lang.code}
                            />
                        </TabPanel>
                    );
                })}
            </TabView>
        </Card>
    );
};

export default PostList;
