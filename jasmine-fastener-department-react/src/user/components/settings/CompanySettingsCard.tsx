import {Box, TextField} from "@mui/material";
import SettingsCard from "./SettingsCard.tsx";
import {Controller, useFormContext} from "react-hook-form";
import Typography from "../../../shared/components/Typography.tsx";
import {memo} from "react";
import {ListAltOutlined} from "@mui/icons-material";

const CompanySettingsCard = () => {

    const {
        control,
        formState: {
            errors
        }
    } = useFormContext();

    return (
        <SettingsCard title={'Данные компании'} icon={<ListAltOutlined/>}>
            <Box className={'w-full flex flex-col gap-[0.5rem]'}>
                <Box>
                    <Typography variant={'labelRegularBold'}>Название компании</Typography>
                    <Controller
                        name="companySettings.title"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                            />
                        )}
                    />
                </Box>

                <Box>
                    <Typography variant={'labelRegularBold'}>Подзаголовок</Typography>

                    <Controller
                        name="companySettings.subTitle"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!errors.subTitle}
                            />
                        )}
                    />
                </Box>
            </Box>
        </SettingsCard>
    )
}

export default memo(CompanySettingsCard);