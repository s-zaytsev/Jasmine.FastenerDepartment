import {memo} from "react";
import {Box} from "@mui/material";
import IconBox from "../../../../shared/components/IconBox.tsx";
import {InsertDriveFileOutlined} from "@mui/icons-material";
import Typography from "../../../../shared/components/Typography.tsx";
import Card from "../../../../shared/components/Card.tsx";
import {TemplateFormatCode} from "../../../models/templateModels.ts";
import FormatsCardItem from "./FormatsCardItem.tsx";

const formats = [
    {code: TemplateFormatCode.word, title: 'Microsoft Word', extension: '.docx'},
    {code: TemplateFormatCode.html, title: 'HTML', extension: '.html'}
]

type FormatsCardProps = {
    selectedFormats: TemplateFormatCode[];
    onSelectFormat: (code: TemplateFormatCode) => void;
}

const FormatsCard = (props: FormatsCardProps) => {
    return (
        <Card>
            <Box className={'w-full flex flex-col gap-[1rem]'}>
                <Box className={'flex items-center gap-[0.5rem]'}>
                    <IconBox>
                        <InsertDriveFileOutlined/>
                    </IconBox>

                    <Box className={'flex flex-col gap-[0.2rem]'}>
                        <Typography variant={'headlineH3'}>Форматы файлов</Typography>
                        <Typography variant={'labelSmall'} color={'tertiary'}>
                            Выберите один или несколько форматов файлов, которые будут прикреплены к сообщению
                        </Typography>
                    </Box>
                </Box>

                <Box className={'flex flex-col gap-[0.5rem]'}>
                    {formats.map(format => (
                        <FormatsCardItem
                            key={format.code}
                            code={format.code}
                            title={format.title}
                            extension={format.extension}
                            isActive={!!props.selectedFormats.find(x => x === format.code)}
                            onChange={props.onSelectFormat}
                        />
                    ))}
                </Box>
            </Box>
        </Card>)
}

export default memo(FormatsCard);