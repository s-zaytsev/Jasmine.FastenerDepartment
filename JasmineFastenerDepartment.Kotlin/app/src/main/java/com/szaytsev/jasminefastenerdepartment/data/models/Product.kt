package com.szaytsev.jasminefastenerdepartment.data.models

import androidx.room.Entity
import androidx.room.PrimaryKey
import androidx.room.TypeConverters
import com.szaytsev.jasminefastenerdepartment.data.converters.ZonedDateTimeConverter
import com.szaytsev.jasminefastenerdepartment.data.serializers.PriceTagCodeSerializer
import com.szaytsev.jasminefastenerdepartment.data.serializers.ProductMeasurementUnitCodeSerializer
import com.szaytsev.jasminefastenerdepartment.data.serializers.ZoneDateTimeSerializer
import kotlinx.serialization.Serializable
import java.time.ZonedDateTime

@TypeConverters(ZonedDateTimeConverter::class)
@Entity(tableName = "products")
@Serializable
data class Product(
    @PrimaryKey
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
