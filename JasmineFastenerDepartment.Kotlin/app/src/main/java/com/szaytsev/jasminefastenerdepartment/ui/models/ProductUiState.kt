package com.szaytsev.jasminefastenerdepartment.ui.models

data class ProductUiState(
    val productUiModel: ProductUiModel = ProductUiModel(),
    val isEntryValid: Boolean = false
)