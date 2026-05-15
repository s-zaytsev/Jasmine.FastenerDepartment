import {type Product, shortMeasurementUnitName} from "../../user/models/productModel.ts";

type ProductPriceProps = {
    product: Product;
}

const ProductPrice = (props: ProductPriceProps) => {

    return (
        <>
            {
                props.product.price.toLocaleString(
                    'ru-Ru', {minimumFractionDigits: 2})} руб./{shortMeasurementUnitName(props.product.measurementUnitCode)
            }.
        </>);
}

export default ProductPrice;