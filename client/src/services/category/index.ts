import httpClient from "../httpClient";
import { Category } from "@/models/Category";

const getCategories = () => {
    return httpClient.get<Category[]>("/categories");
};

const getCategory = (id: number) => {
    return httpClient.get<Category>(`/categories/${id}`);
};

const createCategory = (payload: Category) => {
    return httpClient.post<Category>("/categories", payload);
};

const updateCategory = (payload: Category) => {
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
