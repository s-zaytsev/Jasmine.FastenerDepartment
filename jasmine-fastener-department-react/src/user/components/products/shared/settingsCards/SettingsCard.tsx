import type {ReactNode} from "react";
import {Box} from "@mui/material";
import Typography from "../../../../../shared/components/Typography.tsx";
import Card from "../../../../../shared/components/Card.tsx";
import {primitives} from "../../../../../assets/variables/primitives.ts";

type ProductFormCardProps = {
    title: string;
    icon: ReactNode;
    children: ReactNode;
}

const SettingsCard = (props: ProductFormCardProps) => {
    return (
        <Card>
            <Box className={'w-full'}>
                <Box className={'flex items-center mb-[1rem]'}>

                    <Box className={'flex items-center mr-[0.5rem]'}
                         sx={{color: primitives.colors.primary}}>
                        {props.icon}
                    </Box>

                    <Typography variant={'headlineH3'}>{props.title}</Typography>
                </Box>
                <>{props.children}</>
            </Box>
        </Card>
    )
}

export default SettingsCard;