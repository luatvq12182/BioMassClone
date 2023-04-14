import { NavLink } from "react-router-dom";
import { Sidebar as PSidebar } from "primereact/sidebar";

import { ADMIN_NAVIGATION } from "@/configs/admin-navigation";

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
                <ol
                    style={{
                        margin: "0",
                        padding: "0.25rem 0 0 0.25rem",
                        // borderLeft: "1px solid var(--surface-border)",
                    }}
                >
                    {ADMIN_NAVIGATION.map(({ icon, label, to }) => {
                        return (
                            <li key={to}>
                                <NavLink
                                    onClick={onHide}
                                    to={to}
                                >
                                    <div className='icon'>
                                        <i
                                            className={icon}
                                            style={{
                                                fontSize: "1rem",
                                            }}
                                        ></i>
                                    </div>

                                    {label}
                                </NavLink>
                            </li>
                        );
                    })}
                </ol>
            </PSidebar>
        </div>
    );
};
export default Sidebar;
