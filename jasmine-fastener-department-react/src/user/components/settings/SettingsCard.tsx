import {memo, type ReactNode} from "react";
import {Box} from "@mui/material";
import Typography from "../../../shared/components/Typography.tsx";
import Card from "../../../shared/components/Card.tsx";
import IconBox from "../../../shared/components/IconBox.tsx";

type SettingsCardProps = {
    title: string;
    icon: ReactNode;
    children: ReactNode;
}

const SettingsCard = (props: SettingsCardProps) => {
    return (
        <Card sx={{height: 'fit-content'}}>
            <Box className={'w-full h-fit'}>
                <Box className={'mb-[1rem] flex gap-[0.5rem] items-center'}>
                    <IconBox>{props.icon}</IconBox>
                    <Typography variant={'headlineH3'}>{props.title}</Typography>
                </Box>
                {props.children}
            </Box>
        </Card>
    );
}

export default memo(SettingsCard);