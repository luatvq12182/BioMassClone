import { Sidebar as PSidebar } from "primereact/sidebar";

type Props = {
    isOpen: boolean;
    onHide: () => void;
};

const Sidebar = ({ isOpen, onHide }: Props) => {
    return (
        <div className='card flex justify-content-center'>
            <PSidebar
                visible={isOpen}
                onHide={onHide}
            >
                <h2>Sidebar</h2>
                <p>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                    do eiusmod tempor incididunt ut labore et dolore magna
                    aliqua. Ut enim ad minim veniam, quis nostrud exercitation
                    ullamco laboris nisi ut aliquip ex ea commodo consequat.
                </p>
            </PSidebar>
        </div>
    );
};
export default Sidebar;
