import { InputText } from "primereact/inputtext";

import { CategoryProvider, ICategory } from "@/modules/category";

type Props = {};

const Category = ({}: Props) => {
    const render = (categories: ICategory[]) => {
        console.log(categories);

        return (
            <div className='flex flex-column gap-2'>
                <label htmlFor='username'>Username</label>
                <InputText
                    className="input-text-sm"
                    id='username'
                    aria-describedby='username-help'
                />
                <small id='username-help'>
                    Enter your username to reset your password.
                </small>
            </div>
        );
    };

    return <CategoryProvider render={render} />;
};

export default Category;
