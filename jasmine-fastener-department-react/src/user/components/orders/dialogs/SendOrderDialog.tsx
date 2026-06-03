import type {Order, SendOrder} from "../../../models/orderModels.ts";
import {type ChangeEvent, useEffect, useState} from "react";
import {Box, Dialog, TextField} from "@mui/material";
import Typography from "../../../../shared/components/Typography.tsx";
import FilledButton from "../../../../shared/components/buttons/FilledButton.tsx";
import {MessageType} from "../../../../shared/models/models.ts";
import {emailRegex} from "../../../../shared/models/regularExpressions.ts";
import FileUploader from "../../../../shared/components/files/FileUploader.tsx";

type SendOrderDialogProps = {
    order?: Order;
    open: boolean;
    onClose: () => void;
    onSubmit: (model: SendOrder) => void;
}

const SendOrderDialog = (props: SendOrderDialogProps) => {
    const [email, setEmail] = useState('');
    const [emailError, setEmailError] = useState('');
    const [files, setFiles] = useState<File[]>([]);

    const validateEmail = (value: string): boolean => {
        if (!value) {
            setEmailError('Электронная почта обязательна');
            return false;
        }

        if (!emailRegex.test(value)) {
            setEmailError('Введите корректную электронную почту');
            return false;
        }

        setEmailError('');
        return true;
    };

    const handleBlur = () => {
        validateEmail(email);
    };

    const handleChangeEmail = (e: ChangeEvent<HTMLInputElement>) => {
        const value = e.target.value;
        setEmail(value);

        if (emailError) {
            validateEmail(value);
        }
    }

    const handleChangeFiles = (files: File[]) => {
        setFiles(files);
    }

    const handleSubmit = () => {
        const model: SendOrder = {
            recipientContact: email,
            messageType: MessageType.email,
            attachments: files
        }

        props.onSubmit(model);
    }

    useEffect(() => {
        setEmail("");
        setEmailError('');
    }, []);

    return (
        <Dialog
            open={props.open}
            onClose={props.onClose}
        >
            <Box className={'p-[1rem]'}>
                <Box className={'text-center mb-[1rem]'}>
                    <Typography variant={'headlineH2'}>
                        Отправка заказа
                    </Typography>
                </Box>

                <Box>
                    <Typography variant={'bodyRegular'}>
                        Для отправки заказа <strong>#{props.order?.number ?? ''}</strong> введите электронную почту
                        получателя и затем нажмите кнопку "Отправить"
                    </Typography>
                    <Typography variant={'bodyRegular'}>
                        Также ниже вы можете прикрепить дополнительные файлы.
                    </Typography>
                </Box>

                <Box className={'my-[1rem]'}>
                    <TextField
                        fullWidth
                        placeholder={"Электронная почта получателя"}
                        autoComplete={'off'}
                        value={email}
                        onChange={handleChangeEmail}
                        onBlur={handleBlur}
                        error={Boolean(emailError)}
                        helperText={emailError}
                    />
                </Box>

                <FileUploader onChange={handleChangeFiles}/>

                <Box className={'mt-[1rem] flex justify-end'}>
                    <FilledButton variant="contained" onClick={handleSubmit} disabled={!!emailError || !email}>
                        Отправить
                    </FilledButton>
                </Box>
            </Box>
        </Dialog>
    );
}

export default SendOrderDialog;