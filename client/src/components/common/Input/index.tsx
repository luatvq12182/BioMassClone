import { forwardRef } from "react";
import { TextField, TextFieldProps } from "@mui/material";

type Ref = HTMLInputElement;

const Input = forwardRef<Ref, TextFieldProps>((props, ref) => {
    return (
        <TextField
            inputRef={ref}
            fullWidth
            size='small'
            {...props}
        />
    );
});

export default Input;
