import Page from "../../../../shared/components/layout/Page.tsx";
import OrderStepper from "../shared/stepper/OrderStepper.tsx";
import useCreateOrderPage from "./useCreateOrderPage.ts";


const CreateOrderPage = () => {

    const {
        steps,
        model,
        products,
        suppliers,
        handleSubmit,
        handleMoveToOrder,
        handleDeleteProduct,
        handleUpdateProducts,
        handleChangeSupplier,
        loading
    } = useCreateOrderPage();

    return (
        <Page>
            <OrderStepper
                steps={steps}
                model={model}
                suppliers={suppliers}
                productsToOrder={products}
                onUpdate={handleUpdateProducts}
                onMoveToOrder={handleMoveToOrder}
                onDeleteFromOrder={handleDeleteProduct}
                onSelectSupplier={handleChangeSupplier}
                onSubmit={handleSubmit}
                isLoading={loading}
            />
        </Page>
    );
}

export default CreateOrderPage;