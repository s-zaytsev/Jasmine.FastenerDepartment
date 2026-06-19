import ProductForm from "../shared/ProductForm.tsx";
import Page from "../../../../shared/components/layout/Page.tsx";
import {Box, Tab, Tabs} from "@mui/material";
import ProductHistory from "../history/ProductHistory.tsx";
import Loader from "../../../../shared/components/Loader.tsx";
import useChangeProductPage from "./useChangeProductPage.ts";

const ChangeProductPage = () => {

    const {
        model,
        historyEntries,
        suppliers,
        productTypes,
        loading,
        tabIndex,
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
                type: "submit",
                formId: 'product-edit-form'
            }}>

            <Tabs
                value={tabIndex}
                onChange={handleChangeTabIndex}
                centered
                TabIndicatorProps={{style: {display: 'none'}}}
            >
                <Tab label="Параметры"/>
                <Tab label={`История (${historyEntries?.length ?? 0})`}/>
            </Tabs>

            {tabIndex === 0 &&
                <ProductForm
                    changeModel={model}
                    suppliers={suppliers}
                    productTypes={productTypes}
                    onSubmit={handleSubmit}
                />
            }

            {tabIndex === 1 &&
                <Box className={'w-full'}>
                    <ProductHistory history={historyEntries} productTypes={productTypes}/>
                </Box>
            }
        </Page>
    )
}

export default ChangeProductPage;