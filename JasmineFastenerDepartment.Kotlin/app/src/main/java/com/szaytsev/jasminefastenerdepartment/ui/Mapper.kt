package com.szaytsev.jasminefastenerdepartment.ui

import com.szaytsev.jasminefastenerdepartment.data.models.Product
import com.szaytsev.jasminefastenerdepartment.network.models.SynchronizationProduct
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiModel
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiState
import java.time.ZoneId
import java.time.ZoneOffset
import java.time.ZonedDateTime


fun ProductUiModel.toProduct(): Product = Product(
    id = id,
    number = number,
    name = name,
    price = price,
    priceTagCode = priceTagCode,
    isNeededToOrder = isNeededToOrder,
    isNeededToPrint = isNeededToPrint,
    isDeleted = isDeleted,
    createdDate = createUtcTime,
    modifiedDate = updateUtcTime,
    measurementUnitCode = measurementUnitCode
)

fun Product.toProductDetails(): ProductUiModel = ProductUiModel(
    id = id,
    number = number,
    name = name,
    price = price,
    priceTagCode = priceTagCode,
    measurementUnitCode = measurementUnitCode,
    isNeededToOrder = isNeededToOrder,
    isNeededToPrint = isNeededToPrint,
    isDeleted = isDeleted,
    createUtcTime = createdDate,
    updateUtcTime = modifiedDate
)

fun Product.toProductUiState(isEntryValid: Boolean = false): ProductUiState = ProductUiState(
    productUiModel = this.toProductDetails(),
    isEntryValid = isEntryValid
)

fun SynchronizationProduct.toProduct(): Product = Product(
    id = id,
    number = number,
    name = name,
    price = price,
    isNeededToOrder = isNeededToOrder,
    isNeededToPrint = isNeededToPrint,
    isDeleted = isDeleted,
    priceTagCode = priceTagCode,
    measurementUnitCode = measurementUnitCode,
    createdDate = createdDate,
    modifiedDate = modifiedDate
)

fun Product.toSynchronizationProduct(): SynchronizationProduct = SynchronizationProduct(
    id = id,
    number = number,
    name = name,
    price = price,
    isNeededToOrder = isNeededToOrder,
    isNeededToPrint = isNeededToPrint,
    isDeleted = isDeleted,
    priceTagCode = priceTagCode,
    measurementUnitCode = measurementUnitCode,
    createdDate = createdDate,
    modifiedDate = modifiedDate
)

fun ZonedDateTime.toLocalDateTime(): ZonedDateTime {
    return this.withZoneSameInstant(ZoneId.systemDefault())
}

fun ZonedDateTime.toUtc(): ZonedDateTime {
    return this.withZoneSameInstant(ZoneOffset.UTC)
}

fun ZonedDateTime?.toLocalDateTimeOrNull(): ZonedDateTime? {
    return this?.withZoneSameInstant(ZoneId.systemDefault())
}

fun ZonedDateTime?.toUtcOrDefault(): ZonedDateTime? {
    return this?.withZoneSameInstant(ZoneOffset.UTC)
}
