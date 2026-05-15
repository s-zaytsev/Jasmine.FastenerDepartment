import type {Supplier} from "../../../../models/supplierModels.ts";
import ElementsGroupItem from "../../../../../shared/components/elementsGroup/ElementsGroupItem.tsx";

type SuppliersGroupItemProps = {
    supplier: Supplier;
    isChecked: boolean;
    onChange: (id: string) => void;
}

const SuppliersGroupItem = (props: SuppliersGroupItemProps) => {
    return (
        <ElementsGroupItem
            id={props.supplier.id}
            title={props.supplier.name}
            isChecked={props.isChecked}
            onChange={props.onChange}/>
    )
}

export default SuppliersGroupItem;