import {PriceTagCode, type Product} from "../../../user/models/productModel.ts";
import TemplateS from "./TemplateS.tsx";
import TemplateL from "./TemplateL.tsx";
import TemplateM from "./TemplateM.tsx";
import TemplateXL from "./TemplateXL.tsx";

type PriceTagProps = {
    product: Product;
}

const PriceTag = (props: PriceTagProps) => {
    switch (props.product.priceTagCode) {
        case PriceTagCode.s:
            return <TemplateS product={props.product} />
        case PriceTagCode.l:
            return <TemplateL product={props.product} />
        case PriceTagCode.m:
            return <TemplateM product={props.product} />
        case PriceTagCode.xl:
            return <TemplateXL product={props.product} />
        default:
            return <p>Code isn't supported</p>
    }
}

export default PriceTag;