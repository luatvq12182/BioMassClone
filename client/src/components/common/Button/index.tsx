import { Button as MUIButton, ButtonProps } from "@mui/material";
import LoadingButton from "@mui/lab/LoadingButton";
import SaveIcon from "@mui/icons-material/Save";

type Props = {
    loading?: boolean;
};

const Button = (props: Props & ButtonProps) => {
    if (props.loading) {
        return (
            <LoadingButton
                loading={true}
                loadingPosition='start'
                startIcon={<SaveIcon />}
                variant='contained'
                {...props}
            />
        );
    }

    return (
        <MUIButton
            size='medium'
            variant='contained'
            {...props}
        />
    );
};

export default Button;
