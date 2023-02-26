import { useState, useRef } from "react";
import { Box, Grid } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import VisibilityIcon from "@mui/icons-material/Visibility";
import FileUploadIcon from "@mui/icons-material/FileUpload";

import useFiles from "@/hooks/queries/useFiles";

import { showConfirm } from "@/utils";
import Loading from "@/components/Loading";
import PreviewImage from "@/components/PreviewImage";
import { Button, IconButton } from "@/components/common";
import useUploadFile from "@/hooks/mutations/useUploadFile";
import useDeleteFile from "@/hooks/mutations/useDeleteFile";

const FileManager = () => {
    const ipFile = useRef<HTMLInputElement | null>(null!);
    const { data, isFetching } = useFiles();
    const [imgPreview, setImgPreview] = useState<string | null>(null);
    const [isOpenPreview, setIsOpenPreview] = useState<boolean>(false);

    const { mutate: uploadFile, isLoading: isUploading } = useUploadFile({});
    const { mutate: deleteFile, isLoading: isDeleting } = useDeleteFile({});

    const handleClickUpload = () => {
        ipFile.current?.click();
    };

    const handleChangeFile = () => {
        const formData = new FormData();
        const files = ipFile.current?.files;

        for (const key in files) {
            if (Object.prototype.hasOwnProperty.call(files, key)) {
                formData.append("files", files[Number(key)]);
            }
        }

        uploadFile(formData);
    };

    const handleClickDelete = (id: number) => {
        showConfirm(
            `You are about to permanently delete these items from your site. \nThis action cannot be undone. \n'Cancel' to stop, 'OK' to delete.`,
            () => deleteFile(id)
        );
    };

    return (
        <Box>
            <Loading
                visible={isFetching || isUploading || isDeleting}
            />

            <PreviewImage
                key={imgPreview}
                isOpen={isOpenPreview}
                onClose={() => setIsOpenPreview(false)}
                data={imgPreview || ""}
            />

            <Box className='mb-3'>
                <Button
                    disabled={isDeleting || isFetching}
                    loading={isUploading}
                    onClick={handleClickUpload}
                >
                    Upload file
                    <FileUploadIcon />
                </Button>
                <input
                    onChange={handleChangeFile}
                    ref={ipFile}
                    type='file'
                    hidden={true}
                    multiple={true}
                />
            </Box>

            <Grid
                container
                spacing={1}
                mt={1}
            >
                {data?.data.map((file: any) => {
                    const srcImg =
                        `${import.meta.env.VITE_SERVICE}/images/` +
                        file.fileName;

                    return (
                        <Grid
                            item
                            xs={12}
                            xl={3}
                            key={file.id}
                        >
                            <Box
                                sx={{
                                    backgroundImage: `url(${srcImg})`,
                                    backgroundSize: "cover",
                                    height: "200px",
                                    width: "100%",
                                    borderRadius: 2,
                                    position: "relative",
                                    cursor: "pointer",
                                    "&:hover > div": {
                                        opacity: 1,
                                    },
                                }}
                            >
                                <Box
                                    sx={{
                                        opacity: 0,
                                        display: "flex",
                                        justifyContent: "center",
                                        alignItems: "center",
                                        backgroundColor: "rgb(31 31 31 / 20%)",
                                        backdropFilter: "blur(8px)",
                                        position: "absolute",
                                        top: 0,
                                        right: 0,
                                        bottom: 0,
                                        left: 0,
                                        borderRadius: 2,
                                        transition: "0.3s",
                                    }}
                                >
                                    <IconButton
                                        size='large'
                                        color='info'
                                        disabled={isFetching || isUploading || isDeleting}
                                        onClick={() => {
                                            setImgPreview(srcImg);
                                            setIsOpenPreview(true);
                                        }}
                                    >
                                        <VisibilityIcon />
                                    </IconButton>
                                    <IconButton
                                        size='large'
                                        color='error'
                                        disabled={isFetching || isUploading || isDeleting}
                                        onClick={() =>
                                            handleClickDelete(file.id)
                                        }
                                    >
                                        <DeleteIcon />
                                    </IconButton>
                                </Box>
                            </Box>
                        </Grid>
                    );
                })}
            </Grid>
        </Box>
    );
};

export default FileManager;
