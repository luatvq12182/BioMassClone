import { ReactNode } from "react";
import { ICategory } from "./models";
import { useCategories } from "./queries";

type Props = {
    langCode?: string;
    render: (categories: ICategory[], isLoading?: boolean) => ReactNode;
};

const CategoryProvider = ({ render, langCode }: Props) => {
    const { data, isFetching } = useCategories(langCode);

    return <>{render(data ? data.data : [], isFetching)}</>;
};

export { CategoryProvider };
