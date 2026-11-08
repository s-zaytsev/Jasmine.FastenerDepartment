import {Box, Fade} from "@mui/material";
import type {ReactNode} from "react";
import PageHeader from "../header/PageHeader.tsx";
import type {HeaderButton, HeaderIconButton} from "../../models/models.ts";

export interface PageProps {
    title: string;
    description: string;
    button?: HeaderButton;
    iconButton?: HeaderIconButton;
    children: ReactNode;
}

const Page = (props: PageProps) => {
    return (
        <Box className={'flex flex-col w-full h-full'}>
            <PageHeader
                title={props.title}
                description={props.description}
                button={props.button}
                iconButton={props.iconButton}
            />
            <Fade className={'h-full'} in={true} timeout={300}>
                <Box>{props.children}</Box>
            </Fade>
        </Box>
    );
};

export default Page;