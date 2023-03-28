import { useState } from "react";
import { Dialog } from "primereact/dialog";
import { Button } from "primereact/button";

import NativeMedia from "@/pages/admin/media";
import { IMedia } from "@/modules/media";

type Props = {
    value: IMedia | null;
    onChange: (media: IMedia) => void;
    isOpen: boolean;
    onHide: () => void;
};

const Media = ({ isOpen, value, onChange, onHide }: Props) => {
    const [media, setMedia] = useState<IMedia | null>(null);

    const footerContent = (
        <div>
            <Button
                label='Close'
                icon='pi pi-times'
                onClick={onHide}
                className='p-button-text'
            />
            <Button
                disabled={!media}
                label='Submit'
                icon='pi pi-check'
                onClick={() => {
                    if (media) {
                        onChange(media);
                    }
                    onHide();
                }}
                autoFocus
            />
        </div>
    );

    return (
        <Dialog
            header='Media'
            visible={isOpen}
            style={{ width: "80%" }}
            onHide={onHide}
            footer={footerContent}
        >
            <NativeMedia
                isDialog={true}
                value={value}
                onChange={(media: IMedia) => {
                    setMedia(media);
                }}
            />
        </Dialog>
    );
};

export default Media;
