import {type OrderProduct, OrderStatusCode} from "../../../models/orderModels.ts";
import type {ProductType} from "../../../models/productTypeModels.ts";
import {Box, Grow} from "@mui/material";
import useGroup from "../../../../shared/hooks/useGroup.ts";
import Section from "../../../../shared/components/section/Section.tsx";
import OrderDetailsGridSectionTable from "./OrderDetailsGridSectionTable.tsx";

type OrderDetailsGridProps = {
    orderStatusCode?: OrderStatusCode;
    products?: OrderProduct[];
    productTypes: ProductType[];
}

const OrderDetailsGrid = (props: OrderDetailsGridProps) => {
    const {groupBy} = useGroup();
    const groupedByType = groupBy(
        props.products || [],
        x => x.productType?.name ?? 'Остальное',
        {
            sortFn: (a, b) => a.productName.localeCompare(b.productName),
            sortGroups: true
        });

    return (
        <Box className={'w-full mt-[1rem] flex flex-col gap-[2rem]'}>
            {
                Object.entries(groupedByType).map(([key, value], index) => (
                    <Grow key={key} in={true} timeout={index * 150}>
                        <Box>
                            <Section title={key} itemsCount={value.length}>
                                <OrderDetailsGridSectionTable products={value} orderStatusCode={props.orderStatusCode}/>
                            </Section>
                        </Box>
                    </Grow>
                ))
            }
        </Box>
    );
}

export default OrderDetailsGrid;