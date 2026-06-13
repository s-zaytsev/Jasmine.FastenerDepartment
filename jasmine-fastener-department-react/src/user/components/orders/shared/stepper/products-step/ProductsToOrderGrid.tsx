import {Box} from "@mui/material";
import {ReorderOutlined} from "@mui/icons-material";
import type {ProductToOrder} from "../../../../../models/productsToOrderModels.ts";
import Typography from "../../../../../../shared/components/Typography.tsx";
import useGroup from "../../../../../../shared/hooks/useGroup.ts";
import Section from "../../../../../../shared/components/section/Section.tsx";
import ProductsToOrderGridSectionTable from "./ProductsToOrderGridSectionTable.tsx";
import type {ChangeOrderProduct} from "../../../../../models/orderModels.ts";
import {memo, useMemo} from "react";

type ProductsToOrderFormProps = {
    products?: ProductToOrder[];
    productsInOrder?: ChangeOrderProduct[];
    onMoveToOrder: (product: ProductToOrder) => void;
}

const ProductsToOrderGrid = (props: ProductsToOrderFormProps) => {
    const {
        groupBy
    } = useGroup();
    
    const groupedByType = useMemo(() => {
        return groupBy(
            props.products || [],
            x => x.product.type?.name ?? 'Остальное',
            {
                sortFn: (a, b) => a.product.name.localeCompare(b.product.name),
                sortGroups: true
            });
        
    }, [groupBy, props.products]);

    if (Object.keys(groupedByType).length === 0) {
        return (
            <Box className={'flex items-center'}>
                <Typography variant={'bodyRegular'}>Список продуктов для заказа пуст.{'\n'}
                    Кликните по иконке <ReorderOutlined color={"disabled"}/> в списке товаров для
                    добавления.</Typography>
            </Box>
        );
    }

    return (
        <Box className={"w-full px-[2rem] flex flex-col gap-[2rem]"}>
            {Object.entries(groupedByType).map(([key, value]) => (
                <Box key={key}>
                    <Section title={key}>
                        <ProductsToOrderGridSectionTable
                            products={value}
                            productsInOrder={props.productsInOrder}
                            onMoveToOrder={props.onMoveToOrder}
                        />
                    </Section>
                </Box>
            ))}
        </Box>
    );
}

export default memo(ProductsToOrderGrid);