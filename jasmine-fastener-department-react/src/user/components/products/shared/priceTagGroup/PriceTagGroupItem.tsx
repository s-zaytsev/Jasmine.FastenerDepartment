import {PriceTagCode} from "../../../../models/productModel.ts";
import ElementsGroupItem from "../../../../../shared/components/elementsGroup/ElementsGroupItem.tsx";

type PriceTagGroupItemProps = {
    code: PriceTagCode;
    isChecked: boolean;
    onChange: (value: PriceTagCode) => void;
}

const PriceTagGroupItem = (props: PriceTagGroupItemProps) => {
    return (
        <ElementsGroupItem<PriceTagCode>
            id={props.code}
            title={`Размер ${PriceTagCode[props.code].toUpperCase()}`}
            isChecked={props.isChecked}
            onChange={props.onChange} />
    )
}

export default PriceTagGroupItem;