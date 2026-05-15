import {type BaseSyntheticEvent, useEffect, useState} from "react";
import {Box, Dialog, TextField} from "@mui/material";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";
import type {ChangeProductType, ExtendedProductType} from "../../models/productTypeModels.ts";
import Typography from "../../../shared/components/Typography.tsx";

type ProductTypeDialogProps = {
    productType?: ExtendedProductType;
    open: boolean;
    onClose: () => void;
    onSubmit: (model: ChangeProductType) => void;
}

const ProductTypeDialog = (props: ProductTypeDialogProps) => {
    const [name, setName] = useState("");
    const isNew = !props.productType?.id;

    function onSubmit() {
        const model: ChangeProductType = {name: name};
        props.onSubmit(model);
    }

    function changeName(event: BaseSyntheticEvent) {
        setName(event.target.value);
    }

    useEffect(() => {
        setName(props.productType?.name || "");
    }, [props.productType?.name]);

    return (
        <Dialog
            open={props.open}
            onClose={props.onClose}
        >
            <Box className={'p-[1rem]'}>
                <Box className={'text-center mb-[1rem]'}>
                    <Typography variant={'headlineH2'}>
                        {isNew ? 'Создание типа товара' : 'Редактирование типа товара'}
                    </Typography>
                </Box>

                <Box>
                    <TextField
                        fullWidth
                        placeholder={"Название типа товара"}
                        autoComplete={'off'}
                        value={name}
                        onChange={changeName}
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

export default ProductTypeDialog;