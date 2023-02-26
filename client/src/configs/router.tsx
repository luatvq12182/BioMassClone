import { createBrowserRouter } from "react-router-dom";

import AdminLayout from "@/layouts/Admin";
import ClientLayout from "@/layouts/Client";

import {
    ArticleList,
    Category,
    ContactList,
    CreateArticle,
    EditArticle,
    FileManager,
} from "@/pages/admin";
import SignIn from "@/pages/admin/SignIn";
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
                element: <h1>Hello, Leo Yeager</h1>,
            },
            {
                path: "file-manager",
                element: <FileManager />
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
        element: <SignIn />,
    },
]);

export default router;
