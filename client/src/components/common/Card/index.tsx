import { ReactNode } from "react";
import {
    Card as MUICard,
    CardProps,
    CardContent,
    Typography,
} from "@mui/material";

type Props = {
    title?: ReactNode;
};

const Card = ({ title, children, ...rest }: Props & CardProps) => {
    return (
        <MUICard {...rest}>
            <Typography
                fontSize={20}
                fontWeight={500}
                sx={{ pl: 2, pt: 1 }}
            >
                {title}
            </Typography>

            <CardContent>{children}</CardContent>
        </MUICard>
    );
};

export default Card;
