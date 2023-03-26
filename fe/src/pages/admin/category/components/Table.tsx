import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import {
    CategoryProvider,
    ICategory,
    useDeleteCategory,
} from "@/modules/category";
import { Button } from "primereact/button";
import { showConfirm } from "@/utils";

type Props = {
    langCode?: string;
    onOpenUpdateDialog: (id: number) => void;
};

const Table = ({ langCode, onOpenUpdateDialog }: Props) => {
    const { mutate: deleteCategory, isLoading: isDeleting } =
        useDeleteCategory();

    const handleClickDelete = (id: number) => {
        showConfirm(() => deleteCategory(id));
    };

    return (
        <CategoryProvider
            langCode={langCode}
            render={(categories: ICategory[], isLoading) => {
                return (
                    <DataTable
                        value={categories}
                        loading={isLoading}
                        size='small'
                        showGridlines
                        tableStyle={{ minWidth: "50rem" }}
                    >
                        <Column
                            field='index'
                            header='#'
                            body={(_, e) => {
                                return e.rowIndex + 1;
                            }}
                        ></Column>
                        <Column
                            field='name'
                            header='Name'
                        ></Column>
                        <Column
                            field='slug'
                            header='Slug'
                        ></Column>
                        <Column
                            field='action'
                            header={
                                <i
                                    className='pi pi-spin pi-cog'
                                    style={{ fontSize: "1.5rem" }}
                                ></i>
                            }
                            style={{ width: "150px", textAlign: "center" }}
                            body={(e) => {
                                return (
                                    <div>
                                        <Button
                                            disabled={isDeleting}
                                            icon='pi pi-pencil'
                                            rounded
                                            text
                                            aria-label='Filter'
                                            onClick={() => {
                                                onOpenUpdateDialog(e.categoryId ||  e.id);
                                            }}
                                        />
                                        <Button
                                            disabled={isDeleting}
                                            onClick={() =>
                                                handleClickDelete(e.categoryId ||  e.id)
                                            }
                                            icon='pi pi-trash'
                                            rounded
                                            text
                                            severity='danger'
                                            aria-label='Bookmark'
                                        />
                                    </div>
                                );
                            }}
                        ></Column>
                    </DataTable>
                );
            }}
        />
    );
};

export default Table;
