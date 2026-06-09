import {Box} from "@mui/material";
import {PriceTagCode, type Product} from "../../models/productModel.ts";
import {PrintOutlined, ReorderOutlined} from "@mui/icons-material";
import type {TableColumnDefinition} from "../../../shared/models/models.ts";
import ProductPrice from "../../../shared/components/ProductPrice.tsx";
import SuppliersTableCell from "../shared/SuppliersTableCell.tsx";
import type {ProductType} from "../../models/productTypeModels.ts";
import Typography from "../../../shared/components/Typography.tsx";
import TableRow from "../../../shared/components/tables/TableRow.tsx";
import Chip from "../../../shared/components/Chip.tsx";
import IconButton from "../../../shared/components/buttons/IconButton.tsx";

type ProductGridRowProps = {
    product: Product;
    productTypes: ProductType[];
    headColumns?: TableColumnDefinition[];
    onNavigateToProduct: (parameter: string) => void;
    onChangePrintStatus: (parameter: string) => void;
    onChangeOrderStatus: (parameter: string) => void;
}

const ProductGridRow = (props: ProductGridRowProps) => {

    function getWidth(columnNumber: number) {
        return props.headColumns?.at(columnNumber)?.width || '100%'
    }

    return (
        <TableRow hasHighlight={true} onClick={() => props.onNavigateToProduct(props.product.id)}>

            <Box width={getWidth(0)}>
                <Typography variant={'bodyRegular'} color={'primary'}>{props.product.number}</Typography>
            </Box>

            <Box width={getWidth(1)}>
                <Typography variant={'bodyRegularBold'}>{props.product.name}</Typography>
            </Box>

            <Box width={getWidth(2)}>
                <ProductPrice product={props.product}/>
            </Box>

            <Box width={getWidth(3)}>
                <Box className={'pr-[0.5rem]'}>
                    <Chip title={PriceTagCode[props.product.priceTagCode].toUpperCase()}/>
                </Box>
            </Box>

            <Box width={getWidth(4)}>
                {props.productTypes.find(x => x.id === props.product.type?.id)?.name}
            </Box>

            <Box width={getWidth(5)}>
                <SuppliersTableCell suppliers={props.product.suppliers}/>
            </Box>

            <Box className={'flex items-center gap-[1rem]'} width={getWidth(6)}>
                <IconButton
                    isActive={props.product.isNeededToPrint}
                    description={!props.product.isNeededToPrint ? 'Добавить в список печати' : 'Убрать из списка печати'}
                    onClick={(e) => {
                        e.stopPropagation();
                        props.onChangePrintStatus(props.product.id)
                    }}
                >
                    <PrintOutlined/>
                </IconButton>

                <IconButton
                    isActive={props.product.isNeededToOrder}
                    description={!props.product.isNeededToOrder ? 'Добавить в список заказа' : 'Убрать из списка заказа'}
                    onClick={(e) => {
                        e.stopPropagation();
                        props.onChangeOrderStatus(props.product.id)
                    }}
                >
                    <ReorderOutlined/>
                </IconButton>
            </Box>
        </TableRow>
    );
}

export default ProductGridRow;