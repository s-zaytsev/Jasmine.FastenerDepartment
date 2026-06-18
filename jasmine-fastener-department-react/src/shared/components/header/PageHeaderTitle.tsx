import {memo} from "react";
import {Box} from "@mui/material";
import Typography from "../Typography.tsx";

type PageHeaderTitleProps = {
    title: string;
    description: string;
}

const PageHeaderTitle = (props: PageHeaderTitleProps) => {
    return(
        <Box className={'flex flex-col'}>
            <Typography variant={'headlineH1'}>{props.title}</Typography>
            <Typography variant={'bodySmall'} color={'tertiary'}>{props.description}</Typography>
        </Box>
    );
}

export default memo(PageHeaderTitle);