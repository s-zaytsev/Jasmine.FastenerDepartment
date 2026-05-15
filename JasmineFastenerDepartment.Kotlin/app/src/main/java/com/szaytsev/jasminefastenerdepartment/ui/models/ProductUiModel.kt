package com.szaytsev.jasminefastenerdepartment.ui.models

import com.szaytsev.jasminefastenerdepartment.data.models.ProductMeasurementUnitCode
import com.szaytsev.jasminefastenerdepartment.data.models.ProductPriceTagCode
import com.szaytsev.jasminefastenerdepartment.ui.toUtc
import java.time.ZonedDateTime

data class ProductUiModel(
    val id: String = "",
    val number: Int = 0,
    val name: String = "",
    val price: Double = 0.00,
    val priceTagCode: ProductPriceTagCode = ProductPriceTagCode.M,
    val measurementUnitCode: ProductMeasurementUnitCode = ProductMeasurementUnitCode.Pieces,
    val isNeededToOrder: Boolean = false,
    val isNeededToPrint: Boolean = false,
    val isDeleted: Boolean = false,
    val createUtcTime: ZonedDateTime = ZonedDateTime.now().toUtc(),
    val updateUtcTime: ZonedDateTime = ZonedDateTime.now().toUtc()
)
