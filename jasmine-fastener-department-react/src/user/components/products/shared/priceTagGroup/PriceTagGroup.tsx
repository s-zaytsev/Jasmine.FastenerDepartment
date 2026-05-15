import {enumToArray} from "../../../../../shared/models/models.ts";
import PriceTagGroupItem from "./PriceTagGroupItem.tsx";
import {PriceTagCode} from "../../../../models/productModel.ts";
import ElementsGroup from "../../../../../shared/components/elementsGroup/ElementsGroup.tsx";

type PriceTagGroupProps = {
    checkedCode: PriceTagCode;
    onChange: (value: PriceTagCode) => void;
}

const PriceTagGroup = (props: PriceTagGroupProps) => {
    return (
        <ElementsGroup>
            {enumToArray<PriceTagCode>(PriceTagCode)
                .map(x =>
                    <PriceTagGroupItem
                        key={x}
                        code={x}
                        isChecked={x === props.checkedCode}
                        onChange={props.onChange}/>)
            }
        </ElementsGroup>
    )
}

export default PriceTagGroup;