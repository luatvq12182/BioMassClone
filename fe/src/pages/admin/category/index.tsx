import { useState } from "react";
import { ICategory } from "@/modules/category";
import { Card } from "primereact/card";
import { TabView, TabPanel } from "primereact/tabview";
import Form from "./components/Form";
import Table from "./components/Table";
import { useLangs } from "@/modules/lang/queries";

type Props = {};

const Category = ({}: Props) => {
    const [payload, setPayload] = useState<ICategory[]>([]);
    const { data: langs } = useLangs();

    console.log('langs:', langs)

    const handleChange = () => {
        
    }

    return (
        <div className='grid grid-cols-3 gap-4'>
            <div>
                <Card title='Create category'>
                    <TabView>
                        <TabPanel header='Standard'>
                            <Form />
                        </TabPanel>
                        <TabPanel header='Eng'>
                            <Form />
                        </TabPanel>
                        <TabPanel header='Vie'>
                            <Form />
                        </TabPanel>
                    </TabView>
                </Card>
            </div>

            <div className='col-span-2'>
                <Card title='Category list'>
                    <Table />
                </Card>
            </div>
        </div>
    );
};

export default Category;
