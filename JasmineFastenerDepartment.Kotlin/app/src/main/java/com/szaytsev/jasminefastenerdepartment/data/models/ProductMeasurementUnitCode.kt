package com.szaytsev.jasminefastenerdepartment.data.models

import com.szaytsev.jasminefastenerdepartment.R
import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
enum class ProductMeasurementUnitCode {

    @SerialName(value = "0")
    Unknown,

    @SerialName(value = "1")
    Pieces,

    @SerialName(value = "2")
    Meters,

    @SerialName(value = "3")
    Kilograms,

    @SerialName(value = "4")
    Packages,

    @SerialName(value = "5")
    Sets,

    @SerialName(value = "6")
    Lists;

    companion object {
        fun fromString(value: String) =
            entries.firstOrNull { it.ordinal.toString() == value }
                ?: throw Exception("Unexcepted measurement unit - $value")

        fun getDisplayNameResourceId(code: ProductMeasurementUnitCode): Int {
            if (code == Pieces) {
                return R.string.pieces
            }
            if (code == Meters) {
                return R.string.meters
            }
            if (code == Kilograms) {
                return R.string.kilograms
            }
            if (code == Packages) {
                return R.string.packages
            }
            if (code == Sets) {
                return R.string.sets
            }
            if (code == Lists) {
                return R.string.lists
            }
            return R.string.unknown_measurement_unit
        }

        fun getShortDisplayNameResourceId(code: ProductMeasurementUnitCode): Int {
            if (code == Pieces) {
                return R.string.short_pieces
            }
            if (code == Meters) {
                return R.string.short_meters
            }
            if (code == Kilograms) {
                return R.string.short_kilograms
            }
            if (code == Packages) {
                return R.string.short_packages
            }
            if (code == Sets) {
                return R.string.short_sets
            }
            if (code == Lists) {
                return R.string.short_lists
            }
            return R.string.unknown_measurement_unit
        }
    }
}