import {
    longMeasurementUnitName,
    PriceTagCode,
    ProductChangeReasonCode,
    type ProductHistoryEntry,
    ProductMeasurementUnitCode
} from "../../user/models/productModel.ts";
import type {ProductType} from "../../user/models/productTypeModels.ts";

const useHistory = () => {
    const getText = (history: ProductHistoryEntry, productTypes: ProductType[]) => {
        switch (history.changeReasonCode) {
            case ProductChangeReasonCode.created:
                return [
                    'Товар создан',
                    '',
                    ''];

            case ProductChangeReasonCode.changedNumber:
                return [
                    'Артикул изменен на ',
                    `${history.newValue}`,
                    `Предыдущее артикул: ${history.oldValue}`];

            case ProductChangeReasonCode.changedName:
                return [
                    'Название изменено на ',
                    `${history.newValue}`,
                    `Предыдущее название: ${history.oldValue}`];

            case ProductChangeReasonCode.changedPrice:
                return [
                    'Цена изменена на ',
                    `${history.newValue}`,
                    `Предыдущая цена: ${history.oldValue}`];

            case ProductChangeReasonCode.changedPriceTagCode:
                return [
                    'Размер ценника изменен на ',
                    `${PriceTagCode[+history.newValue]?.toUpperCase()}`,
                    `Предыдущий размер: ${PriceTagCode[+history.oldValue]?.toUpperCase()}`];

            case ProductChangeReasonCode.changedMeasurementUnitCode:
                return [
                    'Единица измерения изменена на ',
                    `${longMeasurementUnitName(
                        ProductMeasurementUnitCode[history.newValue.toString().toLowerCase() as keyof typeof ProductMeasurementUnitCode]
                    )}`,
                    `Предыдущая единица измерения: ${longMeasurementUnitName(ProductMeasurementUnitCode[history.oldValue.toString().toLowerCase() as keyof typeof ProductMeasurementUnitCode])}`];

            case ProductChangeReasonCode.changedIsNeededToOrder:
                return [
                    history.newValue.toString().toLowerCase() === 'true' ? 'Добавлен в список заказа' : 'Удален из списка заказа',
                    '',
                    ''];

            case ProductChangeReasonCode.changedIsNeededToPrint:
                return [
                    history.newValue.toString().toLowerCase() === 'true' ? 'Добавлен в очередь печати' : 'Удален из очереди печати',
                    '',
                    ''];

            case ProductChangeReasonCode.deleted:
                return [
                    'Удалён',
                    '',
                    ''];

            case ProductChangeReasonCode.recovered:
                return [
                    'Восстановлен',
                    '',
                    ''];

            case ProductChangeReasonCode.changedType:
                return [
                    'Тип товара изменен на ',
                    `${productTypes.find(x => x.id === history.newValue.toString())?.name}`,
                    `${productTypes.find(x => x.id === history.oldValue.toString())?.name || ''}`];

            default:
                return "Неизвестная причина изменения товара";
        }
    }

    return {
        getText
    }
}

export default useHistory;