import {type TemplateContent, type TemplateType, TemplateTypeCode} from "../../../models/templateModels.ts";
import ProductCatalogTemplateCardContent from "./ProductCatalogTemplateCardContent.tsx";
import OrderFormTemplateCardContent from "./OrderFormTemplateCardContent.tsx";
import {Box} from "@mui/material";
import {semanticColors} from "../../../../assets/variables/semanticColors.ts";
import {primitives} from "../../../../assets/variables/primitives.ts";
import {memo} from "react";

type TemplateCardContentProps = {
    templateType: TemplateType;
    content: TemplateContent;
}

const TemplateCardContent = (props: TemplateCardContentProps) => {

    const content = () => {
        switch (props.templateType.id) {
            case TemplateTypeCode.productCatalog:
                return <ProductCatalogTemplateCardContent content={props.content} />
            case TemplateTypeCode.orderForm:
                return <OrderFormTemplateCardContent content={props.content} />
            default:
                return <></>;
        }
    }

    return(
        <Box sx={{
            backgroundColor: semanticColors.surface.medium,
            borderRadius: primitives.border.radius,
            padding: '1rem'
        }}>
            {content()}
        </Box>
    )
}

export default memo(TemplateCardContent);