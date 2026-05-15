import type {Product} from "../../../user/models/productModel.ts";
import PriceTagProductName from "./PriceTagProductName.tsx";
import ProductPrice from "../ProductPrice.tsx";

type TemplateSProps = {
    product: Product;
}

const TemplateS = (props: TemplateSProps) => {
    return (
        <>
            <div style={{
                width: "100.8px",
                fontSize: "10px",
                color: "#000000",
                height: "64px",
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
                <p style={{fontSize: '14px', margin: "0"}}><strong>{<ProductPrice product={props.product}/>}</strong></p>
                <p style={{margin: "0"}}><strong>{props.product.number}</strong></p>
            </div>
        </>
    );
}

export default TemplateS;