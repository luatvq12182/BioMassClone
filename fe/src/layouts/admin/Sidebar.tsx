// import { SlideMenu } from "primereact/slidemenu";
import { Sidebar as PSidebar } from "primereact/sidebar";
import { Link } from "react-router-dom";

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
                <ol
                    style={{
                        margin: "0 0 0 0rem",
                        padding: "0.25rem 0 0 0.5rem",
                        // borderLeft: "1px solid var(--surface-border)",
                    }}
                >
                    <li>
                        <span className='menu-child-category'>Category</span>
                        <ol>
                            <li>
                                <Link
                                    onClick={onHide}
                                    to='/admin/category'
                                >
                                    Category list
                                </Link>
                            </li>
                        </ol>
                    </li>

                    <li>
                        <span className='menu-child-category'>Media</span>
                        <ol>
                            <li>
                                <Link
                                    onClick={onHide}
                                    to='/admin/media'
                                >
                                    Upload
                                </Link>
                            </li>
                        </ol>
                    </li>

                    {/* <li>
                        <span className='menu-child-category'>Article</span>
                        <ol>
                            <li>
                                <Link to='/admin/article'>Article list</Link>
                            </li>
                            <li>
                                <Link to='/admin/article/new'>New article</Link>
                            </li>
                        </ol>
                    </li> */}
                </ol>
            </PSidebar>
        </div>
    );
};
export default Sidebar;
