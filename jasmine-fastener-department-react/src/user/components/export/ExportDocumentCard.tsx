import {Box} from "@mui/material";
import type {ReactNode} from "react";

type ExportDocumentCardProps = {
    title: string;
    description: string;
    icon: ReactNode;
    onClick: () => void;
}

const ExportDocumentCard = (props: ExportDocumentCardProps) => {
    return (
        <Box onClick={props.onClick} className={'flex flex-col export-document-card p-[1rem]'}>
            <p>{props.title}</p>
            <Box style={{height: '200px'}}>{props.icon}</Box>
            <p>{props.description}</p>
        </Box>
    );
}

export default ExportDocumentCard;

