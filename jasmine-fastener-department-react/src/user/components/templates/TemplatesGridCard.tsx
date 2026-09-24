import type {Template} from "../../models/templateModels.ts";
import {Box} from "@mui/material";
import Typography from "../../../shared/components/Typography.tsx";
import Card from "../../../shared/components/Card.tsx";
import IconBox from "../../../shared/components/IconBox.tsx";
import {Edit, NotesOutlined} from "@mui/icons-material";
import IconButton from "../../../shared/components/buttons/IconButton.tsx";
import TemplateCardContent from "./templateCardsContent/TemplateCardContent.tsx";
import {memo} from "react";

type TemplateGridCardProps = {
    template: Template;
    onEdit: (id: string) => void;
}

const TemplatesGridCard = (props: TemplateGridCardProps) => {
    return (
        <Card>
            <Box className={'w-full flex flex-col gap-[1rem]'}>
                <Box className={'flex justify-between items-center'}>

                    <Box className={'flex items-center gap-[0.5rem]'}>
                        <IconBox>
                            <NotesOutlined color={'primary'}/>
                        </IconBox>
                        <Typography variant={'headlineH3'}>{props.template.name}</Typography>
                    </Box>

                    <IconButton
                        description={'Редактировать'}
                        onClick={() => props.onEdit(props.template.id)}
                    >
                        <Edit/>
                    </IconButton>
                </Box>

                <TemplateCardContent templateType={props.template.type} content={props.template.content}/>
            </Box>
        </Card>
    );
}

export default memo(TemplatesGridCard);