import type {ProductCatalogTemplateContent, TemplateContent} from "../../../models/templateModels.ts";
import {Box} from "@mui/material";
import Typography from "../../../../shared/components/Typography.tsx";
import Chip from "../../../../shared/components/Chip.tsx";
import {memo} from "react";

type ProductCatalogTemplateCardContentProps = {
    content: TemplateContent;
}

const ProductCatalogTemplateCardContent = (props: ProductCatalogTemplateCardContentProps) => {
    const content = props.content as unknown as ProductCatalogTemplateContent;

    return (
        <Box className={'w-full flex flex-col gap-[0.5rem]'}>
            <Box className={'flex justify-between items-center gap-[0.5rem]'}>
                <Typography variant={'labelRegular'} color={'tertiary'}>Группировка по типу</Typography>
                <Chip
                    title={content.groupByType ? 'Вкл' : 'Выкл'}
                    color={content.groupByType ? 'active' : 'inactive'}
                />
            </Box>

            <Box className={'flex justify-between items-center'}>
                <Typography variant={'labelRegular'} color={'tertiary'}>Колонки</Typography>
                <Box className={'flex gap-[0.5rem]'}>
                    <Typography variant={'labelRegular'}>
                        {content.tableColumns.map(x => x.name).join(', ')}
                    </Typography>
                </Box>
            </Box>
        </Box>
    )
}

export default memo(ProductCatalogTemplateCardContent);