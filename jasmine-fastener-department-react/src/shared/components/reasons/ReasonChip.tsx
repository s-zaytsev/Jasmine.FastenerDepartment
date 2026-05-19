import type {ProductChangeReason} from "../../../user/models/productModel.ts";
import useReason from "../../hooks/useReason.ts";
import Chip from "../Chip.tsx";

type ReasonChipProps = {
    reason: ProductChangeReason;
}

const ReasonChip = (props: ReasonChipProps) => {
    const {
        getReasonName
    } = useReason();

    const reasonName: string = getReasonName(props.reason.code);

    return (
        <Chip title={props.reason.description} color={'active'} />
    )
}

export default ReasonChip;