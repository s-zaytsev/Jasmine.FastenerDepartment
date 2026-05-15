import type {Product} from "../../../user/models/productModel.ts";
import PriceTagProductName from "./PriceTagProductName.tsx";
import ProductPrice from "../ProductPrice.tsx";

type TemplateXLProps = {
    product: Product;
}

const TemplateXL = (props: TemplateXLProps) => {
    return (
        <>
            <div style={{
                width: "156px",
                fontSize: "12px",
                height: "200px",
                color: "#000000",
                border: "1px dashed #000000",
                margin: "-1px 0 0 -1px",
                padding: "5px",
                textAlign: "center",
                display: "flex",
                flexDirection: "column",
                justifyContent: "space-between",
                boxSizing: "unset",
                lineHeight: 'normal'
            }}>
                <PriceTagProductName product={props.product}/>
                <p style={{fontSize: "19px"}}><strong>{<ProductPrice product={props.product}/>}</strong></p>
                <p style={{fontSize: "18px"}}><strong>{props.product.number}</strong></p>
            </div>
        </>
    );
}

export default TemplateXL;