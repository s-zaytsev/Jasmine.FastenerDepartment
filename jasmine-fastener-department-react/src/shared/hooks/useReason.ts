import {ProductChangeReasonCode} from "../../user/models/productModel.ts";

const UseReason = () => {

    const getReasonName = (reasonCode: ProductChangeReasonCode) => {
        switch (reasonCode) {
            case ProductChangeReasonCode.changedName:
                return 'Изменение названия';
            case ProductChangeReasonCode.created:
                return 'Создание';
            case ProductChangeReasonCode.changedNumber:
                return 'Изменение артикула';
            case ProductChangeReasonCode.changedPriceTagCode:
                return 'Изменение размера ценника';
            case ProductChangeReasonCode.changedType:
                return 'Изменение типа';
            case ProductChangeReasonCode.changedPrice:
                return 'Изменение цены';
            case ProductChangeReasonCode.changedMeasurementUnitCode:
                return 'Изменение единицы измерения';
            case ProductChangeReasonCode.changedIsNeededToPrint:
                return 'Изменение статуса печати';
            case ProductChangeReasonCode.changedIsNeededToOrder:
                return 'Изменение статуса заказа';
            case ProductChangeReasonCode.deleted:
                return 'Удаление';
            case ProductChangeReasonCode.recovered:
                return 'Восстановление';
            default:
                return 'Неизвестная причина';
        }
    }

    return {
        getReasonName
    }
}

export default UseReason;