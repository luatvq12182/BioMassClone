import { useState } from 'react';
import { Box } from "@mui/material";
import { GridColumns, GridRowParams } from "@mui/x-data-grid";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";

import { Category } from "@/models/Category";
import { IconButton, Table } from "@/components/common";
import { showConfirm } from "@/utils";
import useDeleteCategory from "@/hooks/mutations/useDeleteCategory";
import CategoryDialog from "./CategoryDialog";

type Props = {
    data: Category[];
    isLoading: boolean;
};

const CategoryTable = ({ isLoading, data }: Props) => {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const [rowSelected, setRowSelected] = useState<Category | null>(null);
    const { mutate: deleteCategory, isLoading: isDeleting } = useDeleteCategory({});

    const handleClickDelete = (id: number) => {
        showConfirm(
            `You are about to permanently delete these items from your site. \nThis action cannot be undone. \n'Cancel' to stop, 'OK' to delete.`,
            () => deleteCategory(id)
        );
    };

    const handleClickEdit = (category: Category) => {
        setIsOpen(true);
        setRowSelected(category);
    };

    const onClose = () => setIsOpen(false);

    const columns: GridColumns<Category> = [
        { field: "name", headerName: "Name", width: 250 },
        { field: "slug", headerName: "Slug", width: 250 },
        {
            field: "action",
            headerName: "",
            width: 150,
            align: "center",
            renderCell: (params) => {
                return (
                    <Box>
                        <IconButton
                            color='info'
                            disabled={isLoading || isDeleting}
                            onClick={() => {
                                handleClickEdit(params.row)
                            }}
                        >
                            <EditIcon />
                        </IconButton>
                        <IconButton
                            color='error'
                            disabled={isLoading || isDeleting}
                            onClick={() => handleClickDelete(params.row.id!)}
                        >
                            <DeleteIcon />
                        </IconButton>
                    </Box>
                );
            },
        },
    ];

    return (
        <>
            <CategoryDialog
                isOpen={isOpen}
                onClose={onClose}
                data={rowSelected}
            />

            <Table
                rows={data}
                columns={columns}
                loading={isLoading}
            />
        </>
    );
};

export default CategoryTable;
