import { createContext, ReactNode, useRef, useState } from "react";
import { Box, CircularProgress } from "@mui/material";

type Context = {
    showLoading: Function;
    hideLoading: Function;
};

type Props = {
    children: ReactNode;
};

const LoadingContext = createContext<Context>({
    showLoading: () => {},
    hideLoading: () => {},
});

const LoadingProvider = ({ children }: Props) => {
    const [isLoading, setLoading] = useState<boolean>(false);
    const numberOfRequests = useRef<number>(0);

    const showLoading = () => {
        numberOfRequests.current++;
        setLoading(true);
    };

    const hideLoading = () => {
        numberOfRequests.current--;
        if (numberOfRequests.current === 0) {
            setLoading(false);
        }
    };

    return (
        <LoadingContext.Provider
            value={{
                showLoading,
                hideLoading,
            }}
        >
            <Box
                sx={{
                    display: isLoading ? "flex" : "none",
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
            {children}
        </LoadingContext.Provider>
    );
};

export { LoadingContext, LoadingProvider };
