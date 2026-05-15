import {Box} from "@mui/material";
import {primitives} from "../../../assets/variables/primitives.ts";
import Typography from "../Typography.tsx";
import {semanticColors} from "../../../assets/variables/semanticColors.ts";

type SectionHeaderProps = {
    title: string;
    itemsCount?: number;
}

const SectionHeader = (props: SectionHeaderProps) => {
    return (
        <Box className={'flex items-center'}>
            <Box sx={{
                borderRadius: primitives.border.radius,
                width: '0.35rem',
                marginRight: '0.5rem',
                backgroundColor: primitives.colors.primary
            }}>
                &nbsp;
            </Box>

            <Box className={'mx-[0.5rem]'}>
                <Typography variant={'headlineH3'}>{props.title}</Typography>
            </Box>

            {props.itemsCount && <Box sx={{
                borderRadius: primitives.border.radius,
                padding: '0.2rem 0.5rem',
                backgroundColor: semanticColors.surface.light
            }}>
                <Typography variant={'labelRegular'}>{props.itemsCount} позиции</Typography>
            </Box>}

        </Box>
    )
}

export default SectionHeader;