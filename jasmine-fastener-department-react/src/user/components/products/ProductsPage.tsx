import {Box} from "@mui/material";
import Page from "../../../shared/components/layout/Page";
import ProductsGrid from "./ProductsGrid.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import ProductPageFiltersComponent from "./productFilters/ProductPageFiltersComponent.tsx";
import useProductsPage from "./useProductsPage.ts";

const ProductsPage = () => {

    const {
        page,
        query,
        filters,
        productTypes,
        loading,
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

    if (loading) {
        setTimeout(() => {
            return <Loader text={'Загрузка списка товаров'}/>
        }, 300);
    }

    return (
        <Page
            title={'Товары'}
            description={'Список доступных товаров'}
            button={{
                label: 'Создать',
                onClick: handleNavigateToCreate
            }}>

            <Box className={'mb-[1rem]'}>
                <ProductPageFiltersComponent
                    searchValue={query.search}
                    filters={filters}
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
                page={page}
                query={query}
                productTypes={productTypes}
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