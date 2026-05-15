import type {ChangeOrder, CreateOrder} from "../../../../../models/orderModels.ts";
import useGroup from "../../../../../../shared/hooks/useGroup.ts";
import {Box} from "@mui/material";
import Section from "../../../../../../shared/components/section/Section.tsx";
import ConfirmOrderSectionTable from "./ConfirmOrderSectionTable.tsx";

type ConfirmOrderProps = {
    model: CreateOrder | ChangeOrder;
}

const ConfirmOrder = (props: ConfirmOrderProps) => {
    const {groupBy} = useGroup();
    const groupedByType = groupBy(
        props.model.products || [],
        x => x.productType?.name ?? 'Остальное',
        {
            sortFn: (a, b) => a.productName.localeCompare(b.productName),
            sortGroups: true
        });

    return (
        <Box className={'w-full flex flex-col gap-[2rem]'}>
            {
                Object.entries(groupedByType).map(([key, value]) => (
                    <Box key={key}>
                        <Section title={key} itemsCount={value.length}>
                            <ConfirmOrderSectionTable products={value}/>
                        </Section>
                    </Box>
                ))
            }
        </Box>
    );
}

export default ConfirmOrder;