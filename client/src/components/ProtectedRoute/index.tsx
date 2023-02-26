import { Navigate } from "react-router-dom";
import { Box } from "@mui/material";

import useAuth from "@/hooks/useAuth";

type Props = {
    children: React.ReactNode | JSX.Element;
};

const ProtectedRoute = ({ children }: Props) => {
    const user = useAuth();

    if (!user) {
        return <Navigate to='/sign-in' />;
    }

    return <Box>{children}</Box>;
};

export default ProtectedRoute;
