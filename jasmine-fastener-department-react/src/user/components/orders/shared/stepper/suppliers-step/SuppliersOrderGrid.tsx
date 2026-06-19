import type {Supplier} from "../../../../../models/supplierModels.ts";
import {Box, Grow} from "@mui/material";
import SuppliersOrderGridRow from "./SuppliersOrderGridRow.tsx";
import {memo} from "react";

type SuppliersOrderGridProps = {
    selectedSupplierId?: string;
    suppliers: Supplier[];
    onSelect: (supplier?: Supplier) => void;
}

const SuppliersOrderGrid = (props: SuppliersOrderGridProps) => {
    return (
        <Box className={'w-full h-full'}>
            {props.suppliers.map((supplier: Supplier, index) =>
                <Grow key={supplier.id} in={true} timeout={index * 150}>
                    <Box>
                        <SuppliersOrderGridRow
                            supplier={supplier}
                            isSelected={props.selectedSupplierId === supplier.id}
                            onSelect={props.onSelect}/>
                    </Box>
                </Grow>
            )}

            <Grow in={true} timeout={props.suppliers.length + 100}>
                <Box>
                    <SuppliersOrderGridRow
                        isSelected={!props.selectedSupplierId}
                        onSelect={props.onSelect}/>
                </Box>
            </Grow>
        </Box>
    )
}

export default memo(SuppliersOrderGrid);

