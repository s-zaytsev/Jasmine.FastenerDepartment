import Page from "../../../../shared/components/layout/Page.tsx";
import OrderStepper from "../shared/stepper/OrderStepper.tsx";
import useChangeOrderPage from "./useChangeOrderPage.ts";


const ChangeOrderPage = () => {

    const {
        steps,
        model,
        products,
        handleSubmit,
        handleMoveToOrder,
        handleDeleteProduct,
        handleUpdateProducts,
        loading
    } = useChangeOrderPage()

    return (
        <Page title={''} description={''}>
            <OrderStepper
                steps={steps}
                model={model}
                suppliers={[]}
                productsToOrder={products}
                onUpdate={handleUpdateProducts}
                onMoveToOrder={handleMoveToOrder}
                onDeleteFromOrder={handleDeleteProduct}
                onSelectSupplier={() => {
                }}
                onSubmit={handleSubmit}
                isLoading={loading}
            />
        </Page>
    );
}

export default ChangeOrderPage;