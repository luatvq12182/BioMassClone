import { Outlet } from "react-router-dom";
import { Box, Container } from "@mui/material";

import Header from "./Header";

const AdminLayout = () => {
    return (
        <Box
            bgcolor='#F1F5F9'
            sx={{
                minHeight: "100vh",
            }}
        >
            <Header />

            <Container
                maxWidth='xl'
                sx={{ mt: 2 }}
            >
                <Outlet />
            </Container>
        </Box>
    );
};

export default AdminLayout;
