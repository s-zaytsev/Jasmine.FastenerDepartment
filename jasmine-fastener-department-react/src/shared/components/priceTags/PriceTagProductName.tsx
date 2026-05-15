import {PriceTagCode, type Product} from "../../../user/models/productModel.ts";
import ApplicationRegexes from "../../expressions/applicationRegexes.ts";
import {Box} from "@mui/material";

type PriceTagProductNameProps = {
    product: Product;
}

const PriceTagProductName = (props: PriceTagProductNameProps) => {
    const Name = () => {
        let name = props.product.name;
        const hasSize = ApplicationRegexes.hasHardwareSize(name);
        if (!hasSize || !props.product.isHardwareSizeEnabled) {
            return DefaultName(name);
        }
        const size = ApplicationRegexes.getHardwareSize(name)!;
        name = name.replace(size[0], '').trim();
        return NameWithHardwareSize(name, size[0]);
    }

    const DefaultName = (name: string) => {
        switch (props.product.priceTagCode) {
            case PriceTagCode.s:
                return <p style={{fontSize: '10px', margin: "0"}}>{props.product.name}</p>;
            case PriceTagCode.l:
                return <p style={{margin: "2px"}}>{name}</p>;
            case PriceTagCode.m:
                return <p style={{fontSize: "14px"}}>{props.product.name}</p>;
            case PriceTagCode.xl:
                return <p style={{fontSize: "14px"}}>{props.product.name}</p>;

            default:
                return <p>Code isn't supported</p>
        }
    }

    const NameWithHardwareSize = (name: string, size: string) => {
        const fontSize = getFontSize(size);
        const marginTop = getMarginTop();
        return (
            <Box style={{margin: '0'}}>
                <p style={{margin: '0'}}>
                    {name}
                    <br/>
                    <strong>
                    <span
                        style={{
                            fontSize: `${fontSize}px`,
                            marginTop: `${marginTop}px`,
                            display: 'block'
                        }}>
                            {size.trim()}
                    </span>
                    </strong>
                </p>
            </Box>
        );
    }

    function getFontSize(hardwareSize: string): number {
        switch (props.product.priceTagCode) {
            case PriceTagCode.s:
                return 14;
            case PriceTagCode.l:
                return 14;
            case PriceTagCode.m:
                return hardwareSize.length > 16 ? 15 : 18;
            case PriceTagCode.xl:
                return hardwareSize.length > 16 ? 15 : 18;
            default:
                return 14;
        }
    }

    function getMarginTop(): number {
        switch (props.product.priceTagCode) {
            case PriceTagCode.s:
                return 2;
            case PriceTagCode.l:
                //return 5;
                return 2;
            case PriceTagCode.m:
                //return 10;
                return 2;
            case PriceTagCode.xl:
                //return 10;
                return 2;
            default:
                return 5;
        }
    }

    return (
        <>
            <Name/>
        </>);
}

export default PriceTagProductName;