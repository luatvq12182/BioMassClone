import { useRef } from "react";
import { Avatar } from "primereact/avatar";
import { Button } from "primereact/button";
import { Menu } from "primereact/menu";

type Props = {
    onOpenSidebar: () => void;
};

const Header = ({ onOpenSidebar }: Props) => {
    const menu = useRef<any>(null);

    const items = [
        {
            label: "Log out",
            icon: "pi pi-sign-out",
            command: (e: any) => {
                window.localStorage.removeItem("accessToken");
                window.location.reload();
                //router.push('/fileupload');
            },
        },
    ];

    return (
        <div className='layout-topbar layout-topbar-sticky'>
            <div className='layout-topbar-inner'>
                <div className='flex items-center'>
                    <Button
                        onClick={onOpenSidebar}
                        icon='pi pi-bars'
                        rounded
                        text
                        aria-label='Filter'
                        size='large'
                    />

                    <h2>Green way</h2>
                </div>

                <Avatar
                    icon='pi pi-user'
                    className='mr-2'
                    size='normal'
                    style={{ backgroundColor: "#2196F3", color: "#ffffff" }}
                    shape='circle'
                    onClick={(e) => menu.current.toggle(e)}
                />

                <Menu
                    model={items}
                    popup
                    ref={menu}
                />
            </div>
        </div>
    );
};

export default Header;
