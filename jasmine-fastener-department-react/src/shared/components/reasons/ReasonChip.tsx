import type {ProductChangeReasonCode} from "../../../user/models/productModel.ts";
import useReason from "../../hooks/useReason.ts";
import Chip from "../Chip.tsx";

type ReasonChipProps = {
    reasonCode: ProductChangeReasonCode;
}

const ReasonChip = (props: ReasonChipProps) => {
    const {
        getReasonName
    } = useReason();

    const reasonName: string = getReasonName(props.reasonCode);

    return (
        <Chip title={reasonName} color={'active'} />
    )
}

export default ReasonChip;