import { httpClient } from "../httpClient";
import { ICategory } from "./models";

const getCategories = (langCode?: string) => {
    return httpClient.get<ICategory[]>(
        `/categories${langCode ? `?lang=${langCode}` : ""}`
    );
};

const getCategory = (id: number) => {
    return httpClient.get<ICategory[]>(`/categories/${id}`);
};

const createCategory = (payload: ICategory[]) => {
    return httpClient.post<ICategory>("/categories", payload);
};

const updateCategory = (id: number) => (payload: ICategory[]) => {
    return httpClient.put(`/categories/${id}`, payload);
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
