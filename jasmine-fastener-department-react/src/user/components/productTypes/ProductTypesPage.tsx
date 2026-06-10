import Loader from "../../../shared/components/Loader.tsx";
import Page from "../../../shared/components/layout/Page.tsx";
import ProductTypeGrid from "./ProductTypeGrid.tsx";
import ProductTypeDialog from "./ProductTypeDialog.tsx";
import useProductTypesPage from "./useProductTypesPage.ts";

const ProductTypesPage = () => {

    const {
        open,
        handleOpenDialogToCreate,
        handleOpenDialogToChange,
        handleChange,
        handleCreate,
        handleClose,
        loading,
        productTypes,
        selectedProductType
    } = useProductTypesPage();

    if (loading) {
        return <Loader text={'Загрузка данных о типах товаров'}/>;
    }

    return (
        <Page
            title={'Типы товаров'}
            description={'Список доступных типов товаров'}
            button={{
                label: 'Создать',
                onClick: handleOpenDialogToCreate
            }}
        >
            <ProductTypeGrid
                productTypes={productTypes}
                onEdit={handleOpenDialogToChange}/>

            <ProductTypeDialog
                productType={selectedProductType}
                open={open}
                onClose={handleClose}
                onSubmit={!selectedProductType ? handleCreate : handleChange}
            />
        </Page>
    )
}

export default ProductTypesPage;