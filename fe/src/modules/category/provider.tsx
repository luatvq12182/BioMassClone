import { ReactNode } from "react";
import { ICategory } from "./models";
import { useCategories } from "./queries";

type Props = {
    render: (categories: ICategory[]) => ReactNode;
};

const CategoryProvider = ({ render }: Props) => {
    const { data } = useCategories();

    return <>{render(data ? data.data : [])}</>;
};

export { CategoryProvider };
