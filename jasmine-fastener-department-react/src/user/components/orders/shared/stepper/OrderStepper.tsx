import {Box} from "@mui/material";
import {useState} from "react";
import type {Supplier} from "../../../../models/supplierModels.ts";
import type {ProductToOrder} from "../../../../models/productsToOrderModels.ts";
import {type ChangeOrderProduct, type OrderStepperModel, OrderStepperStep} from "../../../../models/orderModels.ts";
import Loader from "../../../../../shared/components/Loader.tsx";
import OrderStepperHeader from "./OrderStepperHeader.tsx";
import OrderStepperBody from "./OrderStepperBody.tsx";
import OrderStepperFooter from "./OrderStepperFooter.tsx";
import type {StepperItem} from "../../../../../shared/models/models.ts";

type OrderStepperProps = {
    steps: StepperItem<OrderStepperStep>[];
    model: OrderStepperModel;
    suppliers: Supplier[];
    productsToOrder: ProductToOrder[];
    onSelectSupplier: (supplier?: Supplier) => void;
    onMoveToOrder: (product: ProductToOrder) => void;
    onDeleteFromOrder: (product: ChangeOrderProduct) => void;
    onUpdate: (products: ChangeOrderProduct[]) => void;
    onSubmit: () => void;
    isLoading: boolean;
}

const OrderStepper = (props: OrderStepperProps) => {
    const [activeStep, setActiveStep] = useState(0);

    const handleNext = () => {
        setActiveStep((prevActiveStep) => prevActiveStep + 1);
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };

    return (
        <Box className={'flex flex-col h-full'}>
            <Box className={'flex-shrink-0'}>
                <OrderStepperHeader activeStep={activeStep} steps={props.steps}/>
            </Box>

            <Box className={'flex-1 h-[85vh] overflow-auto'}>
                {props.isLoading && <Loader/>}
                {!props.isLoading &&
                    <OrderStepperBody
                        model={props.model}
                        steps={props.steps}
                        activeStep={activeStep}
                        suppliers={props.suppliers}
                        productsToOrder={props.productsToOrder}
                        onSelectSupplier={props.onSelectSupplier}
                        onMoveToOrder={props.onMoveToOrder}
                        onDeleteFromOrder={props.onDeleteFromOrder}
                        onUpdate={props.onUpdate}
                        onSubmit={props.onSubmit}
                    />
                }
            </Box>

            <Box className={'flex-shrink-0'}>
                <OrderStepperFooter
                    activeStep={activeStep}
                    steps={props.steps}
                    supplier={props.model.supplier}
                    onStepBack={handleBack}
                    onStepNext={handleNext}
                    onSubmit={props.onSubmit}
                />
            </Box>
        </Box>
    )
}

export default OrderStepper;