import type {Supplier} from "../../../../models/supplierModels.ts";
import SuppliersGroupItem from "./SuppliersGroupItem.tsx";
import ElementsGroup from "../../../../../shared/components/elementsGroup/ElementsGroup.tsx";
import {memo} from "react";

type SuppliersGroupProps = {
    suppliers: Supplier[];
    productSupplierIds: string[];
    onChange: (id: string) => void;
}

const SuppliersGroup = (props: SuppliersGroupProps) => {

    return (
        <ElementsGroup>
            {props.suppliers.map(x =>
                <SuppliersGroupItem
                    key={x.id}
                    supplier={x}
                    isChecked={props.productSupplierIds.some(s => s == x.id)}
                    onChange={props.onChange}/>)
            }
        </ElementsGroup>
    )
}

export default memo(SuppliersGroup);