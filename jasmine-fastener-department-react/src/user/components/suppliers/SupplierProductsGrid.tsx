import type {Page, TableColumnDefinition} from "../../../shared/models/models.ts";
import {ProductsQueryParameter} from "../../models/productModel.ts";
import {Box} from "@mui/material";
import SortTableHead from "../../../shared/components/tables/SortTableHead.tsx";
import TablePagination from "../../../shared/components/tables/TablePagination.tsx";
import type {SupplierProduct, SupplierProductsQuery} from "../../models/supplierModels.ts";
import SupplierProductGridRow from "./SupplierProductGridRow.tsx";
import Typography from "../../../shared/components/Typography.tsx";

type SupplierProductGridProps = {
    page?: Page<SupplierProduct>;
    query: SupplierProductsQuery;
    onSort: (parameter: number) => void;
    onPageNoChanged: (parameter: number) => void;
    onPageSizeChanged: (parameter: number) => void;
    onNavigateToProduct: (parameter: string) => void;
    onChangePrintStatus: (parameter: string) => void;
    onChangeOrderStatus: (parameter: string) => void;
    onOpenDialog: (parameter: string) => void;
}

const SupplierProductsGrid = (props: SupplierProductGridProps) => {
    const columns: TableColumnDefinition[] = [
        {title: "Ценник", parameter: ProductsQueryParameter.priceTagCode, width: '15%'},
        {title: "Артикул", parameter: ProductsQueryParameter.productNumber, width: '15%'},
        {title: "Артикул поставщика", width: '25%'},
        {title: "Наименование", parameter: ProductsQueryParameter.name},
        {title: "Цена", parameter: ProductsQueryParameter.price, width: '20%'},
        {title: '', width: '30%'},
    ];

    if (props.page?.items.length === 0) {
        return (
            <Box className={'h-full w-full items-center flex justify-center'}>
                <Typography variant={'headlineH3'}>Список товаров пуст</Typography>
            </Box>
        )
    }

    return (
        <Box sx={{p: '10px 0'}}>
            <SortTableHead onSort={props.onSort} columns={columns} sortBy={props.query.sortBy}
                           sortDesc={props.query.sortDesc}/>
            {props?.page?.items?.map((row: SupplierProduct) => (
                <SupplierProductGridRow
                    key={row.id}
                    supplierProduct={row}
                    headColumns={columns}
                    onNavigateToProduct={props.onNavigateToProduct}
                    onChangeOrderStatus={props.onChangeOrderStatus}
                    onChangePrintStatus={props.onChangePrintStatus}
                    onOpenDialog={props.onOpenDialog}
                />
            ))}

            {
                !props?.page?.items && <p className={'text-center'} style={{margin: '2rem'}}>Товары отсутствуют</p>
            }

            <TablePagination
                variant="bottom"
                pageNo={props.query.pageNo}
                pageSize={props.query.pageSize}
                totalCount={props.page?.totalCount}
                onPageNoChange={props.onPageNoChanged}
                onPageSizeChange={props.onPageSizeChanged}/>
        </Box>
    );
}

export default SupplierProductsGrid;