import {Box} from "@mui/material";
import Typography from "./Typography.tsx";

type EmptyGridProps = {
    message: string;
}

const EmptyGrid = (props: EmptyGridProps) => {
    return (
        <Box className={'h-full w-full items-center flex justify-center'}>
            <Typography variant={'headlineH3'}>{props.message}</Typography>
        </Box>);
};

export default EmptyGrid;