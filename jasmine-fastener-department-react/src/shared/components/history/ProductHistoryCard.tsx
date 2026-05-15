import type {ProductHistoryEntry} from "../../../user/models/productModel.ts";
import Card from "../Card.tsx";
import {Box} from "@mui/material";
import Typography from "../Typography.tsx";
import ReasonChip from "../reasons/ReasonChip.tsx";
import useHistory from "../../hooks/useHistory.ts";
import type {ProductType} from "../../../user/models/productTypeModels.ts";

type ProductHistoryCardProps = {
    historyEntry: ProductHistoryEntry;
    productTypes: ProductType[];
}

const ProductHistoryCard = (props: ProductHistoryCardProps) => {
    const {
        getText
    } = useHistory();

    const text = getText(props.historyEntry, props.productTypes)

    return (
        <Card>
            <Box className={'w-full'}>
                <Box className={'flex w-full justify-between'}>
                    <Typography variant={'bodySmall'}>
                        {new Date(props.historyEntry.createdDate).toLocaleString()}
                    </Typography>
                    <ReasonChip reasonCode={props.historyEntry.changeReasonCode}/>
                </Box>

                <Box className={'w-full my-[0.5rem]'}>
                    <Typography variant={'headlineH3'}>{text[0]}{text[1]}</Typography>
                </Box>

                <Box>
                    <Typography variant={'bodySmall'}>{text[2]}</Typography>
                </Box>

            </Box>
        </Card>
    )
}

export default ProductHistoryCard;