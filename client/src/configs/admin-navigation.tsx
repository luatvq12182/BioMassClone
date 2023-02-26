import ArticleIcon from "@mui/icons-material/Article";
import ListIcon from '@mui/icons-material/List';
import ContactsIcon from "@mui/icons-material/Contacts";
import DashboardIcon from "@mui/icons-material/Dashboard";
import SlideshowIcon from "@mui/icons-material/Slideshow";
import SettingsIcon from "@mui/icons-material/Settings";
import AttachFileIcon from '@mui/icons-material/AttachFile';

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
        label: "File Manager",
        to: "/admin/file-manager",
        icon: <AttachFileIcon />,
    },
    {
        label: "Slider",
        to: "/admin/slider",
        icon: <SlideshowIcon />,
    },
    {
        label: "Category",
        to: "/admin/category",
        icon: <ListIcon />,
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
