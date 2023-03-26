import { httpClient } from "../httpClient";
import { IMedia } from "./models";

const getMedia = () => {
    return httpClient.get<IMedia[]>("/media");
};

const uploadFile = (formData: FormData) => {
    return httpClient.post("/media/upload", formData, {
        headers: {
            "Content-Type": "multipart/form-data",
        },
    });
};

export { getMedia, uploadFile };
