export type NavigationProps = {
    label: string;
    icon: string;
    to: string;
};

const ADMIN_NAVIGATION: NavigationProps[] = [
    {
        label: "Category",
        icon: "pi pi-list",
        to: "/admin/category",
    },
    {
        label: "Media",
        icon: "pi pi-cloud-upload",
        to: "/admin/media",
    },
    {
        label: "Post",
        icon: "pi pi-book",
        to: "/admin/post",
    },
    {
        label: "Slider",
        icon: "pi pi-sliders-h",
        to: "/admin/slider",
    },
];

export { ADMIN_NAVIGATION };
