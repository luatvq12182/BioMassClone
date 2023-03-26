import { Button } from 'primereact/button';

type Props = {
    onOpenSidebar: () => void
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
                        size="large"
                    />
                    
                    <h2>Bio Mass</h2>
                </div>
            </div>
        </div>
    );
};

export default Header;
