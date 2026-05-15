import {Box} from "@mui/material";
import Typography from "../../../../../shared/components/Typography.tsx";
import FilledButton from "../../../../../shared/components/buttons/FilledButton.tsx";
import type {Supplier} from "../../../../models/supplierModels.ts";
import type {StepperItem} from "../../../../../shared/models/models.ts";
import {OrderStepperStep} from "../../../../models/orderModels.ts";

type OrderStepperFooterProps = {
    activeStep: number;
    steps: StepperItem<OrderStepperStep>[];
    supplier?: Supplier;
    onStepBack: () => void;
    onStepNext: () => void;
    onSubmit: () => void;
}

const OrderStepperFooter = (props: OrderStepperFooterProps) => {
    return (
        <Box className={'w-full flex justify-around items-center'}>
            <Box>
                <Typography variant={'bodyRegular'}>Выбранный поставщик</Typography>
                <Typography variant={'bodyRegularBold'} color={'primary'}>
                    {props.supplier?.name ?? 'Без поставщика'}
                </Typography>
            </Box>

            <Box>
                {(props.activeStep !== 0) &&
                    <FilledButton
                        color="inherit"
                        disabled={props.activeStep === 0}
                        onClick={props.onStepBack}
                        sx={{mr: 1}}
                    >
                        Назад
                    </FilledButton>
                }

                {
                    (props.activeStep !== props.steps.length - 1) &&
                    <FilledButton variant={'contained'} onClick={props.onStepNext}>
                        Далее
                    </FilledButton>
                }

                {
                    (props.activeStep === props.steps.length - 1) &&
                    <FilledButton variant={'contained'} onClick={props.onSubmit}>
                        Сохранить
                    </FilledButton>
                }
            </Box>
        </Box>
    );
}

export default OrderStepperFooter;