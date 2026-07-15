package com.szaytsev.jasminefastenerdepartment.data.models

import com.szaytsev.jasminefastenerdepartment.data.serializers.ZoneDateTimeSerializer
import kotlinx.serialization.Serializable
import java.time.ZonedDateTime

@Serializable
data class ProductHistoryEntry(
    val id: String,
    val productId: String,
    @Serializable(with = ZoneDateTimeSerializer::class)
    val createdDate: ZonedDateTime,
    val reason: ProductChangeReason,
    val oldValue: String,
    val newValue: String,
    val productNumber: Int
)
