import httpClient from "../httpClient";

const getFiles = () => {
    return httpClient.get("/files");
};

const uploadFile = (formData: FormData) => {
    return httpClient.post("/files/upload", formData, {
        headers: {
            "Content-Type": "multipart/form-data"
        }
    });
};

const deleteFile = (id: number) => {
    return httpClient.delete(`/files/${id}`);
};

export { getFiles, uploadFile, deleteFile };
