import type {MultiFilter} from "../../models/models.ts";
import {Box, Checkbox, FormControl, MenuItem, OutlinedInput, Select} from "@mui/material";
import Typography from "../Typography.tsx";

type MultiFilterComponentProps<T> = {
    title: string;
    filter?: MultiFilter<T>;
    onChange: (id: T, isEnabled: boolean) => void;
}

function MultiFilterComponent<T>(props: MultiFilterComponentProps<T>) {
    const values = props.filter?.items.filter(x => x.isEnabled).map(x => x.title) ?? [];

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
                    }}>

                    <MenuItem disabled value={`placeholder-${props.title}`}>
                        <em>{props.title}</em>
                    </MenuItem>

                    {props.filter?.items.map((item, index) => {
                        return (
                            <MenuItem
                                key={String(item?.id ?? `no-id-${index}`)}
                                value={item.title}
                                onClick={() => props.onChange(item?.id, !item.isEnabled)}>
                                <Box className={'flex justify-between p-[5[px] w-full items-center'}>
                                    <Box className={'flex items-center'}>
                                        <Checkbox checked={item.isEnabled}/>
                                        <Typography variant={'bodyRegular'}>{item.title || 'Отсутствует'}</Typography>
                                    </Box>
                                    <Typography variant={'bodyRegular'}>{item.count}</Typography>
                                </Box>
                            </MenuItem>
                        );
                    })}
                </Select>
            </FormControl>
        </Box>
    )
}

export default MultiFilterComponent;