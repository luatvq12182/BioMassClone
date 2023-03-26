import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { CategoryProvider, ICategory } from "@/modules/category";
import { Button } from "primereact/button";

type Props = {};

const Table = ({}: Props) => {
    return (
        <CategoryProvider
            render={(categories: ICategory[]) => {
                return (
                    <DataTable
                        value={categories}
                        size="small"
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
                            // headerStyle={{ textAlign: "center" }}
                            // style={{ textAlign: "center" }}
                            body={() => {
                                return (
                                    <div>
                                        <Button
                                            icon='pi pi-pencil'
                                            rounded
                                            text
                                            aria-label='Filter'
                                        />
                                        <Button
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
