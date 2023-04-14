import { useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import Header from "./Header";
import Sidebar from "./Sidebar";

const AdminLayout = () => {
    const [isOpenSidebar, setIsOpenSidebar] = useState<boolean>(false);

    useEffect(() => {
        document.title = "Admin";
    }, []);

    const toggleSidebar = () => {
        setIsOpenSidebar(!isOpenSidebar);
    };

    return (
        <main id='admin-layout'>
            <Header onOpenSidebar={toggleSidebar} />

            <Sidebar
                isOpen={isOpenSidebar}
                onHide={toggleSidebar}
            />

            <div className='layout-content container mx-auto'>
                <div className='layout-content-inner'>
                    <Outlet />
                </div>
            </div>
        </main>
    );
};

export default AdminLayout;
