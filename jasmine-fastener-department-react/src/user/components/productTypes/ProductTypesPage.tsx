import Loader from "../../../shared/components/Loader.tsx";
import Page from "../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";
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
        <Page>
            <Box className={"flex justify-end w-full mb-[1rem]"}>
                <FilledButton onClick={handleOpenDialogToCreate} variant="contained">
                    Добавить
                </FilledButton>
            </Box>

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