package com.szaytsev.jasminefastenerdepartment.network.models

import com.szaytsev.jasminefastenerdepartment.data.models.ProductMeasurementUnitCode
import com.szaytsev.jasminefastenerdepartment.data.models.ProductPriceTagCode
import com.szaytsev.jasminefastenerdepartment.data.serializers.PriceTagCodeSerializer
import com.szaytsev.jasminefastenerdepartment.data.serializers.ProductMeasurementUnitCodeSerializer
import com.szaytsev.jasminefastenerdepartment.data.serializers.ZoneDateTimeSerializer
import kotlinx.serialization.Serializable
import java.time.ZonedDateTime

@Serializable
data class SynchronizationProduct(
    val id: String,
    val number: Int,
    val name: String,
    val price: Double,
    val isNeededToOrder: Boolean,
    val isNeededToPrint: Boolean,
    val isDeleted: Boolean,

    @Serializable(PriceTagCodeSerializer::class)
    val priceTagCode: ProductPriceTagCode,

    @Serializable(ProductMeasurementUnitCodeSerializer::class)
    val measurementUnitCode: ProductMeasurementUnitCode,

    @Serializable(with = ZoneDateTimeSerializer::class)
    val createdDate: ZonedDateTime,

    @Serializable(with = ZoneDateTimeSerializer::class)
    val modifiedDate: ZonedDateTime
)