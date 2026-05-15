import {Box} from "@mui/material";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";
import Page from "../../../shared/components/layout/Page";
import ProductsGrid from "../shared/ProductsGrid.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import ProductPageFiltersComponent from "./productFilters/ProductPageFiltersComponent.tsx";
import {useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import type {ProductsPageState} from "../../models/productModel.ts";
import useProductsPage from "./useProductsPage.ts";

const ProductsPage = () => {
    const state = useAppSelector<ProductsPageState>(
        (state) => state.products
    );

    const {
        handleNavigateToCreate,
        handleNavigateToProduct,
        handleChangePrintStatus,
        handleChangeOrderStatus,
        handleSort,
        handlePageSizeChange,
        handlePageNoChange,
        handleSearch,
        handleSupplierFilterChange,
        handleTypeFilterChange,
        handlePriceTagFilterChange,
        handleOnlyToPrintFilterChange,
        handleOnlyToOrderFilterChange,
        handlePriceRangeChange,
        handleResetOnlyToOrderFilter,
        handleResetPriceRange,
        handleResetTypeFilters,
        handleResetSupplierFilters,
        handleResetOnlyToPrintFilter,
        handleResetPriceTagFilters
    } = useProductsPage();

    if (state.loading) {
        setTimeout(() => {
            return <Loader text={'Загрузка списка товаров'}/>
        }, 300);
    }

    return (
        <Page>
            <Box className={"flex justify-between w-full"}>
                <Box className={'flex items-center'}>
                    {/*<ProductsSearch value={state.query.search} />*/}
                </Box>
                <FilledButton onClick={handleNavigateToCreate} variant="contained">
                    Добавить
                </FilledButton>
            </Box>

            <Box>
                <ProductPageFiltersComponent
                    searchValue={state.query.search}
                    filters={state.filters}
                    onSearch={handleSearch}
                    onSuppliersChange={handleSupplierFilterChange}
                    onPriceTagsChange={handlePriceTagFilterChange}
                    onTypeChange={handleTypeFilterChange}
                    onOnlyToPrintChange={handleOnlyToPrintFilterChange}
                    onOnlyToOrderChange={handleOnlyToOrderFilterChange}
                    onPriceRangeChange={handlePriceRangeChange}
                    resetTypes={handleResetTypeFilters}
                    resetSuppliers={handleResetSupplierFilters}
                    resetPriceTags={handleResetPriceTagFilters}
                    resetOnlyToOrder={handleResetOnlyToOrderFilter}
                    resetOnlyToPrint={handleResetOnlyToPrintFilter}
                    resetPriceRange={handleResetPriceRange}
                />
            </Box>

            <ProductsGrid
                page={state.page}
                query={state.query}
                productTypes={state.productTypes}
                onSort={handleSort}
                onPageNoChanged={handlePageNoChange}
                onPageSizeChanged={handlePageSizeChange}
                onNavigateToProduct={handleNavigateToProduct}
                onChangePrintStatus={handleChangePrintStatus}
                onChangeOrderStatus={handleChangeOrderStatus}/>

        </Page>
    );
}

export default ProductsPage;