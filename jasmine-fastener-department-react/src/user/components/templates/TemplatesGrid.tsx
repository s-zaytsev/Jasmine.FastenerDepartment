import type {Template} from "../../models/templateModels";
import EmptyGrid from "../../../shared/components/EmptyGrid.tsx";
import {Box} from "@mui/material";
import useGroup from "../../../shared/hooks/useGroup.ts";
import {memo, useMemo} from "react";
import TemplatesGridGroup from "./TemplatesGridGroup.tsx";

type TemplatesGridProps = {
    templates: Template[];
    onNavigateToChange: (id: string) => void;
}

const TemplatesGrid = (props: TemplatesGridProps) => {
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
        return <EmptyGrid message={'Список шаблонов пуст'}/>
    }

    return (
        <Box className={'w-full flex flex-col gap-[2rem]'}>
            {Object.entries(groupedByType).map(([key, value]) => (
                <TemplatesGridGroup
                    key={key}
                    name={key}
                    templates={value}
                    onNavigateToChange={props.onNavigateToChange}
                />
            ))}
        </Box>
    );
}

export default memo(TemplatesGrid);