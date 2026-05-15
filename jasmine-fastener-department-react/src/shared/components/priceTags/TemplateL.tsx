import type {Product} from "../../../user/models/productModel.ts";
import PriceTagProductName from "./PriceTagProductName.tsx";
import ProductPrice from "../ProductPrice.tsx";

type TemplateLProps = {
    product: Product;
}

const TemplateL = (props: TemplateLProps) => {
    return (
        <div style={{
            width: "211.8px",
            fontSize: "14px",
            height: "64px",
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
            <div style={{display: "flex", justifyContent: "space-around", alignItems: "center"}}>
                <p style={{fontSize: "14px", margin: "2px"}}>
                    <strong>
                        <ProductPrice product={props.product}/>
                    </strong>
                </p>
                <p style={{fontSize: "14px", margin: "2px"}}><strong>{props.product.number}</strong></p>
            </div>
        </div>);
}

export default TemplateL;