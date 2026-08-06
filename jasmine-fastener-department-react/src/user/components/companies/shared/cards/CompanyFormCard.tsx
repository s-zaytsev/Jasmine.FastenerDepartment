import {memo, type ReactNode} from "react";
import Card from "../../../../../shared/components/Card.tsx";
import {Box} from "@mui/material";
import {primitives} from "../../../../../assets/variables/primitives.ts";
import Typography from "../../../../../shared/components/Typography.tsx";

type CompanyFormCardProps = {
    title: string;
    icon: ReactNode;
    children: ReactNode;
}

const CompanyFormCard = (props: CompanyFormCardProps) => {
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

export default memo(CompanyFormCard);