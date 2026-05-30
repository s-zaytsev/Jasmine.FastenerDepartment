import type {ProductChangeReason} from "../../../user/models/productModel.ts";
import Chip from "../Chip.tsx";

type ReasonChipProps = {
    reason: ProductChangeReason;
}

const ReasonChip = (props: ReasonChipProps) => {
    return (
        <Chip title={props.reason.description} color={'active'} />
    )
}

export default ReasonChip;