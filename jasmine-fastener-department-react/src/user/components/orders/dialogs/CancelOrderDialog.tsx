import {type BaseSyntheticEvent, useEffect, useState} from "react";
import {Box, Dialog, TextField} from "@mui/material";
import Typography from "../../../../shared/components/Typography.tsx";
import FilledButton from "../../../../shared/components/buttons/FilledButton.tsx";
import type {CancelOrder, Order} from "../../../models/orderModels.ts";

type CancelOrderDialogProps = {
    order: Order;
    open: boolean;
    onClose: () => void;
    onSubmit: (model: CancelOrder) => void;
}

const CancelOrderDialog = (props: CancelOrderDialogProps) => {
    const [comment, setComment] = useState("");

    function onSubmit() {
        const model: CancelOrder = {comment: comment};
        props.onSubmit(model);
    }

    function changeComment(event: BaseSyntheticEvent) {
        setComment(event.target.value);
    }

    useEffect(() => {
        setComment("");
    }, []);

    return (
        <Dialog
            open={props.open}
            onClose={props.onClose}
        >
            <Box className={'p-[1rem]'}>
                <Box className={'text-center mb-[1rem]'}>
                    <Typography variant={'headlineH2'}>
                        Отмена заказа
                    </Typography>
                </Box>

                <Box>
                    <TextField
                        fullWidth
                        multiline={true}
                        minRows={3}
                        placeholder={"Комментарий для отмены заказа (необязательное поле)"}
                        autoComplete={'off'}
                        value={comment}
                        onChange={changeComment}
                    />
                </Box>

                <Box className={'mt-[1rem] flex justify-end'}>
                    <FilledButton variant="contained" onClick={onSubmit}>
                        Отправить
                    </FilledButton>
                </Box>
            </Box>
        </Dialog>
    )
}

export default CancelOrderDialog;