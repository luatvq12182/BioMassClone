import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "primereact/button";
import { Card } from "primereact/card";
import { InputText } from "primereact/inputtext";
import { Dropdown } from "primereact/dropdown";
import { TabPanel, TabView } from "primereact/tabview";

import Form from "./components/Form";
import { IPost, useCreatePost } from "@/modules/post";
import { CategoryProvider, ICategory } from "@/modules/category";
import Media from "@/components/Media";
import { IMedia } from "@/modules/media";
import { useLangs } from "@/modules/lang";

const NewPost = () => {
    const navigate = useNavigate();
    const { data: langs } = useLangs();
    const [payload, setPayload] = useState<IPost[]>([]);
    const { mutate: createPost, isLoading } = useCreatePost({
        onSuccess: () => {
            // navigate("/admin/post");
        },
    });
    const [isOpenMedia, setIsOpenMedia] = useState<boolean>(false);
    const [media, setMedia] = useState<IMedia | null>(null);
    const [selectedCategory, setSelectedCategory] = useState<ICategory | null>(
        null
    );
    const [author, setAuthor] = useState<string>("");

    const handleChange =
        (langId: number | null) => (field: keyof IPost) => (e: any) => {
            setPayload((preState: IPost[]) => {
                if (!preState.find((p) => p.languageId === langId)) {
                    return [
                        ...preState,
                        {
                            languageId: langId,
                            [field]: e,
                        },
                    ];
                }

                return preState.map((p) => {
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
            createPost(
                payload.map((i) => {
                    return {
                        ...i,
                        author,
                        thumbnail: media?.imageUrl || "",
                        categoryId: selectedCategory?.id || -1,
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

    return (
        <div className='grid grid-cols-4 gap-4'>
            <Media
                isOpen={isOpenMedia}
                value={media}
                onChange={handleChangeMedia}
                onHide={toggleMediaDialog}
            />

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

                    {media && (
                        <div className='mt-4'>
                            <img
                                className='w-full rounded'
                                src={
                                    import.meta.env.VITE_SERVICE +
                                    media?.imageUrl
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
                                        value={selectedCategory}
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

export default NewPost;
