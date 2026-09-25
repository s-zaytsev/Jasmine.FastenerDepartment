import type {Template, TemplateFormatCode} from "../../models/templateModels.ts";
import {Box, Grow} from "@mui/material";
import Typography from "../../../shared/components/Typography.tsx";
import ExportDocumentCard from "./ExportDocumentCard.tsx";

type ExportDocumentGroupProps = {
    title: string;
    templates: Template[];
    onDownload: (templateId: string, formatCode: TemplateFormatCode) => void;
}

const ExportDocumentGroup = (props: ExportDocumentGroupProps) => {
    return (
        <Box className={'flex flex-col gap-[1rem]'}>
            <Typography variant={'headlineH3'}>{props.title}</Typography>
            <Box className={'flex flex-wrap gap-[1rem]'}>
                {props.templates.map((x, index) => (
                    <Grow key={x.id} in={true} timeout={index * 150}>
                        <Box className={'w-[23%]'}>
                            <ExportDocumentCard
                                key={x.id}
                                template={x}
                                onDownload={props.onDownload}
                            />
                        </Box>
                    </Grow>))}
            </Box>
        </Box>
    )
}

export default ExportDocumentGroup;