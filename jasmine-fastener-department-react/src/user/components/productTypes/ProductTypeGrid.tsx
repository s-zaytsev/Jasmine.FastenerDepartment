import type {ExtendedProductType} from "../../models/productTypeModels.ts";
import {Box, Grow} from "@mui/material";
import Typography from "../../../shared/components/Typography.tsx";
import ProductTypeGridCard from "./ProductTypeGridCard.tsx";

type ProductTypeGridProps = {
    productTypes: ExtendedProductType[];
    onEdit: (id: string) => void;
}

const ProductTypeGrid = (props: ProductTypeGridProps) => {
    if (props.productTypes.length === 0) {
        return (
            <Box className={'h-full w-full items-center flex justify-center'}>
                <Typography variant={'headlineH3'}>Список типов товаров пуст</Typography>
            </Box>
        )
    }

    return (
        <Box className={'flex flex-wrap gap-[1rem]'}>
            {props.productTypes.map((productType, index) =>
                <Grow key={productType.id} in={true} timeout={index * 150}>
                    <Box className={'w-[23%]'}>
                        <ProductTypeGridCard
                            productType={productType}
                            onEdit={props.onEdit}
                        />
                    </Box>
                </Grow>
            )}
        </Box>
    );
}

export default ProductTypeGrid;