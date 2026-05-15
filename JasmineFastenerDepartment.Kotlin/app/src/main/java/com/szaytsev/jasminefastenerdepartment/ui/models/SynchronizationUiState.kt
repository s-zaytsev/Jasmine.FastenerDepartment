package com.szaytsev.jasminefastenerdepartment.ui.models

import com.szaytsev.jasminefastenerdepartment.data.models.Product
import com.szaytsev.jasminefastenerdepartment.network.models.SynchronizationHistoryEntry
import java.time.ZonedDateTime

data class SynchronizationUiState(
    val lastSynchronizeTime: ZonedDateTime? = null,
    val products: List<Product> = emptyList(),
    val successMessage: String = "",
    val errorMessage: String? = "",
    val status: SynchronizeStatus = SynchronizeStatus.Pending,
    val historyItems: List<SynchronizationHistoryEntry> = emptyList()
)
