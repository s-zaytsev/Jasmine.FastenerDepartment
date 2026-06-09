import {Box} from "@mui/material";
import SuppliersOrderGrid from "./suppliers-step/SuppliersOrderGrid.tsx";
import OrderProductsAmountGrid from "./amount-step/OrderProductsAmountGrid.tsx";
import type {Supplier} from "../../../../models/supplierModels.ts";
import type {ProductToOrder} from "../../../../models/productsToOrderModels.ts";
import {type ChangeOrderProduct, type OrderStepperModel, OrderStepperStep} from "../../../../models/orderModels.ts";
import ConfirmOrder from "./confirm-step/ConfirmOrder.tsx";
import type {StepperItem} from "../../../../../shared/models/models.ts";
import ProductsOrderGrid from "./products-step/ProductsOrderGrid.tsx";

type OrderStepperBodyProps = {
    model: OrderStepperModel;
    activeStep: number;
    steps: StepperItem<OrderStepperStep>[];
    suppliers: Supplier[];
    productsToOrder: ProductToOrder[];
    onSelectSupplier: (supplier?: Supplier) => void;
    onMoveToOrder: (product: ProductToOrder) => void;
    onDeleteFromOrder: (id?: string) => void;
    onUpdate: (products: ChangeOrderProduct[]) => void;
    onSubmit: () => void;
}

const OrderStepperBody = (props: OrderStepperBodyProps) => {
    const currentStep = props.steps[props.activeStep].id;

    return (
        <Box className={'flex justify-center'}>

            {currentStep === OrderStepperStep.suppliers &&
                <Box className={'w-[60%] h-full'}>
                    <SuppliersOrderGrid
                        selectedSupplierId={props.model.supplier?.id}
                        suppliers={props.suppliers}
                        onSelect={props.onSelectSupplier}
                    />
                </Box>
            }

            {currentStep === OrderStepperStep.products &&
                <ProductsOrderGrid
                    model={props.model}
                    productsToOrder={props.productsToOrder}
                    onMoveToOrder={props.onMoveToOrder}
                    onDeleteFromOrder={props.onDeleteFromOrder}
                />
            }

            {currentStep === OrderStepperStep.amount &&
                <OrderProductsAmountGrid
                    changeModel={props.model}
                    onUpdate={props.onUpdate}
                    onRemove={props.onDeleteFromOrder}
                />
            }

            {currentStep === OrderStepperStep.confirm &&
                <Box className={'w-[60%]'}>
                    <ConfirmOrder model={props.model}/>
                </Box>
            }
        </Box>
    )
}

export default OrderStepperBody;