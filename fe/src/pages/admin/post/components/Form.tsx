import { Button } from "primereact/button";
import { Editor } from "primereact/editor";
import { InputText } from "primereact/inputtext";
import { InputTextarea } from "primereact/inputtextarea";

import { IPost } from "@/modules/post";

type Props = {
    onChange: (field: keyof IPost) => (e: any) => void;
    onSubmit: () => void;
    loading: boolean;
    data: IPost | null;
};

const Form = ({ onChange, onSubmit, loading, data }: Props) => {
    return (
        <div>
            <div>
                <label htmlFor='title'>Post title</label>
                <InputText
                    autoFocus
                    id='title'
                    className='w-full'
                    onChange={(e) => {
                        onChange("title")(e.target.value);
                    }}
                    value={data?.["title"] || ''}
                />
            </div>

            <div className='mt-4'>
                <label htmlFor='shortDescription'>Short description</label>
                <InputTextarea
                    id='shortDescription'
                    className='w-full'
                    autoResize
                    // value={value}
                    onChange={(e) => {
                        onChange("shortDescription")(e.target.value);
                    }}
                    rows={3}
                    value={data?.["shortDescription"] || ''}
                />
            </div>

            <div className='mt-4'>
                <label htmlFor='body'>Post content</label>

                <Editor
                    value={data?.['body'] || ''}
                    onTextChange={(e) => {
                        onChange("body")(e.htmlValue);
                    }}
                    style={{ height: "320px" }}
                />
            </div>

            <div className='mt-4'>
                <Button
                    loading={loading}
                    label='Submit'
                    onClick={onSubmit}
                />
            </div>
        </div>
    );
};

export default Form;
