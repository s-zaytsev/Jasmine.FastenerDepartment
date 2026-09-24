import type {Template} from "../../models/templateModels.ts";
import {Box, Grow} from "@mui/material";
import TemplatesGridCard from "./TemplatesGridCard.tsx";
import Typography from "../../../shared/components/Typography.tsx";
import {memo} from "react";

type TemplatesGridGroupProps = {
    name: string;
    templates: Template[];
    onNavigateToChange: (id: string) => void;
}

const TemplatesGridGroup = (props: TemplatesGridGroupProps) => {
    return (
        <Box>
            <Typography variant={'headlineH3'}>{props.name}</Typography>
            <Box className={'flex flex-wrap gap-[1rem]'}>
                {props.templates.map((template, index) =>
                    <Grow key={template.id} in={true} timeout={index * 150}>
                        <Box className={'w-[23%]'}>
                            <TemplatesGridCard
                                template={template}
                                onEdit={props.onNavigateToChange}
                            />
                        </Box>
                    </Grow>
                )}
            </Box>
        </Box>
    );
}

export default memo(TemplatesGridGroup);