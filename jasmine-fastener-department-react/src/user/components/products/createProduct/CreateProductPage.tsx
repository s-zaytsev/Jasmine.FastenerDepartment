import {Box} from "@mui/material";
import Page from "../../../../shared/components/layout/Page.tsx";
import ProductForm from "../shared/ProductForm.tsx";
import Loader from "../../../../shared/components/Loader.tsx";
import useCreateProductPage from "./useCreateProductPage.ts";

const CreateProductPage = () => {
    const {
        model,
        suppliers,
        productTypes,
        loading,
        handleSubmit
    } = useCreateProductPage();

    if (loading) {
        return <Loader text={'Загрузка данных для создания товара'}/>;
    }

    return (
        <Page
            title={'Создание товара'}
            description={'Настройка характеристик нового товара'}
            button={{
                label: 'Сохранить',
                type: "submit",
                formId: 'product-edit-form'
            }}>
            <Box className={"flex"}>
                <ProductForm
                    changeModel={model!}
                    suppliers={suppliers}
                    productTypes={productTypes}
                    onSubmit={handleSubmit}/>
            </Box>
        </Page>
    )
}

export default CreateProductPage;