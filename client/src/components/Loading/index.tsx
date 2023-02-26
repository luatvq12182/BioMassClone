import { Box, CircularProgress } from "@mui/material";

type Props = {
    visible: boolean;
};

const Loading = ({ visible }: Props) => {
    return (
        <Box
            sx={{
                display: visible ? "flex" : "none",
                justifyContent: "center",
                alignItems: "center",
                backgroundColor: "rgb(31 31 31 / 20%)",
                backdropFilter: "blur(4px)",
                position: "fixed",
                top: 0,
                right: 0,
                bottom: 0,
                left: 0,
                zIndex: 9999,
            }}
        >
            <CircularProgress size={60} />
        </Box>
    );
};

export default Loading;
