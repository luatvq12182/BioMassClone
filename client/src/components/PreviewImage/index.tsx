import { MouseEvent, useState } from "react";
import { Box } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import ZoomInIcon from "@mui/icons-material/ZoomIn";
import ZoomOutIcon from "@mui/icons-material/ZoomOut";
import UndoIcon from "@mui/icons-material/Undo";
import RedoIcon from "@mui/icons-material/Redo";

import { IconButton } from "../common";

type Props = {
    data: string;
    isOpen: boolean;
    onClose: () => void;
};

const PreviewImage = ({ data, isOpen, onClose }: Props) => {
    const [scale, setScale] = useState<number>(1);
    const [rotate, setRotate] = useState<number>(0);

    const toolbar = [
        {
            icon: <UndoIcon />,
            onClick: (e: MouseEvent<HTMLElement>) => {
                e.stopPropagation();
                setRotate(rotate - 90);
            },
        },
        {
            icon: <RedoIcon />,
            onClick: (e: MouseEvent<HTMLElement>) => {
                e.stopPropagation();
                setRotate(rotate + 90);
            },
        },
        {
            icon: <ZoomOutIcon />,
            onClick: (e: MouseEvent<HTMLElement>) => {
                e.stopPropagation();
                setScale(scale - 0.1);
            },
        },
        {
            icon: <ZoomInIcon />,
            onClick: (e: MouseEvent<HTMLElement>) => {
                e.stopPropagation();
                setScale(scale + 0.1);
            },
        },
        {
            icon: <CloseIcon />,
            onClick: onClose,
        },
    ];

    return (
        <Box
            sx={{
                display: isOpen ? "flex" : "none",
                alignItems: "center",
                justifyContent: "center",
                position: "fixed",
                top: 0,
                left: 0,
                width: "100%",
                height: "100%",
                zIndex: 1101,
                backgroundColor: "rgba(0, 0, 0, 0.9)",
                transitionDuration: "0.2s",
            }}
            onClick={onClose}
        >
            <Box
                sx={{
                    position: "absolute",
                    top: 10,
                    right: 40,
                    display: "flex",
                    zIndex: 1,
                    transform: `scale(1.3)`,
                }}
            >
                {toolbar.map(({ icon, onClick }, index) => {
                    return (
                        <IconButton
                            sx={{
                                color: "#fff",
                            }}
                            onClick={onClick}
                            key={index}
                        >
                            {icon}
                        </IconButton>
                    );
                })}
            </Box>

            <Box
                component='img'
                src={data}
                alt='img preview'
                sx={{
                    transition: "transform .15s",
                    maxWidth: "80vw",
                    maxHeight: "80vh",
                    transform: `rotate(${rotate}deg) scale(${scale})`,
                }}
                onClick={(e: MouseEvent<HTMLElement>) => {
                    e.stopPropagation();
                }}
            ></Box>
        </Box>
    );
};

export default PreviewImage;
