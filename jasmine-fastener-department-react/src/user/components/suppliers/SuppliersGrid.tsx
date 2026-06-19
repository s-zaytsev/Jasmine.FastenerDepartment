import type {ExtendedSupplier} from "../../models/supplierModels.ts";
import SupplierGridCard from "./SupplierGridCard.tsx";
import {Box, Grow} from "@mui/material";
import EmptyGrid from "../../../shared/components/EmptyGrid.tsx";

type SuppliersGridProps = {
    suppliers: ExtendedSupplier[];
    onEdit: (id: string) => void;
    onNavigateToSupplierProducts: (parameter: string) => void;
}

const SuppliersGrid = (props: SuppliersGridProps) => {

    if (props.suppliers.length === 0) {
        return <EmptyGrid message={'Список поставщиков пуст'}/>
    }

    return (
        <Box className={'flex flex-wrap gap-[1rem]'}>
            {props.suppliers.map((supplier, index) =>
                <Grow key={supplier.id} in={true} timeout={index * 150}>
                    <Box className={'w-[23%]'}>
                        <SupplierGridCard
                            supplier={supplier}
                            onEdit={props.onEdit}
                            onNavigateToSupplierProducts={props.onNavigateToSupplierProducts}
                        />
                    </Box>
                </Grow>
            )}
        </Box>
    );
}

export default SuppliersGrid;