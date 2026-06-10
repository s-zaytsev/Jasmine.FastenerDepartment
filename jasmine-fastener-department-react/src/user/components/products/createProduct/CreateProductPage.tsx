import type {Product} from "../../../models/productModel.ts";
import {Box} from "@mui/material";
import Page from "../../../../shared/components/layout/Page.tsx";
import ProductForm from "../shared/ProductForm.tsx";
import PriceTagPreview from "../shared/PriceTagPreview.tsx";
import Loader from "../../../../shared/components/Loader.tsx";
import useCreateProductPage from "./useCreateProductPage.ts";

const CreateProductPage = () => {
    const {
        model,
        suppliers,
        productTypes,
        loading,
        handleCreate,
        handleFormChanged
    } = useCreateProductPage();

    if (loading) {
        return <Loader text={'Загрузка данных для создания товара'}/>;
    }

    const product: Product = {
        number: model.number,
        price: model.price,
        isHardwareSizeEnabled: model.isHardwareSizeEnabled,
        measurementUnitCode: model.measurementUnitCode,
        name: model.name,
        priceTagCode: model.priceTagCode,
        id: '',
        isNeededToOrder: model.isNeededToOrder,
        isNeededToPrint: model.isNeededToPrint,
        historyEntries: [],
        suppliers: []
    }

    return (
        <Page
            title={'Создание товара'}
            description={'Настройка характеристик нового товара'}
            button={{
                label: 'Сохранить',
                onClick: handleCreate
            }}>
            <Box className={"flex"}>
                <ProductForm
                    changeModel={model!}
                    suppliers={suppliers}
                    productTypes={productTypes}
                    onChanged={handleFormChanged}/>

                <PriceTagPreview product={product}/>
            </Box>
        </Page>
    )
}

export default CreateProductPage;