import { useState } from "react";
import { Card } from "primereact/card";
import { Button } from "primereact/button";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";

import { IMedia, MediaProvider, useAddToSlider } from "@/modules/media";
import Media from "@/components/Media";

const Slider = () => {
    const { mutate: addToSlider } = useAddToSlider()
    const [selectedMedia, setSelectedMedia] = useState<IMedia | null>(null);
    const [isOpenMedia, setIsOpenMedia] = useState<boolean>(false);

    const columns = [
        {
            field: "index",
            header: "#",
            style: { width: "50px" },
            body: (_: IMedia, e: any) => {
                return e.rowIndex + 1;
            },
        },
        {
            field: "thumbnail",
            header: "Thumbnail",
            style: { width: "300px" },
            body: (e: IMedia) => {
                return (
                    <img
                        className='w-[150px]'
                        src={import.meta.env.VITE_SERVICE + e.imageUrl}
                        alt=''
                    />
                );
            },
        },
        {
            field: "action",
            header: "#",
            style: { width: "150px", textAlign: "center" },
            body: (e: IMedia) => {
                return (
                    <div>
                        <Button
                            // disabled={isDeleting}
                            // onClick={() =>
                            //     handleClickDelete(e.categoryId || e.id)
                            // }
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

    const toggleMedia = () => {
        setIsOpenMedia(!isOpenMedia);
    };

    const render = (media: IMedia[], isLoading?: boolean) => {
        console.log(media);

        return (
            <DataTable
                value={media}
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
                    <h3>Slider</h3>

                    <Button
                        icon='pi pi-plus'
                        label='New slider'
                        onClick={() => {
                            setIsOpenMedia(!isOpenMedia);
                        }}
                    />
                </div>
            }
        >
            <Media
                value={selectedMedia}
                isOpen={isOpenMedia}
                onHide={toggleMedia}
                onChange={(media: IMedia) => {
                    setSelectedMedia(media);
                    addToSlider([media.id]);
                }}
            />

            <MediaProvider
                isSlider
                render={render}
            />
        </Card>
    );
};

export default Slider;
