import { createBrowserRouter } from "react-router-dom";

import AdminLayout from "@/layouts/Admin";
import ClientLayout from "@/layouts/Client";

import {
    ArticleList,
    Category,
    ContactList,
    CreateArticle,
    EditArticle,
} from "@/pages/admin";
import Login from "@/pages/admin/Login";
import { Article, Contact, Home } from "@/pages/client";
import ProtectedRoute from "@/components/ProtectedRoute";

const router = createBrowserRouter([
    {
        path: "",
        element: <ClientLayout />,
        children: [
            {
                path: "",
                element: <Home />,
            },
            {
                path: "contact",
                element: <Contact />,
            },
            {
                path: "articles",
                element: <Article />,
            },
            {
                path: "products",
                element: <Article />,
            },
            {
                path: "*",
                element: <div>404 Error</div>,
            },
        ],
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
                path: "dashboard",
                element: <div>Dashboard</div>,
            },
            {
                path: "article",
                element: <ArticleList />,
            },
            {
                path: "article/create",
                element: <CreateArticle />,
            },
            {
                path: "article/edit/:id",
                element: <EditArticle />,
            },
            {
                path: "category",
                element: <Category />,
            },
            {
                path: "contact",
                element: <ContactList />,
            },
            {
                path: "slider",
                element: <div>Slider</div>,
            },
            {
                path: "*",
                element: <div>404 Error</div>,
            },
        ],
    },
    {
        path: "/sign-in",
        element: <Login />,
    },
]);

export default router;
