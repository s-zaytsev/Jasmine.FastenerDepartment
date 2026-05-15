package com.szaytsev.jasminefastenerdepartment.ui.models

import com.szaytsev.jasminefastenerdepartment.data.models.Product

data class OrderUiState(
    val productList: List<Product> = listOf()
)