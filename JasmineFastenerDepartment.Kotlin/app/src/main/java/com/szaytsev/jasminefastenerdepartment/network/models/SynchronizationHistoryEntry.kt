package com.szaytsev.jasminefastenerdepartment.network.models

import com.szaytsev.jasminefastenerdepartment.data.serializers.ZoneDateTimeSerializer
import kotlinx.serialization.Serializable
import java.time.ZonedDateTime

@Serializable
data class SynchronizationHistoryEntry(
    @Serializable(with = ZoneDateTimeSerializer::class)
    val date: ZonedDateTime,
    val historyItems: List<SynchronizationHistoryEntryItem>
)