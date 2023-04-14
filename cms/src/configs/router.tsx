import { createBrowserRouter } from "react-router-dom";

import AdminLayout from "@/layouts/admin";
import ProtectedRoute from "@/components/ProtectedRoute";

import {
    Category,
    EditPost,
    Login,
    Media,
    NewPost,
    PostList,
    Slider,
} from "@/pages/admin";

const router = createBrowserRouter([
    {
        path: "/",
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
                path: "slider",
                element: <Slider />,
            },
            {
                path: "post",
                element: <PostList />,
            },
            {
                path: "post/new",
                element: <NewPost />,
            },
            {
                path: "post/edit/:id",
                element: <EditPost />,
            },
            {
                path: "*",
                element: <h1>404</h1>,
            },
        ],
    },
    {
        path: "*",
        element: <h1>404</h1>,
    },
    {
        path: "/login",
        element: <Login />,
    },
]);

export default router;
