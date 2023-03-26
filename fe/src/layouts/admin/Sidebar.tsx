// import { SlideMenu } from "primereact/slidemenu";
import { Sidebar as PSidebar } from "primereact/sidebar";

type Props = {
    isOpen: boolean;
    onHide: () => void;
};

const Sidebar = ({ isOpen, onHide }: Props) => {
    const items = [
        {
            label: "Category",
            icon: "pi pi-fw pi-file",
        },
        {
            label: "Media",
            icon: "pi pi-fw pi-file",
        },
        {
            label: "Article",
            icon: "pi pi-fw pi-file",
            items: [
                {
                    label: "New article",
                    icon: "pi pi-fw pi-plus",
                },
                {
                    label: "Article list",
                    icon: "pi pi-fw pi-trash",
                },
            ],
        },
    ];

    return (
        <div className='card flex justify-content-center'>
            <PSidebar
                visible={isOpen}
                onHide={onHide}
            >
                {/* <SlideMenu
                    model={items}
                    viewportHeight={220}
                /> */}
            </PSidebar>
        </div>
    );
};
export default Sidebar;
