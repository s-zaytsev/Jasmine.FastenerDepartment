import type {MultiFilter} from "../../models/models.ts";
import {Box, FormControl, MenuItem, OutlinedInput, Select} from "@mui/material";
import Typography from "../Typography.tsx";
import {memo, useMemo} from "react";
import MultiFilterMenuItem from "./MultiFilterMenuItem.tsx";

type MultiFilterComponentProps<T> = {
    title: string;
    filter?: MultiFilter<T>;
    onChange: (id: T, isEnabled: boolean) => void;
}

function MultiFilterComponent<T>(props: MultiFilterComponentProps<T>) {
    const values = useMemo(() => {
        return props.filter?.items.filter(x => x.isEnabled).map(x => x.title?.length ? x.title : 'Отсутствует') ?? [];
    }, [props.filter?.items]);

    return (
        <Box className={'w-full'}>
            <FormControl fullWidth>
                <Select
                    multiple
                    displayEmpty
                    value={values}
                    input={<OutlinedInput/>}
                    renderValue={(selected) => {
                        if (selected.length === 0) {
                            return <Typography variant={'bodyRegular'} color={'tertiary'}>{props.title}</Typography>;
                        } else {
                            return selected.join(', ')
                        }
                    }}
                    MenuProps={{ PaperProps: { sx: { maxHeight: 500 } } }}
                >

                    <MenuItem disabled value={`placeholder-${props.title}`}>
                        <em>{props.title}</em>
                    </MenuItem>

                    {props.filter?.items.map((item, index) =>
                        <MultiFilterMenuItem
                            key={String(item.id ?? `no-id-${index}`)}
                            title={item.title}
                            isEnabled={item.isEnabled}
                            count={item.count}
                            itemId={item.id}
                            onChange={props.onChange}
                        />)}
                </Select>
            </FormControl>
        </Box>
    )
}

export default memo(MultiFilterComponent) as typeof MultiFilterComponent;