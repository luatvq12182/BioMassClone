import slugify from 'slugify';
import { useForm } from "react-hook-form";
import { Box, Typography } from "@mui/material";

import { Category } from "@/models/Category";
import useCreateCategory from "@/hooks/mutations/useCreateCategory";
import { Input, Button, Card } from "@/components/common";

type Props = {
    isLoading: boolean;
};

const defaultValues = {
    name: "",
    slug: "",
}

const CategoryForm = ({ isLoading }: Props) => {
    const {
        reset,
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<Category>({
        defaultValues
    });
    const { mutate: createCategory, isLoading: isCreating } = useCreateCategory({
        onSuccess: () => {
            reset({
                name: "",
                slug: "",
            });
        },
    });

    const submitHandler = (data: Category) => {
        const { name, slug } = data;

        createCategory({
            name: name,
            slug: slug.trim() ? slug : slugify(name.trim().toLowerCase()) 
        });
    };

    return (
        <Card title='New Category'>
            <Box
                component='form'
                onSubmit={handleSubmit(submitHandler)}
            >
                <Input
                    sx={{ mb: 2 }}
                    error={!!errors["name"]}
                    label='Category name'
                    {...register("name", {
                        required: true,
                    })}
                />

                <Input
                    label='Slug'
                    {...register("slug")}
                />
                <Typography 
                    fontStyle={'italic'} 
                    variant='caption'
                >
                    (Auto-generated if not provided)
                </Typography>

                <Button
                    type='submit'
                    fullWidth
                    disabled={isLoading}
                    loading={isCreating}
                    sx={{ mt: 2 }}
                >
                    Add to List
                </Button>
            </Box>
        </Card>
    );
};

export default CategoryForm;
