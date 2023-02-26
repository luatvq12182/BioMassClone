import { DataGrid, DataGridProps } from "@mui/x-data-grid";

type Props = {};

const Table = (props: Props & DataGridProps) => {
    return (
        <DataGrid
            sx={{ height: "300px" }}
            rowHeight={35}
            headerHeight={40}
            {...props}
        />
    );
};

export default Table;
