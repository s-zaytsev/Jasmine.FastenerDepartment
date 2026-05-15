package com.szaytsev.jasminefastenerdepartment.network.models

import com.szaytsev.jasminefastenerdepartment.data.serializers.ZoneDateTimeSerializer
import kotlinx.serialization.Serializable
import java.time.ZonedDateTime

@Serializable
data class SynchronizationRequest(
    @Serializable(with = ZoneDateTimeSerializer::class)
    val lastSynchronizeUtcTime: ZonedDateTime?,
    val products: List<SynchronizationProduct>
)