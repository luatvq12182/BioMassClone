import { useState } from "react";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";

type Props = {};

const Form = ({}: Props) => {
    const [value, setValue] = useState("");

    return (
        <div>
            <div className='flex flex-col gap-1'>
                <label htmlFor='name'>Name</label>
                <InputText
                    id='name'
                    aria-describedby='name-help'
                />
            </div>

            <div className='flex flex-col gap-1 mt-3'>
                <label htmlFor='slug'>Slug</label>
                <InputText
                    id='slug'
                    aria-describedby='slug-help'
                />
                <small id='slug-help'>(Auto generated if not provided)</small>
            </div>

            <div className="mt-3">
                <Button label='Submit' />
            </div>
        </div>
    );
};

export default Form;
