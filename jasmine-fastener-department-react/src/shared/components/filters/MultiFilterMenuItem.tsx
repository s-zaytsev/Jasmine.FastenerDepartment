import {Box, Checkbox, MenuItem} from "@mui/material";
import Typography from "../Typography.tsx";
import {memo} from "react";
import {primitives} from "../../../assets/variables/primitives.ts";

type MultiFilterMenuItemProps<T> = {
    title: string;
    isEnabled: boolean;
    count: number;
    itemId: T;
    onChange: (id: T, isEnabled: boolean) => void;
}

function MultiFilterMenuItem<T>(props: MultiFilterMenuItemProps<T>) {
    return (
        <MenuItem
            value={props.title}
            onClick={() => props.onChange(props.itemId, !props.isEnabled)}
        >
            <Box className={'w-full flex items-center justify-between pr-[0.5rem]'}
                sx={{
                    backgroundColor: props.isEnabled ? primitives.colors.tonal : undefined,
                    borderRadius: primitives.border.radius
                }}
            >
                <Box className={'w-full flex items-center'}>
                    <Checkbox checked={props.isEnabled}/>
                    <Typography
                        variant={props.isEnabled ? 'bodyRegularBold' : 'bodyRegular'}
                        color={props.isEnabled ? 'primary' : 'tertiary'}>
                        {props.title || 'Отсутствует'}
                    </Typography>
                </Box>
                <Typography
                    variant={props.isEnabled ? 'bodyRegularBold' : 'bodyRegular'}
                    color={props.isEnabled ? 'primary' : 'tertiary'}>
                    {props.count}
                </Typography>
            </Box>
        </MenuItem>
    );
}

export default memo(MultiFilterMenuItem) as typeof MultiFilterMenuItem;