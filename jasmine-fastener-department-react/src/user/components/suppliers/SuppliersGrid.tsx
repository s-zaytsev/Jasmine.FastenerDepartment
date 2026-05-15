import type {ExtendedSupplier} from "../../models/supplierModels.ts";
import SupplierGridCard from "./SupplierGridCard.tsx";
import {Box} from "@mui/material";
import EmptyGrid from "../../../shared/components/EmptyGrid.tsx";

type SuppliersGridProps = {
    suppliers: ExtendedSupplier[];
    onEdit: (id: string) => void;
    onNavigateToSupplierProducts: (parameter: string) => void;
}

const SuppliersGrid = (props: SuppliersGridProps) => {

    if (props.suppliers.length === 0) {
        return <EmptyGrid message={'Список поставщиков пуст'} />
    }

    return (
        <Box className={'flex flex-wrap gap-[1rem]'}>
            {props.suppliers.map((supplier) =>
                <SupplierGridCard
                    key={supplier.id}
                    supplier={supplier}
                    onEdit={props.onEdit}
                    onNavigateToSupplierProducts={props.onNavigateToSupplierProducts}
                />)}
        </Box>
    );
}

export default SuppliersGrid;