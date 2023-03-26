import { createBrowserRouter } from "react-router-dom";

import AdminLayout from "@/layouts/admin";

import Category from "@/pages/admin/category";
import Login from "@/pages/admin/login";

const router = createBrowserRouter([
    {
        path: "/",
        element: <h1>Home page</h1>,
    },
    {
        path: "admin",
        element: <AdminLayout />,
        children: [
            {
                path: "category",
                element: <Category />,
            },
        ],
    },
    {
        path: "/login",
        element: <Login />
    }
]);

export default router;
