package com.szaytsev.jasminefastenerdepartment.ui.shared

import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import com.szaytsev.jasminefastenerdepartment.data.models.ProductChangeReasonCode
import com.szaytsev.jasminefastenerdepartment.data.models.ProductHistoryEntry
import com.szaytsev.jasminefastenerdepartment.data.models.ProductMeasurementUnitCode
import com.szaytsev.jasminefastenerdepartment.data.models.ProductPriceTagCode

@Composable
fun ProductChangeReason(historyEntry: ProductHistoryEntry) {
    val code = historyEntry.changeReasonCode;

    if (code === ProductChangeReasonCode.Created) {
        Text("Товар создан")
    } else if (code === ProductChangeReasonCode.ChangedNumber) {
        Text("Артикул изменен с ${historyEntry.oldValue} на ${historyEntry.newValue}")
    } else if (code === ProductChangeReasonCode.ChangedName) {
        Text("Наименование изменено с \"${historyEntry.oldValue}\" на \"${historyEntry.newValue}\"")
    } else if (code === ProductChangeReasonCode.ChangedPrice) {
        Text("Цена изменена с ${historyEntry.oldValue} на ${historyEntry.newValue}")
    } else if (code === ProductChangeReasonCode.ChangedPriceTagCode) {
        Text(
            "Размер ценника изменен с ${ProductPriceTagCode.fromString(historyEntry.oldValue)} на ${
                ProductPriceTagCode.fromString(
                    historyEntry.newValue
                )
            }"
        )
    } else if (code === ProductChangeReasonCode.ChangedMeasurementUnitCode) {
        Text(
            "Единица измерения изменена с ${ProductMeasurementUnitCode.fromString(historyEntry.oldValue)} на ${
                ProductMeasurementUnitCode.fromString(
                    historyEntry.newValue
                )
            }"
        )
    } else if (code === ProductChangeReasonCode.ChangedIsNeededToOrder) {
        Text(
            if (historyEntry.newValue.lowercase() == "true") {
                "Добавлен в список заказа"

            } else {
                "Удален из списка заказа"
            }
        )
    } else if (code === ProductChangeReasonCode.ChangedIsNeededToPrint) {
        Text(
            if (historyEntry.newValue.lowercase() == "true") {
                "Добавлен в очередь печати"

            } else {
                "Удален из очереди печати"
            }
        )
    } else if (code === ProductChangeReasonCode.Deleted) {
        Text("Удален")
    } else if (code === ProductChangeReasonCode.Recovered) {
        Text("Восстановлен")
    } else {
        Text("Неизвестная причина изменения товара")
    }
}