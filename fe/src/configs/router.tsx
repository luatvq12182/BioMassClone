import { createBrowserRouter } from "react-router-dom";

import AdminLayout from "@/layouts/admin";

import Category from "@/pages/admin/category";
import Login from "@/pages/admin/login";
import ProtectedRoute from "@/components/ProtectedRoute";
import Media from "@/pages/admin/media";

const router = createBrowserRouter([
    {
        path: "/",
        element: <h1>Home page</h1>,
    },
    {
        path: "admin",
        element: (
            <ProtectedRoute>
                <AdminLayout />
            </ProtectedRoute>
        ),
        children: [
            {
                path: "category",
                element: <Category />,
            },
            {
                path: "media",
                element: <Media />,
            },
        ],
    },
    {
        path: "/login",
        element: <Login />,
    },
]);

export default router;
