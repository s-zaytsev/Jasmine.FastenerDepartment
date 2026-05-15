import type {MultiFilter} from "../../models/models.ts";
import ElementsGroup from "../elementsGroup/ElementsGroup.tsx";
import ElementsGroupItem from "../elementsGroup/ElementsGroupItem.tsx";

type GroupFilterProps<T> = {
    filter?: MultiFilter<T>;
    prefix?: string;
    onChange: (id: T, isEnabled: boolean) => void;
}

function GroupFilter<T>(props: GroupFilterProps<T>) {
    return (
        <ElementsGroup>
            {props.filter?.items.map((x, index) =>
                <ElementsGroupItem
                    key={String(x?.id ?? `no-id-${index}`)}
                    id={x.id}
                    title={props.prefix ? `${props.prefix} ${x.title}` : x.title}
                    isChecked={x.isEnabled}
                    onChange={() => props.onChange(x.id, !x.isEnabled)}
                />
            )}
        </ElementsGroup>
    )
}

export default GroupFilter;