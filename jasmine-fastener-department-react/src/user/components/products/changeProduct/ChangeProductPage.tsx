import ProductForm from "../shared/ProductForm.tsx";
import Page from "../../../../shared/components/layout/Page.tsx";
import {Box, Tab, Tabs} from "@mui/material";
import PriceTagPreview from "../shared/PriceTagPreview.tsx";
import ProductHistory from "../history/ProductHistory.tsx";
import Loader from "../../../../shared/components/Loader.tsx";
import useChangeProductPage from "./useChangeProductPage.ts";

const ChangeProductPage = () => {

    const {
        product,
        model,
        suppliers,
        productTypes,
        loading,
        tabIndex,
        handleFormChanged,
        handleChangeTabIndex,
        handleSubmit,
    } = useChangeProductPage();

    if (loading) {
        return <Loader text={`Загрузка информации о товаре`}/>;
    }

    return (
        <Page
            title={'Редактирование товара'}
            description={'Изменение характеристик существующего товара'}
            button={{
                label: 'Сохранить',
                onClick: handleSubmit
            }}>

            <Tabs value={tabIndex} onChange={handleChangeTabIndex} centered>
                <Tab label="Параметры"/>
                <Tab label={`История (${product.historyEntries.length})`}/>
            </Tabs>

            {tabIndex === 0 &&
                <Box className={"flex w-full"}>
                    <ProductForm
                        changeModel={model}
                        suppliers={suppliers}
                        productTypes={productTypes}
                        onChanged={handleFormChanged}/>

                    <PriceTagPreview product={product}/>

                </Box>
            }

            {tabIndex === 1 &&
                <Box className={'w-full'}>
                    <ProductHistory history={product.historyEntries} productTypes={productTypes}/>
                </Box>
            }
        </Page>
    )
}

export default ChangeProductPage;