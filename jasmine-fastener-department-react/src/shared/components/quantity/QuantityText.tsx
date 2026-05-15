import type {Quantity} from "../../models/models.ts";
import useQuantity from "../../hooks/useQuantity.ts";

type QuantityTextProps = {
    quantity: Quantity;
}

const QuantityText = (props: QuantityTextProps) => {
    const {getText} = useQuantity();

    return <>{getText(props.quantity)}</>
}

export default QuantityText;