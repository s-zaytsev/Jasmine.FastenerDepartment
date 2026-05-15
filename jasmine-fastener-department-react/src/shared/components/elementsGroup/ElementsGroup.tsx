import type {ReactNode} from "react";
import {Box} from "@mui/material";

type ElementsGroupProps = {
    children: ReactNode;
}

const ElementsGroup = (props: ElementsGroupProps) => {
    return (
        <Box className={'flex gap-[0.5rem] flex-grow items-center'}>
            <>{props.children}</>
        </Box>
    )
}

export default ElementsGroup;
