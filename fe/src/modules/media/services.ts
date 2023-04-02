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

const deleteMedia = (id: number) => {
    return httpClient.delete(`/media/${id}`)
}

const addToSlider = (ids: number[]) => {
    return httpClient.post("/media/add-to-slider", ids);
}

export { getMedia, uploadFile, deleteMedia, addToSlider };
