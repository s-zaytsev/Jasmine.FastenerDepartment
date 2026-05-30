import SettingsCard from "./SettingsCard.tsx";
import {Box, TextField} from "@mui/material";
import {Controller, useForm} from "react-hook-form";
import type {ChangeEmailSettings} from "../../models/settingsModels.ts";
import {useEffect} from "react";
import Typography from "../../../shared/components/Typography.tsx";
import {EmailOutlined} from "@mui/icons-material";

type EmailSettingsCardProps = {
    settings: ChangeEmailSettings;
    onChange: (data: ChangeEmailSettings) => void;
}

const EmailSettingsCard = (props: EmailSettingsCardProps) => {
    const {
        formState: {
            errors
        },
        control,
        reset,
        watch
    } = useForm<ChangeEmailSettings>({
        defaultValues: props.settings,
        mode: "onBlur"
    });

    useEffect(() => {
        reset(props.settings);
    }, [props.settings, reset]);

    useEffect(() => {
        const subscription = watch((data) => {
            props.onChange(data as ChangeEmailSettings);
        });

        return () => subscription.unsubscribe();
    }, [watch, props.onChange]);

    return (
        <SettingsCard title={'Электронная почта'} icon={<EmailOutlined/>}>
            <Box component="form" className={'w-full flex flex-col gap-[0.5rem]'}>
                <Box>
                    <Typography variant={'labelRegularBold'}>Отображаемое имя</Typography>
                    <Controller
                        name="displayName"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!errors.displayName}
                                helperText={errors.displayName?.message}
                            />
                        )}
                    />
                </Box>

                <Box>
                    <Typography variant={'labelRegularBold'}>Адрес электронной почты</Typography>
                    <Controller
                        name="userName"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!errors.userName}
                                helperText={errors.userName?.message}
                            />
                        )}
                    />
                </Box>

                <Box>
                    <Typography variant={'labelRegularBold'}>Пароль</Typography>
                    <Controller
                        name="password"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                type={'password'}
                                autoComplete={'off'}
                                error={!!errors.password}
                                helperText={errors.password?.message}
                            />
                        )}
                    />
                </Box>

                <Box>
                    <Typography variant={'labelRegularBold'}>SMTP-адрес</Typography>
                    <Controller
                        name="smtpUrl"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!errors.smtpUrl}
                                helperText={errors.smtpUrl?.message}
                            />
                        )}
                    />
                </Box>

                <Box>
                    <Typography variant={'labelRegularBold'}>SMTP-порт</Typography>
                    <Controller
                        name="smtpPort"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                type={'number'}
                                error={!!errors.smtpPort}
                                helperText={errors.smtpPort?.message}
                            />
                        )}
                    />
                </Box>
            </Box>
        </SettingsCard>
    )
}

export default EmailSettingsCard;