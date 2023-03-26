import { ChangeEvent, useEffect, useState } from "react";
import { useQueryClient } from "react-query";
import { Card } from "primereact/card";
import { TabView, TabPanel } from "primereact/tabview";
import Form from "./components/Form";
import Table from "./components/Table";

import {
    CATEGORIES_QUERY_KEY,
    ICategory,
    useCreateCategory,
} from "@/modules/category";
import { ILang, useLangs } from "@/modules/lang";

const Category = () => {
    const queryClient = useQueryClient();
    const [payload, setPayload] = useState<ICategory[]>([]);
    const { data: langs } = useLangs();
    const { mutate: createCategory, isLoading } = useCreateCategory({
        onSuccess: () => {
            setPayload([]);
            queryClient.invalidateQueries([
                CATEGORIES_QUERY_KEY,
                langs?.data?.[activeIndex - 1]?.code,
            ]);
        },
    });
    const [activeIndex, setActiveIndex] = useState<number>(0);

    useEffect(() => {
        queryClient.invalidateQueries([
            CATEGORIES_QUERY_KEY,
            langs?.data?.[activeIndex - 1]?.code,
        ]);
    }, [activeIndex]);

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
            createCategory(payload);
        } catch (error) {
            console.log(error);
        }
    };

    return (
        <div className='grid grid-cols-3 gap-4'>
            <div>
                <Card title='Create category'>
                    <TabView>
                        <TabPanel header='Standard'>
                            <Form
                                languageId={null}
                                onChange={handleChange}
                                onSubmit={handleSubmit}
                                loading={isLoading}
                                data={
                                    payload.find(
                                        (c) => c.languageId === null
                                    ) || null
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
                </Card>
            </div>

            <div className='col-span-2'>
                <Card title='Category list'>
                    <TabView
                        activeIndex={activeIndex}
                        onTabChange={(e) => setActiveIndex(e.index)}
                    >
                        <TabPanel header='Standard'>
                            <Table />
                        </TabPanel>

                        {langs?.data?.map((lang) => {
                            return (
                                <TabPanel
                                    key={lang.id}
                                    header={lang.name}
                                >
                                    <Table langCode={lang.code} />
                                </TabPanel>
                            );
                        })}
                    </TabView>
                </Card>
            </div>
        </div>
    );
};

export default Category;
