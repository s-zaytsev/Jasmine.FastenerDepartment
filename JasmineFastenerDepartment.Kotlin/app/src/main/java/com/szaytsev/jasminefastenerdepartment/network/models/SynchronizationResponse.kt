package com.szaytsev.jasminefastenerdepartment.network.models

import kotlinx.serialization.Serializable

@Serializable
data class SynchronizationResponse(
    val newProducts: List<SynchronizationProduct>,
    val modifiedProducts: List<SynchronizationProduct>,
    val historyEntries: List<SynchronizationHistoryEntry>
)