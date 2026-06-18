import {numberEnumToArray} from "../../../../../shared/models/models.ts";
import {PriceTagCode} from "../../../../models/productModel.ts";
import ElementsGroup from "../../../../../shared/components/elementsGroup/ElementsGroup.tsx";
import PriceTagGroupItem from "./PriceTagGroupItem.tsx";
import {memo, useMemo} from "react";

type PriceTagGroupProps = {
    checkedCode: PriceTagCode;
    onChange: (value: PriceTagCode) => void;
}

const PriceTagGroup = (props: PriceTagGroupProps) => {

    const items = useMemo(() => {
        return numberEnumToArray(PriceTagCode)
            .map(x =>
                <PriceTagGroupItem
                    key={x}
                    code={x}
                    isChecked={x === props.checkedCode}
                    onChange={props.onChange}/>);
    }, [props.checkedCode, props.onChange]);

    return (
        <ElementsGroup>
            {items}
        </ElementsGroup>
    )
}

export default memo(PriceTagGroup);