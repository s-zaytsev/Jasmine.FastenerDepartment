import type {Quantity} from "../models/models.ts";
import {shortMeasurementUnitName} from "../../user/models/productModel.ts";

const useQuantity = () => {
    const getText = (quantity?: Quantity) => {
        if (!quantity) return '0';

        if (quantity.measurementUnitCode) {
            return `${quantity.value} ${shortMeasurementUnitName(quantity.measurementUnitCode!)}`;
        }
        return `${quantity.value} ${quantity.specialMeasurementUnit}`;
    }

    return {
        getText
    };
}

export default useQuantity;