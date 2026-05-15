import {type Control, Controller, useWatch} from "react-hook-form";
import {type ChangeProduct} from "../../../../models/productModel";
import {Box, FormControl, Switch} from "@mui/material";
import {PrintOutlined} from "@mui/icons-material";
import Typography from "../../../../../shared/components/Typography";
import ApplicationRegexes from "../../../../../shared/expressions/applicationRegexes";
import SettingsCard from "./SettingsCard.tsx";
import PriceTagGroup from "../priceTagGroup/PriceTagGroup.tsx";

type PrintSettingsCardProps = {
    control: Control<ChangeProduct, unknown, ChangeProduct>;
}

const PrintSettingsCard = (props: PrintSettingsCardProps) => {
    const productName = useWatch({
        control: props.control,
        name: "name"
    });

    const hasHardwareSize = ApplicationRegexes.hasHardwareSize(productName || "");

    return (
        <SettingsCard title={'Настройки печати'} icon={<PrintOutlined/>}>

            <Box className={'flex justify-between gap-[1rem]'}>
                <Controller
                    name="priceTagCode"
                    control={props.control}
                    render={({field}) => (
                        <PriceTagGroup
                            checkedCode={field.value}
                            onChange={(value) => {
                                field.onChange(value);
                                field.onBlur();
                            }}
                        />
                    )}
                />

                <Box className={'flex flex-col items-center'}>
                    <Controller
                        name="isHardwareSizeEnabled"
                        control={props.control}
                        render={({field}) => (
                            <FormControl fullWidth margin="none">
                                <Box className={'flex w-full justify-between items-center'}>
                                    <Typography variant={'bodyRegular'}>Выделять размер</Typography>
                                    <Switch
                                        disabled={!hasHardwareSize}
                                        {...field}
                                        checked={Boolean(field.value) && hasHardwareSize}
                                    />
                                </Box>
                            </FormControl>
                        )}
                    />

                    <Controller
                        name="isNeededToPrint"
                        control={props.control}
                        render={({field}) => (
                            <FormControl fullWidth margin="none">
                                <Box className={'flex w-full justify-between items-center'}>
                                    <Typography variant={'bodyRegular'}>Необходимо распечатать</Typography>
                                    <Switch
                                        {...field}
                                        checked={field.value}/>
                                </Box>
                            </FormControl>
                        )}
                    />
                </Box>
            </Box>
        </SettingsCard>
    )
}

export default PrintSettingsCard;