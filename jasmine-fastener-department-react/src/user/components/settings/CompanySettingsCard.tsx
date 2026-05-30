import {Box, TextField} from "@mui/material";
import SettingsCard from "./SettingsCard.tsx";
import {Controller, useForm} from "react-hook-form";
import type {ChangeCompanySettings} from "../../models/settingsModels.ts";
import Typography from "../../../shared/components/Typography.tsx";
import {useEffect} from "react";
import {ListAltOutlined} from "@mui/icons-material";

type CompanySettingsCardProps = {
    settings: ChangeCompanySettings;
    onChange: (data: ChangeCompanySettings) => void;
}

const CompanySettingsCard = (props: CompanySettingsCardProps) => {
    const {
        formState: {
            errors
        },
        control,
        reset,
        watch
    } = useForm<ChangeCompanySettings>({
        defaultValues: props.settings,
        mode: "onBlur"
    });

    useEffect(() => {
        reset(props.settings);
    }, [props.settings, reset]);

    useEffect(() => {
        const subscription = watch((data) => {
            props.onChange(data as ChangeCompanySettings);
        });

        return () => subscription.unsubscribe();
    }, [watch, props.onChange]);

    return (
        <SettingsCard title={'Данные компании'} icon={<ListAltOutlined/>}>
            <Box component="form" className={'w-full flex flex-col gap-[0.5rem]'}>
                <Box>
                    <Typography variant={'labelRegularBold'}>Название компании</Typography>

                    <Controller
                        name="title"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!errors.title}
                                helperText={errors.title?.message}
                            />
                        )}
                    />
                </Box>

                <Box>
                    <Typography variant={'labelRegularBold'}>Подзаголовок</Typography>

                    <Controller
                        name="subTitle"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!errors.subTitle}
                                helperText={errors.subTitle?.message}
                            />
                        )}
                    />
                </Box>
            </Box>
        </SettingsCard>
    )
}

export default CompanySettingsCard;