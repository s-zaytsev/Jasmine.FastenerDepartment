import {memo, type ReactNode} from "react";
import {Box} from "@mui/material";
import FilledButton from "../buttons/FilledButton.tsx";
import PageHeaderTitle from "./PageHeaderTitle.tsx";
import IconButton from "../buttons/IconButton.tsx";

type PageHeaderProps = {
    title: string;
    description: string;
    button?: {
        label: string;
        type?: "submit" | "reset" | "button";
        formId?: string;
        isDisabled?: boolean;
        onClick?: (args?: any) => void;
    };
    iconButton?: {
        icon: ReactNode;
        description: string;
        isDisabled?: boolean;
        onClick?: (args?: any) => void;
    };
}

const PageHeader = (props: PageHeaderProps) => {
    return (
        <Box className={'flex justify-between w-full items-center mb-[1rem]'}>
            <PageHeaderTitle title={props.title} description={props.description}/>

            <Box className={'flex items-center gap-[1rem]'}>
                {props.iconButton && <Box>
                    <IconButton
                        hasBackground={true}
                        description={props.iconButton?.description}
                        onClick={props.iconButton.onClick}
                        isDisabled={props.iconButton.isDisabled}
                    >
                        {props.iconButton.icon}
                    </IconButton>
                </Box>}

                {props.button && <Box>
                    <FilledButton
                        disabled={props.button.isDisabled}
                        type={props.button.type || "button"}
                        form={props.button.formId}
                        variant={'contained'}
                        onClick={props.button.onClick}
                    >
                        {props.button.label}
                    </FilledButton>
                </Box>}
            </Box>
        </Box>
    );
}

export default memo(PageHeader);