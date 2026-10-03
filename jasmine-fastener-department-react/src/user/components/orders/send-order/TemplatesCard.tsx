import type {Template} from "../../../models/templateModels.ts";
import {Box} from "@mui/material";
import TemplatesCardItem from "./TemplatesCardItem.tsx";
import IconBox from "../../../../shared/components/IconBox.tsx";
import {NotesOutlined} from "@mui/icons-material";
import Typography from "../../../../shared/components/Typography.tsx";
import Card from "../../../../shared/components/Card.tsx";
import {memo} from "react";

type TemplatesCardProps = {
    templates: Template[];
    selectedTemplate?: Template;
    onSelectTemplate: (id: string) => void;
}

const TemplatesCard = (props: TemplatesCardProps) => {
    return (
        <Card>
            <Box className={'w-full flex flex-col gap-[1rem]'}>
                <Box className={'flex items-center gap-[0.5rem]'}>
                    <IconBox>
                        <NotesOutlined color={'primary'}/>
                    </IconBox>

                    <Box className={'flex flex-col gap-[0.2rem]'}>
                        <Typography variant={'headlineH3'}>Шаблоны</Typography>
                        <Typography variant={'labelSmall'} color={'tertiary'}>
                            Выберите шаблон бланка заказа, который будет сформирован и отправлен адресату
                        </Typography>
                    </Box>
                </Box>

                <Box className={'flex flex-col gap-[0.5rem]'}>
                    {props.templates.map(template => (
                        <TemplatesCardItem
                            key={template.id}
                            template={template}
                            isActive={props.selectedTemplate?.id === template.id}
                            onChange={props.onSelectTemplate}/>
                    ))}
                </Box>
            </Box>
        </Card>
    )
}

export default memo(TemplatesCard);