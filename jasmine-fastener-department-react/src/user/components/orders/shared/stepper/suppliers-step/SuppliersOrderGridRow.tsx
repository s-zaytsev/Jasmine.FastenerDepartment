import type {Supplier} from "../../../../../models/supplierModels.ts";
import Card from "../../../../../../shared/components/Card.tsx";
import {Box} from "@mui/material";
import Typography from "../../../../../../shared/components/Typography.tsx";
import {ApartmentOutlined} from "@mui/icons-material";
import {primitives} from "../../../../../../assets/variables/primitives.ts";
import {semanticColors} from "../../../../../../assets/variables/semanticColors.ts";

type SuppliersOrderGridRowProps = {
    selectedSupplierId?: string;
    supplier?: Supplier;
    onSelect: (supplier?: Supplier) => void;
}

const SuppliersOrderGridRow = (props: SuppliersOrderGridRowProps) => {
    const isSelected = props.selectedSupplierId === props.supplier?.id;

    return (
        <Card
            backgroundColor={isSelected ? primitives.colors.tonal : undefined}
            hasHighlight={true}
            onClick={() => props.onSelect(props.supplier)}
        >
            <Box className={'w-full flex items-center justify-between'}>
                <Box className={'flex items-center'}>
                    <Box sx={{
                        color: primitives.colors.primary,
                        backgroundColor: semanticColors.surface.light,
                        borderRadius: primitives.border.radius,
                        padding: '0.5rem 1rem',
                        marginRight: '0.5rem'
                    }}>
                        <ApartmentOutlined/>
                    </Box>

                    <Box>
                        <Typography variant={'headlineH3'}>{props.supplier?.name ?? 'Без поставщика'}</Typography>
                        <Typography variant={'bodySmall'}>{props.supplier?.address}</Typography>
                    </Box>
                </Box>
                <Box className={'flex flex-col items-center'}>
                    <Box sx={{
                        borderRadius: '50%',
                        border: `2px solid ${isSelected ? primitives.colors.primary : semanticColors.surface.medium}`,
                        backgroundColor: `${isSelected ? primitives.colors.primary : undefined}`,
                        width: '1rem',
                        height: '1rem'
                    }}/>
                </Box>

            </Box>
        </Card>
    )
}

export default SuppliersOrderGridRow;