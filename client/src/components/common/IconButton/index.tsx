import { IconButton as MUIIconButton, IconButtonProps } from "@mui/material";

type Props = {};

const IconButton = (props: Props & IconButtonProps) => {
    return (
        <MUIIconButton
            aria-label='delete'
            size='small'
            {...props}
        />
    );
};

export default IconButton;
