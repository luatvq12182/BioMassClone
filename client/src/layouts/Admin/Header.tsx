import { useState } from "react";
import { useNavigate } from "react-router-dom";
import {
    AppBar,
    Box,
    Drawer,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
    ListItemButton,
    Toolbar,
    Typography,
    IconButton,
    MenuItem,
    Menu,
} from "@mui/material/";
import AccountCircle from "@mui/icons-material/AccountCircle";
import MenuIcon from "@mui/icons-material/Menu";

import useAuth from "@/hooks/useAuth";
import navigations from "@/configs/admin-navigation";

export default function Header() {
    const navigate = useNavigate();
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const [isOpenDrawer, setIsOpenDrawer] = useState<boolean>(false);
    const { signOut } = useAuth();

    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const handleSignOut = () => {
        signOut(() => {
            localStorage.removeItem("accessToken");
            localStorage.removeItem("user");
        });
    };

    const renderMenuItems = () => (
        <Box
            role='presentation'
            onClick={() => {
                setIsOpenDrawer(!isOpenDrawer);
            }}
            onKeyDown={() => {
                setIsOpenDrawer(!isOpenDrawer);
            }}
            sx={{
                width: 250,
            }}
        >
            <List>
                {navigations.map(({ label, to, icon }) => {
                    return (
                        <ListItem
                            key={to}
                            disablePadding
                            onClick={() => {
                                navigate(to);
                            }}
                        >
                            <ListItemButton>
                                <ListItemIcon>{icon}</ListItemIcon>
                                <ListItemText primary={label} />
                            </ListItemButton>
                        </ListItem>
                    );
                })}
            </List>
        </Box>
    );

    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position='static'>
                <Toolbar>
                    <IconButton
                        size='large'
                        edge='start'
                        color='inherit'
                        aria-label='menu'
                        sx={{ mr: 2 }}
                        onClick={() => setIsOpenDrawer(!isOpenDrawer)}
                    >
                        <MenuIcon />
                    </IconButton>
                    <Typography
                        variant='h6'
                        component='div'
                        sx={{ flexGrow: 1 }}
                    >
                        Bio Mass Clone
                    </Typography>

                    <Box>
                        <IconButton
                            size='large'
                            aria-label='account of current user'
                            aria-controls='menu-appbar'
                            aria-haspopup='true'
                            onClick={handleMenu}
                            color='inherit'
                        >
                            <AccountCircle />
                        </IconButton>

                        <Menu
                            id='menu-appbar'
                            anchorEl={anchorEl}
                            anchorOrigin={{
                                vertical: "top",
                                horizontal: "right",
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: "top",
                                horizontal: "right",
                            }}
                            open={Boolean(anchorEl)}
                            onClose={handleClose}
                        >
                            <MenuItem onClick={handleClose}>Profile</MenuItem>
                            <MenuItem onClick={handleSignOut}>
                                Sign Out
                            </MenuItem>
                        </Menu>
                    </Box>
                </Toolbar>
            </AppBar>

            <Drawer
                anchor={"left"}
                open={isOpenDrawer}
                onClose={() => {
                    setIsOpenDrawer(!isOpenDrawer);
                }}
            >
                {renderMenuItems()}
            </Drawer>
        </Box>
    );
}
