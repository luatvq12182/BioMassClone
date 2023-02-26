import { useEffect, KeyboardEvent } from "react";
import { useForm } from "react-hook-form";
import slugify from 'slugify';
import {
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Typography,
} from "@mui/material";

import { Button, Input } from "@/components/common";
import { Category } from "@/models/Category";
import useUpdateCategory from "@/hooks/mutations/useUpdateCategory";

type Props = {
    data: Category | null;
    isOpen: boolean;
    onClose: () => void;
};

const CategoryDialog = ({ data, isOpen, onClose }: Props) => {
    const {
        reset,
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<Category>();
    const { mutate: updateCategory, isLoading: isUpdating } = useUpdateCategory(
        {
            onSuccess: onClose,
        }
    );

    const submitHandler = (data: Category) => {
        const { name, slug, id } = data;

        updateCategory({
            id: id,
            name: name,
            slug: slug.trim() ? slug : slugify(name.trim().toLowerCase()) 
        });
    };

    useEffect(() => {
        if (data) {
            reset(data);
        }
    }, [data, reset]);

    const onPressEnter = (e: KeyboardEvent<HTMLInputElement>) => {
        if (e.key === "Enter") {
            handleSubmit(submitHandler)();
        }
    };

    return (
        <Dialog
            open={isOpen}
            onClose={onClose}
            aria-labelledby='alert-dialog-title'
            aria-describedby='alert-dialog-description'
        >
            <DialogTitle id='alert-dialog-title'>Edit Category</DialogTitle>
            <DialogContent>
                <Input
                    sx={{ mt: 1 }}
                    error={!!errors["name"]}
                    label='Category name'
                    {...register("name", {
                        required: true,
                    })}
                    onKeyDown={onPressEnter}
                />

                <Input
                    label='Slug'
                    sx={{ mt: 2 }}
                    {...register("slug")}
                    onKeyDown={onPressEnter}
                />
                <Typography 
                    fontStyle={'italic'} 
                    variant='caption'
                >
                    (Auto-generated if not provided)
                </Typography>
            </DialogContent>
            <DialogActions>
                <Button
                    variant='text'
                    onClick={onClose}
                    disabled={isUpdating}
                >
                    Cancel
                </Button>
                <Button
                    onClick={handleSubmit(submitHandler)}
                    autoFocus
                    loading={isUpdating}
                >
                    Submit
                </Button>
            </DialogActions>
        </Dialog>
    );
};

export default CategoryDialog;
