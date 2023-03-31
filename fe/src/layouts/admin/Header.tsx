import { Avatar } from "primereact/avatar";
import { Button } from "primereact/button";

type Props = {
    onOpenSidebar: () => void;
};

const Header = ({ onOpenSidebar }: Props) => {
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
                />
            </div>
        </div>
    );
};

export default Header;
