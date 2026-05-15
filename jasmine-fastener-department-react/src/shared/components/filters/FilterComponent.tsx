import type {Filter} from "../../models/models.ts";
import {Box, Checkbox} from "@mui/material";
import Typography from "../Typography.tsx";
import {type ChangeEvent, useState} from "react";

type FilterComponentProps<T> = {
    filter: Filter<T>
    onChange: (id: T, isEnabled: boolean) => void;
}

function FilterComponent<T>(props: FilterComponentProps<T>) {
    const [isEnabled, setIsEnabled] = useState<boolean>(props.filter.isEnabled);

    function handleChange(e: ChangeEvent<HTMLInputElement>) {
        setIsEnabled(e.target.checked);
        props.onChange(props.filter.id, e.target.checked);
    }

    return (
        <Box className={'filter-row'}>
            <Box className={'flex'}>
                <Typography variant={'labelRegular'}>{props.filter.title || 'Отсутствует'}</Typography>
                <Typography variant={'labelRegular'}>&nbsp;({props.filter.count})</Typography>
            </Box>
            <Checkbox checked={isEnabled} onChange={(e) => handleChange(e)}/>
        </Box>
    )
}

export default FilterComponent;