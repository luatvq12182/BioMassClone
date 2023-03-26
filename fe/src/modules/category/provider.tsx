import { ReactNode } from "react";
import { ICategory } from "./models";
import { useCategories, useCategory } from "./queries";

type Props = {
    id?: number;
    langCode?: string;
    render: (categories: ICategory[], isLoading?: boolean) => ReactNode;
};

const CategoryProvider = ({ render, langCode, id }: Props) => {
    const { data: categories, isFetching: isFetchingCategories } =
        useCategories(langCode);
    const { data: category, isFetching: isFetchingCategory } = useCategory(
        id || -1
    );

    return (
        <>
            {render(
                id
                    ? category?.data
                        ? category.data
                        : []
                    : categories?.data || [],
                id ? isFetchingCategory : isFetchingCategories
            )}
        </>
    );
};

export { CategoryProvider };
