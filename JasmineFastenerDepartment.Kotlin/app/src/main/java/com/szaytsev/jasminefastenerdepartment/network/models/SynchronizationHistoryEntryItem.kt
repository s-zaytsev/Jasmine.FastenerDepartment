package com.szaytsev.jasminefastenerdepartment.network.models

import com.szaytsev.jasminefastenerdepartment.data.models.ProductHistoryEntry
import kotlinx.serialization.Serializable

@Serializable
data class SynchronizationHistoryEntryItem(
    val productId: String,
    val productHistoryEntries: List<ProductHistoryEntry>
)