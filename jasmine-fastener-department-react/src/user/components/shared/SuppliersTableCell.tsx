import type {Supplier} from "../../models/supplierModels.ts";
import {Box, Tooltip} from "@mui/material";
import Typography from "../../../shared/components/Typography.tsx";

type SuppliersTableCellProps = {
    suppliers: Supplier[];
}

const SuppliersTableCell = (props: SuppliersTableCellProps) => {

    if (props.suppliers.length <= 1) {
        return (<Typography variant={'bodyRegular'}>{props.suppliers.map(x => x.name)}</Typography>);
    }

    return (
        <Tooltip
            title={props.suppliers.map(x => x.name).join(', ')}
            placement={'bottom-start'}
        >
            <Box className={'flex gap-[0.2rem]'}>
                <Typography variant={'bodyRegular'}>{props.suppliers[0].name}</Typography>
                <Typography variant={'bodyRegularBold'} color={'primary'}>{`(+${props.suppliers.length - 1})`}</Typography>
            </Box>
        </Tooltip>
    );
}

export default SuppliersTableCell;