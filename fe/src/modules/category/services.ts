import { httpClient } from "../httpClient";
import { ICategory } from "./models";

const getCategories = () => {
    return httpClient.get<ICategory[]>("/categories");
};

const getCategory = (id: number) => {
    return httpClient.get<ICategory>(`/categories/${id}`);
};

const createCategory = (payload: ICategory) => {
    return httpClient.post<ICategory>("/categories", payload);
};

const updateCategory = (payload: ICategory) => {
    return httpClient.put(`/categories/${payload.id}`, payload);
};

const deleteCategory = (id: number) => {
    return httpClient.delete(`/categories/${id}`);
};

export {
    getCategories,
    getCategory,
    createCategory,
    updateCategory,
    deleteCategory,
};
