import type {Template, TemplateFormatCode} from "../../models/templateModels.ts";
import useGroup from "../../../shared/hooks/useGroup.ts";
import {useMemo} from "react";
import EmptyGrid from "../../../shared/components/EmptyGrid.tsx";
import {Box} from "@mui/material";
import ExportDocumentGroup from "./ExportDocumentGroup.tsx";

type ExportDocumentGridProps = {
    templates: Template[];
    onDownload: (templateId: string, formatCode: TemplateFormatCode) => void;
}

const ExportDocumentGrid = (props: ExportDocumentGridProps) => {
    const {groupBy} = useGroup();

    const groupedByType = useMemo(() => {
        return groupBy(
            props.templates || [],
            x => x.type.name,
            {
                sortFn: (a, b) => a.name.localeCompare(b.name),
                sortGroups: true
            });
    }, [groupBy, props.templates]);

    if (props.templates.length === 0) {
        return <EmptyGrid message={'Список доступных документов пуст'}/>
    }

    return (
        <Box className={'w-full flex flex-col gap-[2rem]'}>
            {Object.entries(groupedByType).map(([key, value]) => (
                <ExportDocumentGroup
                    key={key}
                    title={key}
                    templates={value}
                    onDownload={props.onDownload}
                />
            ))}
        </Box>
    );
}

export default ExportDocumentGrid;