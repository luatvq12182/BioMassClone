import { useContext } from "react";
import { LoadingContext } from "@/contexts/Loading";

const useLoading = () => {
    const { showLoading, hideLoading } = useContext(LoadingContext);

    return { showLoading, hideLoading };
};

export default useLoading;
