import {Box} from "@mui/material";
import {type Template, TemplateFormatCode} from "../../models/templateModels.ts";
import Card from "../../../shared/components/Card.tsx";
import Typography from "../../../shared/components/Typography.tsx";
import {EmailOutlined, FileDownloadOutlined} from "@mui/icons-material";
import ExportFormatGrid from "./ExportFormatGrid.tsx";
import {memo, useState} from "react";
import IconButton from "../../../shared/components/buttons/IconButton.tsx";

type ExportDocumentCardProps = {
    template: Template;
    onDownload: (templateId: string, formatCode: TemplateFormatCode) => void;
    onSend: (templateId: string, formatCode: TemplateFormatCode) => void;
}

const ExportDocumentCard = (props: ExportDocumentCardProps) => {
    const [formatTypeCode, setFormatTypeCode] = useState<TemplateFormatCode>(TemplateFormatCode.word)

    const handleChangeFormatTypeCode = (code: TemplateFormatCode) => {
        setFormatTypeCode(code);
    }

    const handleDownload = () => {
        props.onDownload(props.template.id, formatTypeCode);
    }

    const handleSend = () => {
        props.onSend(props.template.id, formatTypeCode);
    }

    return (
        <Card>
            <Box className={'w-full flex flex-col gap-[1rem]'}>
                <Box className={'flex justify-between items-center'}>
                    <Typography variant={'headlineH3'}>{props.template?.name}</Typography>

                    <Box className={'flex gap-[0.5rem]'}>
                        <IconButton onClick={handleSend} hasBackground={true} description={'Отправить документ'}>
                            <EmailOutlined/>
                        </IconButton>

                        <IconButton onClick={handleDownload} hasBackground={true} description={'Скачать документ'}>
                            <FileDownloadOutlined/>
                        </IconButton>
                    </Box>
                </Box>

                <ExportFormatGrid
                    selected={formatTypeCode}
                    onChange={handleChangeFormatTypeCode}
                />
            </Box>
        </Card>
    );
}

export default memo(ExportDocumentCard);

