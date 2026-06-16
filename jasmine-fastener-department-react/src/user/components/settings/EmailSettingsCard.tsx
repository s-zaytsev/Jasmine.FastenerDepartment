import SettingsCard from "./SettingsCard.tsx";
import {Box, TextField} from "@mui/material";
import {Controller, useFormContext} from "react-hook-form";
import Typography from "../../../shared/components/Typography.tsx";
import {EmailOutlined} from "@mui/icons-material";
import {memo} from "react";

const EmailSettingsCard = () => {

    const {
        control,
        formState: {
            errors
        }
    } = useFormContext();

    return (
        <SettingsCard title={'Электронная почта'} icon={<EmailOutlined/>}>
            <Box className={'w-full flex flex-col gap-[0.5rem]'}>
                <Box>
                    <Typography variant={'labelRegularBold'}>Отображаемое имя</Typography>
                    <Controller
                        name="emailSettings.displayName"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!errors.displayName}
                                helperText={errors.root?.message}
                            />
                        )}
                    />
                </Box>

                <Box>
                    <Typography variant={'labelRegularBold'}>Адрес электронной почты</Typography>
                    <Controller
                        name="emailSettings.userName"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!errors.userName}
                            />
                        )}
                    />
                </Box>

                <Box>
                    <Typography variant={'labelRegularBold'}>Пароль</Typography>
                    <Controller
                        name="emailSettings.password"
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
                            />
                        )}
                    />
                </Box>

                <Box>
                    <Typography variant={'labelRegularBold'}>SMTP-адрес</Typography>
                    <Controller
                        name="emailSettings.smtpUrl"
                        control={control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!errors.smtpUrl}
                            />
                        )}
                    />
                </Box>

                <Box>
                    <Typography variant={'labelRegularBold'}>SMTP-порт</Typography>
                    <Controller
                        name="emailSettings.smtpPort"
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
                            />
                        )}
                    />
                </Box>
            </Box>
        </SettingsCard>
    )
}

export default memo(EmailSettingsCard);