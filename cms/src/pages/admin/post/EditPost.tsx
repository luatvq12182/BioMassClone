import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Button } from "primereact/button";
import { Card } from "primereact/card";
import { InputText } from "primereact/inputtext";
import { Dropdown } from "primereact/dropdown";
import { TabPanel, TabView } from "primereact/tabview";

import Form from "./components/Form";
import { IPost, useUpdatePost, usePost } from "@/modules/post";
import { CategoryProvider, ICategory } from "@/modules/category";
import Media from "@/components/Media";
import { IMedia } from "@/modules/media";
import { useLangs } from "@/modules/lang";

const EditPost = () => {
    const navigate = useNavigate();
    const { id: postIdParam } = useParams();
    const { data: langs } = useLangs();
    const { data: postDetail, isError } = usePost({
        id: Number(postIdParam),
        onSuccess: (data: IPost[]) => {
            setPayload(data);
        },
    });
    const { mutate: updatePost, isLoading } = useUpdatePost(
        Number(postIdParam),
        {
            onSuccess: () => {
                window.location.href = '/admin/post'
            },
        }
    );

    const [payload, setPayload] = useState<IPost[]>([]);
    const [isOpenMedia, setIsOpenMedia] = useState<boolean>(false);
    const [media, setMedia] = useState<IMedia | null>(null);
    const [selectedCategory, setSelectedCategory] = useState<ICategory | null>(
        null
    );
    const [author, setAuthor] = useState<string>("");

    const handleChange =
        (langId: number | null) => (field: keyof IPost) => (e: any) => {
            setPayload((preState: any) => {
                if (!preState.find((p: any) => p.languageId === langId)) {
                    return [
                        ...preState,
                        {
                            languageId: langId,
                            [field]: e,
                        },
                    ];
                }

                return preState.map((p: any) => {
                    if (p.languageId === langId) {
                        return {
                            ...p,
                            [field]: e,
                        };
                    }

                    return p;
                });
            });
        };

    const handleSubmit = () => {
        try {
            updatePost(
                payload.map((i) => {
                    return {
                        ...i,
                        author: author || postDetail?.data?.[0]?.author || "",
                        thumbnail:
                            media?.imageUrl ||
                            postDetail?.data?.[0]?.thumbnail ||
                            "",
                        categoryId:
                            selectedCategory?.id ||
                            postDetail?.data?.[0]?.categoryId ||
                            -1,
                    };
                })
            );
        } catch (error) {
            console.log(error);
        }
    };

    const toggleMediaDialog = () => {
        setIsOpenMedia(!isOpenMedia);
    };

    const handleChangeMedia = (media: IMedia) => {
        setMedia(media);
        console.log(media);
    };

    if (isError) {
        return (
            <div>
                <span>Something went wrong, please try again!</span>
            </div>
        );
    }

    return (
        <div className='grid grid-cols-4 gap-4'>
            <Media
                isOpen={isOpenMedia}
                value={media}
                onChange={handleChangeMedia}
                onHide={toggleMediaDialog}
            />

            <div className='col-span-4'>
                <Button
                    link
                    icon='pi pi-backward'
                    label='Post list'
                    onClick={() => {
                        navigate("/post");
                    }}
                />
            </div>

            <div className='col-span-3'>
                <Card title='New Post'>
                    <TabView>
                        <TabPanel header='Standard'>
                            <Form
                                onChange={handleChange(null)}
                                onSubmit={handleSubmit}
                                loading={isLoading}
                                data={
                                    payload.find(
                                        (c) => c.languageId === null
                                    ) || null
                                }
                            />
                        </TabPanel>
                        {langs?.data?.map((lang) => {
                            return (
                                <TabPanel
                                    key={lang.id}
                                    header={lang.name}
                                >
                                    <Form
                                        onChange={handleChange(lang.id)}
                                        onSubmit={handleSubmit}
                                        loading={isLoading}
                                        data={
                                            payload.find(
                                                (c) => c.languageId === lang.id
                                            ) || null
                                        }
                                    />
                                </TabPanel>
                            );
                        })}
                    </TabView>
                </Card>
            </div>

            <div>
                <Card title='Thumnail'>
                    <Button
                        label='Choose image'
                        icon='pi pi-plus'
                        onClick={toggleMediaDialog}
                    />

                    {(postDetail?.data?.[0]?.thumbnail || media) && (
                        <div className='mt-4'>
                            <img
                                className='w-full rounded'
                                src={
                                    import.meta.env.VITE_SERVICE +
                                    (media?.imageUrl ||
                                        postDetail?.data?.[0]?.thumbnail)
                                }
                                alt='Preview image'
                            />
                        </div>
                    )}

                    <div className='mt-4'>
                        <label htmlFor='category'>Category</label>

                        <CategoryProvider
                            render={(categories: ICategory[]) => {
                                return (
                                    <Dropdown
                                        id='category'
                                        value={
                                            selectedCategory ||
                                            categories.find((c) => {
                                                return (
                                                    Number(c.id) ===
                                                    Number(
                                                        postDetail?.data?.[0]
                                                            ?.categoryId
                                                    )
                                                );
                                            })
                                        }
                                        onChange={(e) => {
                                            setSelectedCategory(e.value);
                                        }}
                                        options={categories}
                                        optionLabel='name'
                                        placeholder='Select a Category'
                                        className='w-full md:w-14rem'
                                    />
                                );
                            }}
                        />
                    </div>

                    <div className='mt-4'>
                        <label htmlFor='author'>Author</label>

                        <InputText
                            id='author'
                            value={
                                author || postDetail?.data?.[0]?.author || ""
                            }
                            className='w-full'
                            onChange={(e) => {
                                setAuthor(e.target.value);
                            }}
                        />
                    </div>
                </Card>
            </div>
        </div>
    );
};

export default EditPost;
