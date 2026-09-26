import type {ChangeRecipient, Recipient} from "../../models/recipientModels.ts";
import {type BaseSyntheticEvent, useEffect, useState} from "react";
import {Box, Dialog, TextField} from "@mui/material";
import Typography from "../../../shared/components/Typography.tsx";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";

type RecipientDialogProps = {
    recipient?: Recipient;
    open: boolean;
    onClose: () => void;
    onSubmit: (model: ChangeRecipient) => void;
}

const RecipientDialog = (props: RecipientDialogProps) => {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");

    const isNew = !props.recipient?.id;

    const onSubmit = () => {
        const model: ChangeRecipient = {
            name: name,
            email: email
        };
        props.onSubmit(model);
    }

    const changeName = (event: BaseSyntheticEvent) => {
        setName(event.target.value);
    }

    const changeEmail = (event: BaseSyntheticEvent) => {
        setEmail(event.target.value);
    }

    useEffect(() => {
        setName(props.recipient?.name || "");
        setEmail(props.recipient?.email || "");
    }, [props.recipient?.name, props.recipient?.email]);

    return (
        <Dialog
            open={props.open}
            onClose={props.onClose}
        >
            <Box className={'p-[1rem]'}>
                <Box className={'text-center mb-[1rem]'}>
                    <Typography variant={'headlineH2'}>
                        {isNew ? 'Создание контакта' : 'Редактирование контакта'}
                    </Typography>
                </Box>

                <Box className={'flex flex-col gap-[1rem]'}>
                    <TextField
                        fullWidth
                        placeholder={"Наименование контакта"}
                        autoComplete={'off'}
                        value={name}
                        onChange={changeName}
                    />

                    <TextField
                        fullWidth
                        placeholder={"Электронная почта"}
                        autoComplete={'off'}
                        value={email}
                        onChange={changeEmail}
                    />
                </Box>

                <Box className={'mt-[1rem] flex justify-end'}>
                    <FilledButton variant="contained" onClick={onSubmit} disabled={!name}>
                        {isNew ? 'Создать' : 'Обновить'}
                    </FilledButton>
                </Box>
            </Box>
        </Dialog>
    )
}

export default RecipientDialog;