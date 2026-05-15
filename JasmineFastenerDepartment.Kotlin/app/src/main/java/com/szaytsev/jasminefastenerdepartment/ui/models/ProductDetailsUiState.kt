package com.szaytsev.jasminefastenerdepartment.ui.models

data class ProductDetailsUiState(
    val isDeleted: Boolean = false,
    val productUiModel: ProductUiModel = ProductUiModel()
)