import {Box} from "@mui/material";
import type {ReactNode} from "react";
import Typography from "../Typography.tsx";
import FilledButton from "../buttons/FilledButton.tsx";

export interface PageProps {
    title: string;
    description: string;
    button?: {
        label: string;
        onClick: (args?: any) => void;
    };
    children: ReactNode;
}

const Page = (props: PageProps) => {
    return (
        <Box className={'flex flex-col w-full h-full'}>
            <Box className={'flex justify-between w-full items-center mb-[1rem]'}>
                <Box className={'flex flex-col'}>
                    <Typography variant={'headlineH1'}>{props.title}</Typography>
                    <Typography variant={'bodySmall'} color={'tertiary'}>{props.description}</Typography>
                </Box>
                {props.button && <Box>
                    <FilledButton
                        variant={'contained'}
                        onClick={props.button.onClick}
                    >
                        {props.button.label}
                    </FilledButton>
                </Box>}
            </Box>

            {props.children}
        </Box>
    );
};

export default Page;