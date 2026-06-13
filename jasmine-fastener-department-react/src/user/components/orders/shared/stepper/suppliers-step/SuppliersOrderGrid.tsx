import type {Supplier} from "../../../../../models/supplierModels.ts";
import {Box} from "@mui/material";
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
            {props.suppliers.map((supplier: Supplier) =>
                <SuppliersOrderGridRow
                    key={supplier.id}
                    supplier={supplier}
                    isSelected={props.selectedSupplierId === supplier.id}
                    onSelect={props.onSelect}/>
            )}

            <SuppliersOrderGridRow
                isSelected={!props.selectedSupplierId}
                onSelect={props.onSelect}/>
        </Box>
    )
}

export default memo(SuppliersOrderGrid);

