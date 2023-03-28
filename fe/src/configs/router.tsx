import { createBrowserRouter } from "react-router-dom";

import AdminLayout from "@/layouts/admin";

import Category from "@/pages/admin/category";
import Login from "@/pages/admin/login";
import ProtectedRoute from "@/components/ProtectedRoute";
import Media from "@/pages/admin/media";
import PostList from "@/pages/admin/post";
import NewPost from "@/pages/admin/post/NewPost";

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
            {
                path: "post",
                element: <PostList />
            },
            {
                path: 'post/new',
                element: <NewPost />
            },
            {
                path: "*",
                element: <h1>404</h1>
            },
        ],
    },
    {
        path: "*",
        element: <h1>404</h1>
    },
    {
        path: "/login",
        element: <Login />,
    },
]);

export default router;
