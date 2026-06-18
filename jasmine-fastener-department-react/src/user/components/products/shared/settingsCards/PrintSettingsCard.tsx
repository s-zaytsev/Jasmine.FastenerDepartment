import {type Control, Controller, useWatch} from "react-hook-form";
import {type ChangeProduct} from "../../../../models/productModel";
import {Box, FormControl, Switch} from "@mui/material";
import {PrintOutlined} from "@mui/icons-material";
import Typography from "../../../../../shared/components/Typography";
import ApplicationRegexes from "../../../../../shared/expressions/applicationRegexes";
import SettingsCard from "./SettingsCard.tsx";
import PriceTagGroup from "../priceTagGroup/PriceTagGroup.tsx";
import {memo, useMemo} from "react";

type PrintSettingsCardProps = {
    control: Control<ChangeProduct, unknown, ChangeProduct>;
}

const PrintSettingsCard = (props: PrintSettingsCardProps) => {
    const productName = useWatch({
        control: props.control,
        name: "name"
    });

    const hasHardwareSize = useMemo(() => {
        return ApplicationRegexes.hasHardwareSize(productName || "");
    }, [productName]);

    return (
        <SettingsCard title={'Настройки печати'} icon={<PrintOutlined/>}>

            <Box className={'flex justify-between gap-[1rem]'}>
                <Controller
                    name="priceTagCode"
                    control={props.control}
                    render={({field}) => (
                        <PriceTagGroup
                            checkedCode={field.value}
                            onChange={field.onChange}
                        />
                    )}
                />

                <Box className={'flex flex-col items-center gap-[0.5rem]'}>
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

export default memo(PrintSettingsCard);