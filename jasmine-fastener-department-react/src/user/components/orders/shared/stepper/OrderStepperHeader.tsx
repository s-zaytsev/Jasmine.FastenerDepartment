import {Step, StepLabel, Stepper} from "@mui/material";
import {memo, type ReactNode} from "react";
import type {StepperItem} from "../../../../../shared/models/models.ts";
import {OrderStepperStep} from "../../../../models/orderModels.ts";

type OrderStepperHeaderProps = {
    activeStep: number;
    steps: StepperItem<OrderStepperStep>[];
}

const OrderStepperHeader = (props: OrderStepperHeaderProps) => {
    return(
        <Stepper className={'mb-[1rem]'} activeStep={props.activeStep}>
            {props.steps.map((step) => {
                const stepProps: { completed?: boolean } = {};
                const labelProps: {
                    optional?: ReactNode;
                } = {};
                return (
                    <Step key={step.id} {...stepProps}>
                        <StepLabel {...labelProps}>{step.label}</StepLabel>
                    </Step>
                );
            })}
        </Stepper>
    )
}

export default memo(OrderStepperHeader);