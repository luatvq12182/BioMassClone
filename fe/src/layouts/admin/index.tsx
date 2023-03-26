import { useEffect } from "react";
import { Outlet } from "react-router-dom";

const AdminLayout = () => {
    useEffect(() => {
        document.title = "Admin";
    }, []);

    return (
        <main id='admin-layout'>
            <Outlet />
        </main>
    );
};

export default AdminLayout;
