import {type Template, TemplateFormatCode} from "../../../models/templateModels.ts";
import type {SendDocument} from "../../../models/exportModels.ts";
import {type BaseSyntheticEvent, useEffect, useState} from "react";
import {Box, Dialog, TextField} from "@mui/material";
import Typography from "../../../../shared/components/Typography.tsx";
import FilledButton from "../../../../shared/components/buttons/FilledButton.tsx";

type SendExportDocumentDialogProps = {
    template?: Template;
    formatCode?: TemplateFormatCode;
    open: boolean;
    onClose: () => void;
    onSubmit: (model: SendDocument) => void;
}

const SendExportDocumentDialog = (props: SendExportDocumentDialogProps) => {
    const [contact, setContact] = useState<string>('');

    const onSubmit = () => {
        const model: SendDocument = {
            recipientContact: contact,
            templateId: props.template!.id,
            formatCode: props.formatCode!
        };

        props.onSubmit(model);
    }

    const onChangeContact = (event: BaseSyntheticEvent) => {
        setContact(event.target.value);
    }

    useEffect(() => {
        setContact('');
    }, []);

    return (
        <Dialog
            open={props.open}
            onClose={props.onClose}
        >
            <Box className={'p-[1rem] flex flex-col gap-[1rem]'}>
                <Box className={'text-center mb-[1rem]'}>
                    <Typography variant={'headlineH2'}>
                        Отправка документа
                    </Typography>
                </Box>

                <Typography variant={'bodyRegular'}>
                    Для отправки документа "{props.template!.type.name}" в
                    формате {TemplateFormatCode[props.formatCode!]} введите контакт получателя ниже
                </Typography>

                <Box>
                    <TextField
                        fullWidth
                        placeholder={"Контакт получателя"}
                        autoComplete={'off'}
                        value={contact}
                        onChange={onChangeContact}
                    />
                </Box>

                <Box className={'mt-[1rem] flex justify-end'}>
                    <FilledButton variant="contained" disabled={!contact.length} onClick={onSubmit}>
                        Отправить
                    </FilledButton>
                </Box>
            </Box>
        </Dialog>
    )
}

export default SendExportDocumentDialog;