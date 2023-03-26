import { ChangeEvent, useEffect, useState } from "react";
import { useQueryClient } from "react-query";
import { Dialog } from "primereact/dialog";
import { TabView, TabPanel } from "primereact/tabview";

import { ILang, useLangs } from "@/modules/lang";
import {
    CATEGORIES_QUERY_KEY,
    ICategory,
    useCategory,
    useUpdateCategory,
} from "@/modules/category";
import Form from "./Form";

type Props = {
    id: number;
    isOpen: boolean;
    onHide: () => void;
    activeIndex: number;
};

const UpdateDialog = ({ id, activeIndex, isOpen, onHide }: Props) => {
    const queryClient = useQueryClient();
    const [payload, setPayload] = useState<ICategory[]>([]);
    const { data: langs } = useLangs();
    const { mutate: updateCategory, isLoading } = useUpdateCategory(id, {
        onSuccess: () => {
            onHide();
            // setPayload([]);
            queryClient.invalidateQueries([
                CATEGORIES_QUERY_KEY,
                langs?.data?.[activeIndex - 1]?.code,
            ]);
        },
    });
    const { data: category } = useCategory(id || -1);

    useEffect(() => {
        setPayload([]);
    }, [id]);

    if (!payload.length && category) {
        setPayload(category.data);
    }

    const handleChange =
        (langId: number | null) =>
        (field: keyof ICategory) =>
        (e: ChangeEvent<HTMLInputElement>) => {
            setPayload((preState: ICategory[]) => {
                if (!preState.find((c) => c.languageId === langId)) {
                    return [
                        ...preState,
                        {
                            languageId: langId,
                            [field]: e.target.value,
                        },
                    ];
                }

                return preState.map((c) => {
                    if (c.languageId === langId) {
                        return {
                            ...c,
                            [field]: e.target.value,
                        };
                    }

                    return c;
                });
            });
        };

    const handleSubmit = () => {
        try {
            updateCategory(payload);
        } catch (error) {
            console.log(error);
        }
    };

    return (
        <Dialog
            header='Edit category'
            visible={isOpen}
            style={{ width: "30vw" }}
            onHide={onHide}
        >
            <TabView>
                <TabPanel header='Standard'>
                    <Form
                        languageId={null}
                        onChange={handleChange}
                        onSubmit={handleSubmit}
                        loading={isLoading}
                        data={
                            payload.find((c) => c.languageId === null) || null
                        }
                    />
                </TabPanel>

                {langs?.data?.map((lang: ILang) => {
                    return (
                        <TabPanel
                            key={lang.id}
                            header={lang.name}
                        >
                            <Form
                                languageId={lang.id}
                                onChange={handleChange}
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
        </Dialog>
    );
};

export default UpdateDialog;
