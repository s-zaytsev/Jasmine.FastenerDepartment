import {useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import Loader from "../../../shared/components/Loader.tsx";
import Page from "../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";
import type {ProductTypesState} from "../../models/productTypeModels.ts";
import ProductTypeGrid from "./ProductTypeGrid.tsx";
import ProductTypeDialog from "./ProductTypeDialog.tsx";
import useProductTypesPage from "./useProductTypesPage.ts";

const ProductTypesPage = () => {
    const state = useAppSelector<ProductTypesState>(
        (state) => state.productTypes
    );

    const {
        open,
        handleOpenDialogToCreate,
        handleOpenDialogToChange,
        handleChange,
        handleCreate,
        handleClose,
    } = useProductTypesPage();

    if (state.loading) {
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
                productTypes={state.productTypes}
                onEdit={handleOpenDialogToChange}/>

            <ProductTypeDialog
                productType={state.selectedProductType}
                open={open}
                onClose={handleClose}
                onSubmit={!state.selectedProductType ? handleCreate : handleChange}
            />
        </Page>
    )
}

export default ProductTypesPage;