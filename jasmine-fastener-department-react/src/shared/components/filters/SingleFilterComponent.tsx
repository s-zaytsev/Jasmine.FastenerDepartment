import type {SingleFilter} from "../../models/models.ts";
import {type ChangeEvent, memo, useCallback, useState} from "react";
import {Box, Switch} from "@mui/material";
import Typography from "../Typography.tsx";

type SingleFilterComponentProps = {
    title: string;
    filter?: SingleFilter;
    onChange: (isEnabled: boolean) => void;
}

const SingleFilterComponent = (props: SingleFilterComponentProps) => {
    const [isEnabled, setIsEnabled] = useState(props.filter?.isEnabled ?? false);

    const handleChange = useCallback((e: ChangeEvent<HTMLInputElement>) => {
        setIsEnabled(e.target.checked);
        props.onChange(e.target.checked);
    }, []);

    return (
        <Box className={'flex justify-between items-center mx-[1rem] w-full'}>
            <Typography variant={'bodyRegularBold'}>{props.title} ({props.filter?.count})&nbsp;</Typography>
            <Switch checked={isEnabled} onChange={(e) => handleChange(e)}/>
        </Box>
    );
}

export default memo(SingleFilterComponent);