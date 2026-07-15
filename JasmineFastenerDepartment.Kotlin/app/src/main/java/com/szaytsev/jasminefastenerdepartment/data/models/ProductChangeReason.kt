package com.szaytsev.jasminefastenerdepartment.data.models

import com.szaytsev.jasminefastenerdepartment.data.serializers.ProductChangeReasonCodeSerializer
import kotlinx.serialization.Serializable

@Serializable
data class ProductChangeReason(
    @Serializable(ProductChangeReasonCodeSerializer::class)
    val code: ProductChangeReasonCode,
    val description: String
)