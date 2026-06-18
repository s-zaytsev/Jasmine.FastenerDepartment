import {memo} from "react";
import {Box} from "@mui/material";
import FilledButton from "../buttons/FilledButton.tsx";
import PageHeaderTitle from "./PageHeaderTitle.tsx";

type PageHeaderProps = {
    title: string;
    description: string;
    button?: {
        label: string;
        type?: "submit" | "reset" | "button";
        formId?: string;
        onClick?: (args?: any) => void;
    };
}

const PageHeader = (props: PageHeaderProps) => {
    return (
        <Box className={'flex justify-between w-full items-center mb-[1rem]'}>
            <PageHeaderTitle title={props.title} description={props.description}/>

            {props.button && <Box>
                <FilledButton
                    type={props.button.type || "button"}
                    form={props.button.formId}
                    variant={'contained'}
                    onClick={props.button.onClick}
                >
                    {props.button.label}
                </FilledButton>
            </Box>}
        </Box>
    );
}

export default memo(PageHeader);