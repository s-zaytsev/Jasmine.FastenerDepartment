import {PriceTagCode} from "../../models/productModel.ts";
import type {TableColumnDefinition} from "../../../shared/models/models.ts";
import {Box, Button} from "@mui/material";
import ProductPrice from "../../../shared/components/ProductPrice.tsx";
import {Edit, PrintOutlined, ReorderOutlined} from "@mui/icons-material";
import type {SupplierProduct} from "../../models/supplierModels.ts";
import TableRow from "../../../shared/components/tables/TableRow.tsx";

type SupplierProductGridRowProps = {
    supplierProduct: SupplierProduct;
    headColumns?: TableColumnDefinition[];
    onNavigateToProduct: (parameter: string) => void;
    onChangePrintStatus: (parameter: string) => void;
    onChangeOrderStatus: (parameter: string) => void;
    onOpenDialog: (parameter: string) => void;
}

const SupplierProductGridRow = (props: SupplierProductGridRowProps) => {

    function getWidth(columnNumber: number) {
        return props.headColumns?.at(columnNumber)?.width || '100%'
    }

    return (
        <TableRow hasHighlight={true} onClick={() => props.onNavigateToProduct(props.supplierProduct.product.id)}>
            <Box
                width={getWidth(0)}>{PriceTagCode[props.supplierProduct.product.priceTagCode].toUpperCase()}</Box>
            <Box width={getWidth(1)}>{props.supplierProduct.product.number}</Box>
            <Box width={getWidth(2)}>
                {props.supplierProduct.number}
            </Box>
            <Box width={getWidth(3)}>{props.supplierProduct.product.name}</Box>
            <Box
                width={getWidth(4)}> <ProductPrice product={props.supplierProduct.product}/>
            </Box>
            <Box width={getWidth(5)}>
                <Button onClick={(e) => {
                    e.stopPropagation();
                    props.onOpenDialog(props.supplierProduct.id)
                }}>
                    <Edit/>
                </Button>

                <Button onClick={(e) => {
                    e.stopPropagation();
                    props.onChangePrintStatus(props.supplierProduct.product.id)
                }}>
                    {props.supplierProduct.product.isNeededToPrint ? <PrintOutlined/> :
                        <PrintOutlined color={'action'}/>}
                </Button>

                <Button onClick={(e) => {
                    e.stopPropagation();
                    props.onChangeOrderStatus(props.supplierProduct.product.id)
                }}>
                    {props.supplierProduct.product.isNeededToOrder ? <ReorderOutlined/> :
                        <ReorderOutlined color={'action'}/>}
                </Button>
            </Box>
        </TableRow>
    );
}

export default SupplierProductGridRow;