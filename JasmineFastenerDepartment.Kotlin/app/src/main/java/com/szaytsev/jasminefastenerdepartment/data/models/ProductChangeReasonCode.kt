package com.szaytsev.jasminefastenerdepartment.data.models

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
enum class ProductChangeReasonCode {

    @SerialName(value = "0")
    Unknown,

    @SerialName(value = "1")
    Created,

    @SerialName(value = "2")
    ChangedNumber,

    @SerialName(value = "3")
    ChangedName,

    @SerialName(value = "4")
    ChangedPrice,

    @SerialName(value = "5")
    ChangedPriceTagCode,

    @SerialName(value = "6")
    ChangedMeasurementUnitCode,

    @SerialName(value = "7")
    ChangedIsNeededToOrder,

    @SerialName(value = "8")
    ChangedIsNeededToPrint,

    @SerialName(value = "9")
    Deleted,

    @SerialName(value = "10")
    Recovered,

    @SerialName(value = "11")
    ChangedType;

    companion object {
        fun fromString(value: String) =
            ProductChangeReasonCode.entries.firstOrNull { it.ordinal.toString() == value }
                ?: throw Exception("Unexcepted change reason - $value")
    }
}