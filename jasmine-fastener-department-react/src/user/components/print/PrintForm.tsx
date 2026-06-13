import {Box} from "@mui/material";
import PrintFormRow from "./PrintFormRow.tsx";
import {PrintOutlined} from "@mui/icons-material";
import type {ProductToPrint} from "../../models/printModels.ts";
import {memo} from "react";

type PrintFormProps = {
    products: ProductToPrint[];
    onIncrement: (id: string) => void;
    onDecrement: (id: string) => void;
    onChangeCount: (id: string, count: number) => void;
    onDelete: (id: string) => void;
}

const PrintForm = (props: PrintFormProps) => {
    return (
        <Box className={"w-full h-full p-4 mt-[1rem]"}>
            {props.products?.map(x =>
                <PrintFormRow
                    key={x.product.id}
                    product={x.product}
                    count={x.count}
                    onDecrement={props.onDecrement}
                    onIncrement={props.onIncrement}
                    onChangeCount={props.onChangeCount}
                    onDelete={props.onDelete}
                />)}
            {!props.products?.length &&
                <Box className={'center-component'}>
                    <p>Очередь печати пуста. Кликните по иконке <PrintOutlined color={"disabled"}/> в списке товаров для
                        добавления товара на печать.</p>
                </Box>
            }
        </Box>
    )
}

export default memo(PrintForm);