import { createTheme } from "@mui/material/styles";

// Base on bootstrap breakpoints => https://getbootstrap.com/docs/5.0/layout/breakpoints/
const theme = createTheme({
    breakpoints: {
        values: {
            xs: 0,
            sm: 576,
            md: 768,
            lg: 992,
            xl: 1200,
        },
    },
});

export default theme;
