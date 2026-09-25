import {Box} from "@mui/material";
import {type Template, TemplateFormatCode} from "../../models/templateModels.ts";
import Card from "../../../shared/components/Card.tsx";
import Typography from "../../../shared/components/Typography.tsx";
import IconBox from "../../../shared/components/IconBox.tsx";
import {SubjectOutlined} from "@mui/icons-material";
import ExportFormatGrid from "./ExportFormatGrid.tsx";
import {useState} from "react";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";

type ExportDocumentCardProps = {
    template: Template;
    onDownload: (templateId: string, formatCode: TemplateFormatCode) => void;
}

const ExportDocumentCard = (props: ExportDocumentCardProps) => {
    const [formatTypeCode, setFormatTypeCode] = useState<TemplateFormatCode>(TemplateFormatCode.word)

    const handleChangeFormatTypeCode = (code: TemplateFormatCode) => {
        setFormatTypeCode(code);
    }

    const handleDownload = () => {
        debugger
        props.onDownload(props.template.id, formatTypeCode);
    }

    return (
        <Card>
            <Box className={'w-full flex flex-col gap-[1rem]'}>
                <Box className={'flex'}>
                    <IconBox>
                        <SubjectOutlined/>
                    </IconBox>
                </Box>

                <Typography variant={'headlineH3'}>{props.template?.name}</Typography>

                <ExportFormatGrid
                    selected={formatTypeCode}
                    onChange={handleChangeFormatTypeCode}
                />

                <Box className={'flex justify-center items-center'}>
                    <FilledButton
                        variant={'contained'}
                        sx={{width: '100%'}}
                        onClick={handleDownload}>
                        Скачать
                    </FilledButton>
                </Box>
            </Box>
        </Card>
    );
}

export default ExportDocumentCard;

