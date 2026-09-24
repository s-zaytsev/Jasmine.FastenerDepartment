import type {OrderFormTemplateContent, TemplateContent} from "../../../models/templateModels.ts";
import {Box} from "@mui/material";
import Typography from "../../../../shared/components/Typography.tsx";
import Chip from "../../../../shared/components/Chip.tsx";
import {memo} from "react";

type OrderFormTemplateCardContentProps = {
    content: TemplateContent;
}

const OrderFormTemplateCardContent = (props: OrderFormTemplateCardContentProps) => {
    const content = props.content as unknown as OrderFormTemplateContent;

    return (
        <Box className={'w-full flex flex-col gap-[0.5rem]'}>

            <Box className={'flex justify-between items-center'}>
                <Typography variant={'labelRegular'} color={'tertiary'}>Данные компании</Typography>
                <Chip
                    title={content.hasCompanyData ? 'Вкл' : 'Выкл'}
                    color={content.hasCompanyData ? 'active' : 'inactive'}
                />
            </Box>

            <Box className={'flex justify-between items-center'}>
                <Typography variant={'labelRegular'} color={'tertiary'}>Группировка по типу</Typography>
                <Chip
                    title={content.groupByType ? 'Вкл' : 'Выкл'}
                    color={content.groupByType ? 'active' : 'inactive'}
                />
            </Box>

            <Box className={'flex justify-between items-center  gap-[0.5rem]'}>
                <Typography variant={'labelRegular'} color={'tertiary'}>Колонки</Typography>
                <Box className={'flex'}>
                    <Typography variant={'labelRegular'}>
                        {content.tableColumns.map(x => x.name).join(', ')}
                    </Typography>
                </Box>
            </Box>
        </Box>
    )
}

export default memo(OrderFormTemplateCardContent);