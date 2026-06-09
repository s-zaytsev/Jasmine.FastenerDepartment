import {Box} from "@mui/material";
import {type Product, type ProductsQuery, ProductsQueryParameter} from "../../models/productModel.ts";
import ProductGridRow from "./ProductGridRow.tsx";
import SortTableHead from "../../../shared/components/tables/SortTableHead.tsx";
import type {Page, TableColumnDefinition} from "../../../shared/models/models.ts";
import TablePagination from "../../../shared/components/tables/TablePagination.tsx";
import type {ProductType} from "../../models/productTypeModels.ts";

type ProductGridProps = {
    page?: Page<Product>;
    query: ProductsQuery;
    productTypes: ProductType[];
    onSort: (parameter: number) => void;
    onPageNoChanged: (parameter: number) => void;
    onPageSizeChanged: (parameter: number) => void;
    onNavigateToProduct: (parameter: string) => void;
    onChangePrintStatus: (parameter: string) => void;
    onChangeOrderStatus: (parameter: string) => void;
}

const ProductsGrid = (props: ProductGridProps) => {
    const columns: TableColumnDefinition[] = [
        {title: "Артикул", parameter: ProductsQueryParameter.productNumber, width: '15%'},
        {title: "Наименование", parameter: ProductsQueryParameter.name},
        {title: "Цена", parameter: ProductsQueryParameter.price, width: '25%'},
        {title: "Ценник", parameter: ProductsQueryParameter.priceTagCode, width: '15%', columnAlign: 'center'},
        {title: "Тип", parameter: ProductsQueryParameter.type, width: '30%'},
        {title: "Поставщики", width: '30%'},
        {title: '', width: '20%'},
    ];

    return (
        <Box sx={{p: '10px 0'}}>
            <SortTableHead
                onSort={props.onSort}
                columns={columns}
                sortBy={props.query.sortBy}
                sortDesc={props.query.sortDesc}
            />
            {props?.page?.items?.map((row: Product) => (
                <ProductGridRow
                    key={row.id}
                    product={row}
                    productTypes={props.productTypes}
                    headColumns={columns}
                    onNavigateToProduct={props.onNavigateToProduct}
                    onChangeOrderStatus={props.onChangeOrderStatus}
                    onChangePrintStatus={props.onChangePrintStatus}
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

export default ProductsGrid;