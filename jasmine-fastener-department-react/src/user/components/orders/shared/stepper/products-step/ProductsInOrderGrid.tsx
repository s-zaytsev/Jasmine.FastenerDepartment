import {Box} from "@mui/material";
import {AddOutlined} from "@mui/icons-material";
import Typography from "../../../../../../shared/components/Typography.tsx";
import useGroup from "../../../../../../shared/hooks/useGroup.ts";
import type {ChangeOrderProduct} from "../../../../../models/orderModels.ts";
import Section from "../../../../../../shared/components/section/Section.tsx";
import ProductsInOrderGridSectionTable from "./ProductsInOrderGridSectionTable.tsx";
import IconBox from "../../../../../../shared/components/IconBox.tsx";

type ProductsInOrderFormProps = {
    products?: ChangeOrderProduct[];
    onRemoveFromOrder: (product: ChangeOrderProduct) => void;
}

const ProductsInOrderGrid = (props: ProductsInOrderFormProps) => {
    const {groupBy} = useGroup();
    const groupedByType = groupBy(
        props.products || [],
        x => x.productType?.name ?? 'Остальное',
        {
            sortFn: (a, b) => a.productName.localeCompare(b.productName),
            sortGroups: true
        });

    if (Object.keys(groupedByType).length === 0) {
        return (
            <Box className={'h-full flex flex-col justify-center text-center items-center'}>
                <Typography variant={'bodyRegular'}>
                    Список товаров заказа пуст.
                </Typography>
                <Typography variant={'bodyRegular'} sx={{display: 'flex', gap: '0.5rem'}}>
                    Кликните по иконке <IconBox><AddOutlined/></IconBox> в списке товаров для заказа.
                </Typography>
            </Box>
        );
    }

    return (
        <Box className={"w-full px-[2rem] flex flex-col gap-[2rem]"}>
            {Object.entries(groupedByType).map(([key, value]) => (
                <Box key={key}>
                    <Section title={key}>
                        <ProductsInOrderGridSectionTable
                            products={value}
                            onRemoveFromOrder={props.onRemoveFromOrder}
                        />
                    </Section>
                </Box>
            ))}
        </Box>
    );
}

export default ProductsInOrderGrid;