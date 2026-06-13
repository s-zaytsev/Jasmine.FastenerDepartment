import {Box, TextField} from "@mui/material";
import MinusIcon from "../../../assets/icons/MinusIcon.tsx";
import PlusIcon from "../../../assets/icons/PlusIcon.tsx";
import type {Product} from "../../models/productModel.ts";
import {type BaseSyntheticEvent, memo, useCallback} from "react";
import Typography from "../../../shared/components/Typography.tsx";
import {Close} from "@mui/icons-material";
import TableRow from "../../../shared/components/tables/TableRow.tsx";
import IconButton from "../../../shared/components/buttons/IconButton.tsx";

type PrintFormRowProps = {
    product: Product;
    count: number;
    onIncrement: (id: string) => void;
    onDecrement: (id: string) => void;
    onChangeCount: (id: string, count: number) => void;
    onDelete: (id: string) => void;
}

const PrintFormRow = (props: PrintFormRowProps) => {

    const handleChangeCount = useCallback((event: BaseSyntheticEvent) => {
        props.onChangeCount(props.product.id, event.target.value);
    }, []);

    return (
        <TableRow hasHighlight={true}>
            <Box className={'flex justify-between w-full'}>
                <Box className={"flex items-center w-[75%]"}>
                    <IconButton
                        isActive={true}
                        description={'Удалить из списка печати'}
                        onClick={() => props.onDelete(props.product.id)}
                    >
                        <Close/>
                    </IconButton>
                    <Typography variant={'bodyRegular'}>{props.product.name}</Typography>
                </Box>
                <Box className={"flex justify-center items-center w-[25%]"}>
                    <IconButton
                        isActive={props.count > 1}
                        isDisabled={props.count <= 1}
                        description={'Уменьшить количество'}
                        onClick={() => props.onDecrement(props.product.id)}>
                        <MinusIcon/>
                    </IconButton>

                    <TextField
                        className={'w-[40%]'}
                        autoComplete={'false'}
                        type={'number'}
                        value={props.count}
                        onChange={handleChangeCount}
                    />

                    <IconButton
                        description={'Увеличить количество'}
                        onClick={() => props.onIncrement(props.product.id)}>
                        <PlusIcon/>
                    </IconButton>
                </Box>
            </Box>
        </TableRow>
    );
}

export default memo(PrintFormRow);
