import {Box} from "@mui/material";
import {ArrowDropDown, ArrowDropUp} from "@mui/icons-material";
import Typography from "../Typography.tsx";

type SortTableHeadLabelProps = {
    isActive: boolean;
    direction: "asc" | "desc" | undefined;
    onClick: () => void;
    title?: string;
}

const SortTableHeadLabel = (props: SortTableHeadLabelProps) => {
    return (
        <Box onClick={props.onClick} className={'flex cursor-pointer'}>
            <Typography variant={'bodyRegularBold'}>{props.title}</Typography>
            {props.isActive && props.direction === 'desc' && <ArrowDropDown/>}
            {props.isActive && props.direction === 'asc' && <ArrowDropUp/>}
        </Box>
    )
}

export default SortTableHeadLabel;