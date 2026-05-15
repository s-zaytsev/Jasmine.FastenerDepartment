import type {Quantity} from "../../models/models.ts";
import {Box} from "@mui/material";
import Typography from "../Typography.tsx";
import Chip from "../Chip.tsx";
import useQuantity from "../../hooks/useQuantity.ts";

type QuantityChipProps = {
    title?: string;
    quantity?: Quantity;
    type: QuantityChipType;
}

export type QuantityChipType = 'active' | 'inactive';

const QuantityChip = (props: QuantityChipProps) => {
    const {getText} = useQuantity();
    const isActive = props.type === "active" && props.quantity?.value;

    return (
        <Box className={'flex justify-center items-center'}>
            <Box>
                <Typography
                    variant={'labelRegular'}
                    color={isActive ? 'primary' : undefined}
                    sx={{marginBottom: '0.5rem'}}
                >
                    {props.title}
                </Typography>

                <Chip title={getText(props.quantity)} color={isActive ? 'active' : 'inactive'}/>
            </Box>
        </Box>
    )
}

export default QuantityChip;

