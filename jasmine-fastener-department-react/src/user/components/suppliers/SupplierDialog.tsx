import {Box, Dialog, TextField} from "@mui/material";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";
import {type BaseSyntheticEvent, useEffect, useState} from "react";
import type {ChangeSupplierModel, ExtendedSupplier} from "../../models/supplierModels.ts";
import Typography from "../../../shared/components/Typography.tsx";

type SupplierDialogProps = {
    supplier?: ExtendedSupplier;
    open: boolean;
    onClose: () => void;
    onSubmit: (model: ChangeSupplierModel) => void;
}

const SupplierDialog = (props: SupplierDialogProps) => {
    const [name, setName] = useState("");
    const [address, setAddress] = useState("");
    const isNew = !props.supplier?.id;

    function onSubmit() {
        const model: ChangeSupplierModel = {name: name, address: address};
        props.onSubmit(model);
    }

    function changeName(event: BaseSyntheticEvent) {
        setName(event.target.value);
    }

    function changeAddress(event: BaseSyntheticEvent) {
        setAddress(event.target.value);
    }

    useEffect(() => {
        setName(props.supplier?.name || "");
        setAddress(props.supplier?.address || "");
    }, [props.supplier?.address, props.supplier?.name]);

    return (
        <Dialog
            open={props.open}
            onClose={props.onClose}
        >
            <Box className={'p-[1rem]'}>
                <Box className={'text-center mb-[1rem]'}>
                    <Typography variant={'headlineH2'}>
                        {isNew ? 'Создание поставщика' : 'Редактирование поставщика'}
                    </Typography>
                </Box>

                <Box>
                    <TextField
                        fullWidth
                        placeholder={"Название поставщика"}
                        autoComplete={'off'}
                        value={name}
                        onChange={changeName}
                    />

                    <TextField
                        sx={{marginTop: '1rem'}}
                        multiline={true}
                        minRows={3}
                        fullWidth
                        placeholder={"Адрес поставщика"}
                        autoComplete={'off'}
                        value={address}
                        onChange={changeAddress}
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

export default SupplierDialog;

