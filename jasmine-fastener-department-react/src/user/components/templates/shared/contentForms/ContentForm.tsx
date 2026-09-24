import {
    type ChangeTemplate,
    type TemplateContentTableColumnGroup,
    TemplateTypeCode
} from "../../../../models/templateModels.ts";
import ProductCatalogContentForm from "./ProductCatalogContentForm.tsx";
import OrderFormContentForm from "./OrderFormContentForm.tsx";
import {type Control, type FieldErrors, useWatch} from "react-hook-form";
import {memo} from "react";

type ContentFormProps = {
    control: Control<ChangeTemplate, unknown, ChangeTemplate>;
    errors: FieldErrors<ChangeTemplate>;
    columnGroups: TemplateContentTableColumnGroup[];
    onChange: () => void;
}

const ContentForm = (props: ContentFormProps) => {
    const templateTypeCode = useWatch({
        control: props.control,
        name: "typeCode"
    });

    switch (templateTypeCode) {
        case TemplateTypeCode.productCatalog: {
            const group = props.columnGroups.find(x => x.typeCode === TemplateTypeCode.productCatalog);

            return <ProductCatalogContentForm
                control={props.control}
                errors={props.errors}
                columns={group?.columns ?? []}
                onChange={props.onChange}
            />
        }

        case TemplateTypeCode.orderForm: {
            const group = props.columnGroups.find(x => x.typeCode === TemplateTypeCode.orderForm);
            return <OrderFormContentForm
                control={props.control}
                errors={props.errors}
                columns={group?.columns ?? []}
                onChange={props.onChange}
            />
        }

    }
}

export default memo(ContentForm);
