package com.szaytsev.jasminefastenerdepartment.data.models

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
enum class ProductPriceTagCode {
    @SerialName(value = "0")
    Unknown,

    @SerialName(value = "1")
    S,

    @SerialName(value = "2")
    SM,

    @SerialName(value = "3")
    L,

    @SerialName(value = "4")
    M,

    @SerialName(value = "5")
    XL;

    companion object {
        fun fromString(value: String) =
            entries.firstOrNull { it.ordinal.toString() == value }
                ?: throw Exception("Unexcepted price tag - $value")
    }
}