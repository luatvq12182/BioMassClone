import { Grid, Typography } from "@mui/material";

import { Card } from "@/components/common";
import CategoryForm from "./components/CategoryForm";
import CategoryTable from "./components/CategoryTable";

import useCategories from "@/hooks/queries/useCategories";

const Category = () => {
    const { data, isFetching, error } = useCategories();

    if (error) {
        return <Typography>Something went wrong!!!</Typography>;
    }

    return (
        <Grid
            container
            spacing={2}
        >
            <Grid
                item
                xs={12}
                xl={4}
            >
                <CategoryForm isLoading={isFetching} />
            </Grid>

            <Grid
                item
                xs={12}
                xl={8}
            >
                <Card title='Category List'>
                    <CategoryTable
                        data={data?.data || []}
                        isLoading={isFetching}
                    />
                </Card>
            </Grid>
        </Grid>
    );
};

export default Category;
