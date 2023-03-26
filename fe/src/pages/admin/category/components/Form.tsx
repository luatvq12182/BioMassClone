import { ChangeEvent, useState } from "react";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import { ICategory } from "@/modules/category";

type Props = {
    languageId: number | null;
    onChange: (
        langId: number | null
    ) => (field: keyof ICategory) => (e: ChangeEvent<HTMLInputElement>) => void;
    onSubmit: () => void;
    loading: boolean;
    data: ICategory | null;
};

const Form = ({ languageId, onChange, onSubmit, loading, data }: Props) => {
    return (
        <div>
            <div className='flex flex-col gap-1'>
                <label htmlFor='name'>Name</label>
                <InputText
                    autoFocus
                    id='name'
                    aria-describedby='name-help'
                    onChange={onChange(languageId)("name")}
                    disabled={loading}
                    value={data?.["name"] || ''}
                />
            </div>

            <div className='flex flex-col gap-1 mt-3'>
                <label htmlFor='slug'>Slug</label>
                <InputText
                    id='slug'
                    aria-describedby='slug-help'
                    onChange={onChange(languageId)("slug")}
                    disabled={loading}
                    value={data?.["slug"] || ''}
                />
                <small id='slug-help'>(Auto generated if not provided)</small>
            </div>

            <div className='mt-3'>
                <Button
                    onClick={onSubmit}
                    label='Submit'
                    loading={loading}
                />
            </div>
        </div>
    );
};

export default Form;
