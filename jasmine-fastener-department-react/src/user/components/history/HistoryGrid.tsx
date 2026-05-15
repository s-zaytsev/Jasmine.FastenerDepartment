import {type ProductHistoryEntry} from "../../models/productModel.ts";
import {Box} from "@mui/material";
import type {ProductType} from "../../models/productTypeModels.ts";
import ProductHistoryCard from "../../../shared/components/history/ProductHistoryCard.tsx";
import Section from "../../../shared/components/section/Section.tsx";

type HistoryRowProps = {
    date: Date;
    items: ProductHistoryEntry[];
    productTypes: ProductType[];
    onNavigate: (id: string) => void;
}

const HistoryGrid = (props: HistoryRowProps) => {

    return (
        <Box className={'flex flex-col'}>
            <Section title={new Date(props.date).toLocaleDateString()}>
                <Box className={'flex flex-col'}>
                    {props.items.map((x) =>
                        <ProductHistoryCard key={x.id} historyEntry={x} productTypes={props.productTypes}/>)
                    }
                </Box>
            </Section>
        </Box>
    )
}

export default HistoryGrid;