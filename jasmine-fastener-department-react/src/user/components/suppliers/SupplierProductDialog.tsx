import type {ChangeSupplierProduct, SupplierProduct} from "../../models/supplierModels.ts";
import {type BaseSyntheticEvent, useEffect, useState} from "react";
import {Box, Dialog, TextField} from "@mui/material";
import Typography from "../../../shared/components/Typography.tsx";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";

type SupplierProductDialogProps = {
    product?: SupplierProduct;
    open: boolean;
    onClose: () => void;
    onSubmit: (model: ChangeSupplierProduct) => void;
}

const SupplierProductDialog = (props: SupplierProductDialogProps) => {
    const [number, setNumber] = useState("");

    function onSubmit() {
        const model: ChangeSupplierProduct = {number: number};
        props.onSubmit(model);
    }

    function changeNumber(event: BaseSyntheticEvent) {
        setNumber(event.target.value);
    }

    useEffect(() => {
        setNumber(props.product?.number || "");
    }, [props.product]);

    return (
        <Dialog
            open={props.open}
            onClose={props.onClose}
        >
            <Box className={'p-[1rem]'}>
                <Box className={'text-center mb-[1rem]'}>
                    <Typography variant={'headlineH2'}>
                        Редактирование товара поставщика
                    </Typography>
                </Box>

                <Box>
                    <TextField
                        fullWidth
                        placeholder={"Номер товара"}
                        autoComplete={'off'}
                        value={number}
                        onChange={changeNumber}
                    />
                </Box>

                <Box className={'mt-[1rem] flex justify-end'}>
                    <FilledButton variant="contained" onClick={onSubmit}>
                        Сохранить
                    </FilledButton>
                </Box>
            </Box>
        </Dialog>
    )
}

export default SupplierProductDialog;