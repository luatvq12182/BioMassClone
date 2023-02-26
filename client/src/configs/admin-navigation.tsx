import ArticleIcon from "@mui/icons-material/Article";
import CategoryIcon from "@mui/icons-material/Category";
import ContactsIcon from "@mui/icons-material/Contacts";
import DashboardIcon from "@mui/icons-material/Dashboard";
import SlideshowIcon from "@mui/icons-material/Slideshow";
import SettingsIcon from "@mui/icons-material/Settings";

type Navigation = {
    label: string;
    to: string;
    icon: React.ReactElement;
};

const navigations: Navigation[] = [
    {
        label: "Dashboard",
        to: "/admin/dashboard",
        icon: <DashboardIcon />,
    },
    {
        label: "Slider",
        to: "/admin/slider",
        icon: <SlideshowIcon />,
    },
    {
        label: "Category",
        to: "/admin/category",
        icon: <CategoryIcon />,
    },
    {
        label: "Article",
        to: "/admin/article",
        icon: <ArticleIcon />,
    },
    {
        label: "Contact",
        to: "/admin/contact",
        icon: <ContactsIcon />,
    },
    {
        label: "Setting",
        to: "/admin/setting",
        icon: <SettingsIcon />,
    },
];

export default navigations;
