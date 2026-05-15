import {type Control, Controller} from "react-hook-form";
import type {ChangeProduct} from "../../../../models/productModel";
import {Box, FormControl, Switch} from "@mui/material";
import {InfoOutlined} from "@mui/icons-material";
import Typography from "../../../../../shared/components/Typography";
import SettingsCard from "./SettingsCard.tsx";

type OrderSettingsCardProps = {
    control: Control<ChangeProduct, unknown, ChangeProduct>;
}

const OrderSettingsCard = (props: OrderSettingsCardProps) => {
    return (
        <SettingsCard title={'Настройки заказа'} icon={<InfoOutlined/>}>
            <Box className={'w-full'}>
                <Controller
                    name="isNeededToOrder"
                    control={props.control}
                    render={({field}) => (
                        <FormControl fullWidth margin="none">
                            <Box className={'flex w-full justify-between items-center'}>
                                <Typography variant={'bodyRegular'}>Необходимо заказать</Typography>
                                <Switch
                                    {...field}
                                    checked={field.value}/>
                            </Box>
                        </FormControl>
                    )}
                />
            </Box>
        </SettingsCard>
    )
}

export default OrderSettingsCard;