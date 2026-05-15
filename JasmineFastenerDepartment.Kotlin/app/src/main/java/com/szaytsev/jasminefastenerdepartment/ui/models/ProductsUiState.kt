package com.szaytsev.jasminefastenerdepartment.ui.models

import com.szaytsev.jasminefastenerdepartment.data.models.Product

data class ProductsUiState(
    val productList: List<Product> = listOf(),
    var filter: String = "",
    val isLoading: Boolean = false
)