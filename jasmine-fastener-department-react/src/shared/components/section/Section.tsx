import type {ReactNode} from "react";
import {Box} from "@mui/material";
import SectionHeader from "./SectionHeader.tsx";

type SectionProps = {
    title: string;
    itemsCount?: number;
    children: ReactNode;
}

const Section = (props: SectionProps) => {
    return (
        <Box className={'w-full'}>
            <SectionHeader title={props.title} itemsCount={props.itemsCount}/>
            <Box className={'w-full mt-[0.5rem]'}>
                {props.children}
            </Box>
        </Box>
    )
}

export default Section;