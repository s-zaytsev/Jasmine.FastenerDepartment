import type {Order} from "../../../models/orderModels.ts";
import {Box, LinearProgress} from "@mui/material";
import Typography from "../../../../shared/components/Typography.tsx";
import FilledButton from "../../../../shared/components/buttons/FilledButton.tsx";
import Card from "../../../../shared/components/Card.tsx";

type OrderDetailsHeaderProps = {
    order?: Order;
    onDownload: () => void;
}

const OrderDetailsHeader = (props: OrderDetailsHeaderProps) => {
    const orderNumber = "0".repeat(8 - (props.order?.number.toString().length || 0)) + props.order?.number.toString();
    const notFulfilledCount = props.order?.products?.filter(x => !x.isFulfilled)?.length ?? 1;
    const percent = notFulfilledCount === 0 ? 100 : notFulfilledCount / (props.order?.products?.length || 1) * 100;

    return (
        <Box className={'flex justify-center w-full'}>
            <Box className={'flex justify-between w-full items-center gap-[1rem]'}>
                <Card>
                    <Box>
                        <Typography variant="labelRegular">Номер заказа</Typography>
                        <Typography variant={'headlineH3'}>#{orderNumber}</Typography>
                    </Box>
                </Card>

                <Card>
                    <Box>
                        <Typography variant="labelRegular">Поставщик</Typography>
                        <Typography variant={'headlineH3'}>{props.order?.supplier?.name}</Typography>
                    </Box>
                </Card>

                <Card>
                    <Box>
                        <Typography variant="labelRegular">Процент доставки</Typography>
                        <Box className={'flex'}>
                            <Typography variant={'headlineH3'} color={'primary'}>{percent}%</Typography>
                            <LinearProgress variant="determinate" value={percent}/>
                        </Box>
                    </Box>
                </Card>

                <Box>
                    <FilledButton variant={'contained'} onClick={props.onDownload}>Скачать</FilledButton>
                </Box>
            </Box>

        </Box>
    );
}

export default OrderDetailsHeader;