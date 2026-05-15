import type {Supplier} from "../../../../../models/supplierModels.ts";
import {Box} from "@mui/material";
import SuppliersOrderGridRow from "./SuppliersOrderGridRow.tsx";

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
                    selectedSupplierId={props.selectedSupplierId}
                    supplier={supplier}
                    onSelect={props.onSelect}/>
            )}

            <SuppliersOrderGridRow
                selectedSupplierId={props.selectedSupplierId}
                onSelect={props.onSelect}/>
        </Box>
    )
}

export default SuppliersOrderGrid;

