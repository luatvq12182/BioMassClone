import React, { useRef, useState } from "react";
import classNames from "classnames";
import { Toast } from "primereact/toast";
import { FileUpload } from "primereact/fileupload";
import { ProgressBar } from "primereact/progressbar";
import { Button } from "primereact/button";
import { Tooltip } from "primereact/tooltip";
import { Tag } from "primereact/tag";
import { TabView, TabPanel } from "primereact/tabview";
import { Card } from "primereact/card";
import { Image } from "primereact/image";

import useMedia from "@/modules/media/queries";
import {
    IMedia,
    MediaProvider,
    useDeleteMedia,
    useUploadFile,
} from "@/modules/media";
import { showConfirm } from "@/utils";
import Loading from "@/components/Loading";

type Props = {
    isDialog?: boolean;
    value?: IMedia | null;
    onChange?: (media: IMedia) => void;
};

const Media = ({ isDialog = false, value = null, onChange }: Props) => {
    const { refetch } = useMedia();
    const [selectedImg, setSelectedImg] = useState<IMedia | null>(value);
    const { mutate: uploadMedia, isLoading: isUploading } = useUploadFile({
        onSuccess: () => {
            (fileUploadRef.current as any).clear();

            (toast.current as any).show({
                severity: "info",
                summary: "Success",
                detail: "File Uploaded",
            });

            refetch();
        },
    });
    const { mutate: deleteFile, isLoading: isDeleting } = useDeleteMedia({
        onSuccess: () => {
            refetch();
        },
    });

    const toast = useRef(null);
    const [totalSize, setTotalSize] = useState(0);
    const fileUploadRef = useRef(null);

    const onTemplateSelect = (e: any) => {
        let _totalSize = totalSize;
        let files = e.files;

        Object.keys(files).forEach((key) => {
            _totalSize += files[key].size || 0;
        });

        setTotalSize(_totalSize);
    };

    const onTemplateUpload = (e: any) => {
        let _totalSize = 0;

        e.files.forEach((file: any) => {
            _totalSize += file.size || 0;
        });

        setTotalSize(_totalSize);
    };

    const onTemplateRemove = (file: any, callback: any) => {
        setTotalSize(totalSize - file.size);
        callback();
    };

    const onTemplateClear = () => {
        setTotalSize(0);
    };

    const headerTemplate = (options: any) => {
        const { className, chooseButton, uploadButton, cancelButton } = options;
        const value = totalSize / 100000;
        const formatedValue =
            fileUploadRef && fileUploadRef.current
                ? (fileUploadRef.current as any).formatSize(totalSize)
                : "0 B";

        return (
            <div
                className={className}
                style={{
                    backgroundColor: "transparent",
                    display: "flex",
                    alignItems: "center",
                }}
            >
                {chooseButton}
                {uploadButton}
                {cancelButton}
                <div className='flex align-items-center gap-3 ml-auto'>
                    <span>{formatedValue} / 10 MB</span>
                    <ProgressBar
                        value={value}
                        showValue={false}
                        style={{ width: "10rem", height: "12px" }}
                    ></ProgressBar>
                </div>
            </div>
        );
    };

    const itemTemplate = (file: any, props: any) => {
        return (
            <div className='flex align-items-center flex-wrap'>
                <div
                    className='flex align-items-center'
                    style={{ width: "40%" }}
                >
                    <img
                        alt={file.name}
                        role='presentation'
                        src={file.objectURL}
                        width={100}
                    />
                    <span className='flex flex-column text-left ml-3'>
                        {file.name}
                        <small>{new Date().toLocaleDateString()}</small>
                    </span>
                </div>
                <Tag
                    value={props.formatSize}
                    severity='warning'
                    className='px-3 py-2'
                />
                <Button
                    type='button'
                    icon='pi pi-times'
                    className='p-button-outlined p-button-rounded p-button-danger ml-auto'
                    onClick={() => onTemplateRemove(file, props.onRemove)}
                />
            </div>
        );
    };

    const emptyTemplate = () => {
        return (
            <div className='flex align-items-center flex-column'>
                <i
                    className='pi pi-image mt-3 p-5'
                    style={{
                        fontSize: "5em",
                        borderRadius: "50%",
                        backgroundColor: "var(--surface-b)",
                        color: "var(--surface-d)",
                    }}
                ></i>
                <span
                    style={{
                        fontSize: "1.2em",
                        color: "var(--text-color-secondary)",
                    }}
                    className='my-5'
                >
                    Drag and Drop Image Here
                </span>
            </div>
        );
    };

    const chooseOptions = {
        icon: "pi pi-fw pi-images",
        iconOnly: true,
        className: "custom-choose-btn p-button-rounded p-button-outlined",
    };

    const uploadOptions = {
        icon: "pi pi-fw pi-cloud-upload",
        iconOnly: true,
        className:
            "custom-upload-btn p-button-success p-button-rounded p-button-outlined",
    };

    const cancelOptions = {
        icon: "pi pi-fw pi-times",
        iconOnly: true,
        className:
            "custom-cancel-btn p-button-danger p-button-rounded p-button-outlined",
    };

    const customBase64Uploader = async (event: any) => {
        // convert file to base64 encoded
        const files = event.files;

        const formData = new FormData();

        for (const key in files) {
            if (Object.prototype.hasOwnProperty.call(files, key)) {
                formData.append("file", files[Number(key)]);
            }
        }

        uploadMedia(formData);
    };

    const getUrlImage = (src: string) => {
        return import.meta.env.VITE_SERVICE + src;
    };

    const isLoading = isUploading || isDeleting;

    return React.createElement(
        isDialog ? "div" : Card,
        { title: "Media" },
        <TabView>
            <TabPanel header='Media list'>
                <MediaProvider
                    render={(media: IMedia[]) => {
                        if (!isDialog) {
                            return (
                                <div>
                                    <Loading loading={isLoading} />

                                    <div className='grid grid-cols-4 gap-4 mt-4'>
                                        {media?.map((item) => {
                                            return (
                                                <div
                                                    key={item.id}
                                                    className='relative'
                                                >
                                                    <div className='absolute -top-2 -right-2 z-[999]'>
                                                        <Button
                                                            icon='pi pi-trash'
                                                            rounded
                                                            severity='danger'
                                                            aria-label='Cancel'
                                                            onClick={() => {
                                                                showConfirm(
                                                                    () => {
                                                                        deleteFile(
                                                                            item.id
                                                                        );
                                                                    }
                                                                );
                                                            }}
                                                        />
                                                    </div>

                                                    <Image
                                                        src={getUrlImage(
                                                            item.imageUrl
                                                        )}
                                                        alt='Image'
                                                        // width='250'
                                                        preview
                                                    />
                                                </div>
                                            );
                                        })}
                                    </div>
                                </div>
                            );
                        }

                        return (
                            <div>
                                <Loading loading={isLoading} />

                                <div className='grid grid-cols-4 gap-4 mt-4'>
                                    {media?.map((item) => {
                                        return (
                                            <div
                                                onClick={() => {
                                                    setSelectedImg(item);
                                                    onChange?.(item);
                                                }}
                                                className={classNames(
                                                    "h-[250px] w-full ease-out duration-300 hover:opacity-80 hover:scale-95",
                                                    {
                                                        "scale-95 border-2 border-sky-400 border-solid":
                                                            selectedImg?.id ===
                                                            item.id,
                                                    }
                                                )}
                                                key={item.id}
                                                style={{
                                                    backgroundImage: `url(${getUrlImage(
                                                        item.imageUrl
                                                    )})`,
                                                    backgroundSize: "cover",
                                                    borderRadius: "5px",
                                                    cursor: "pointer",
                                                }}
                                            ></div>
                                        );
                                    })}
                                </div>
                            </div>
                        );
                    }}
                />
            </TabPanel>

            <TabPanel header='Upload media'>
                <Toast ref={toast}></Toast>

                <Tooltip
                    target='.custom-choose-btn'
                    content='Choose'
                    position='bottom'
                />
                <Tooltip
                    target='.custom-upload-btn'
                    content='Upload'
                    position='bottom'
                />
                <Tooltip
                    target='.custom-cancel-btn'
                    content='Clear'
                    position='bottom'
                />

                <FileUpload
                    customUpload
                    uploadHandler={customBase64Uploader}
                    ref={fileUploadRef}
                    multiple
                    accept='image/*'
                    maxFileSize={10000000}
                    onUpload={onTemplateUpload}
                    onSelect={onTemplateSelect}
                    onError={onTemplateClear}
                    onClear={onTemplateClear}
                    headerTemplate={headerTemplate}
                    itemTemplate={itemTemplate}
                    emptyTemplate={emptyTemplate}
                    chooseOptions={chooseOptions}
                    uploadOptions={uploadOptions}
                    cancelOptions={cancelOptions}
                />
            </TabPanel>
        </TabView>
    );
};

export default Media;
