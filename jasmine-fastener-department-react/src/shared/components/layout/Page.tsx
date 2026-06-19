import {Box, Fade} from "@mui/material";
import type {ReactNode} from "react";
import PageHeader from "../header/PageHeader.tsx";

export interface PageProps {
    title: string;
    description: string;
    button?: {
        label: string;
        type?: "submit" | "reset" | "button";
        formId?: string;
        onClick?: (args?: any) => void;
    };
    children: ReactNode;
}

const Page = (props: PageProps) => {
    return (
        <Box className={'flex flex-col w-full h-full'}>
            <PageHeader
                title={props.title}
                description={props.description}
                button={props.button}
            />
            <Fade in={true} timeout={300}>
                <Box>{props.children}</Box>
            </Fade>
        </Box>
    );
};

export default Page;