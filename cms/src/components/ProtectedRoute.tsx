import { ReactNode, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import useLocalStorage from "@/hooks/useLocalStorage";

type Props = {
    children: ReactNode;
};

const ProtectedRoute = ({ children }: Props) => {
    const [token] = useLocalStorage("accessToken", "");
    const navigate = useNavigate();

    useEffect(() => {
        if (!token) {
            navigate("/login");
        }
    }, []);

    return <>{children}</>;
};

export default ProtectedRoute;
