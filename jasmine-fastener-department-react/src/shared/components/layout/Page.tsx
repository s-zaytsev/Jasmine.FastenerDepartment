import {Box} from "@mui/material";
import type {ReactNode} from "react";

export interface PageProps {
    children: ReactNode;
}

const Page = (props: PageProps) => {
    return (
        <Box className={'flex flex-col w-full h-full'}>
            {props.children}
        </Box>
    );
};

export default Page;