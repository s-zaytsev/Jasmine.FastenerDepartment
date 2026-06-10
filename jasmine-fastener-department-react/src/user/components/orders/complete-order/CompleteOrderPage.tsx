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
        <Page
            title={'Завершение заказа'}
            description={'Проверка деталей заказа и его завершение'}
            button={{
                label: 'Завершить',
                onClick: handleSubmit
        }}>
            <CompleteOrderProductForm
                model={model}
                productTypes={productTypes}
                onSubmit={handleSubmit}
            />
        </Page>
    )
}

export default CompleteOrderPage;