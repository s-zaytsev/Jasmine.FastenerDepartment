import Page from "../../../../shared/components/layout/Page.tsx";
import CompleteOrderProductForm from "./CompleteOrderProductForm.tsx";
import Loader from "../../../../shared/components/Loader.tsx";
import useCompleteOrderPage from "./useCompleteOrderPage.ts";

const CompleteOrderPage = () => {
    const {
        model,
        loading,
        productTypes,
        handleSubmit
    } = useCompleteOrderPage();

    if (loading) {
        return <Loader text={'Загружаем данные заказа'}/>
    }

    return (
        <Page>
            <CompleteOrderProductForm
                model={model}
                productTypes={productTypes}
                onSubmit={handleSubmit}
            />
        </Page>
    )
}

export default CompleteOrderPage;