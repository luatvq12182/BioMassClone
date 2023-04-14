export type NavigationProps = {
    label: string;
    icon: string;
    to: string;
};

const ADMIN_NAVIGATION: NavigationProps[] = [
    {
        label: "Category",
        icon: "pi pi-list",
        to: "/category",
    },
    {
        label: "Media",
        icon: "pi pi-cloud-upload",
        to: "/media",
    },
    {
        label: "Post",
        icon: "pi pi-book",
        to: "/post",
    },
    {
        label: "Slider",
        icon: "pi pi-sliders-h",
        to: "/slider",
    },
];

export { ADMIN_NAVIGATION };
