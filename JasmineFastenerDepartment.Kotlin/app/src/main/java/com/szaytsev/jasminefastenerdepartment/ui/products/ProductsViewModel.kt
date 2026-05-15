package com.szaytsev.jasminefastenerdepartment.ui.products

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.szaytsev.jasminefastenerdepartment.data.repositories.ProductsRepository
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductsUiState
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class ProductsViewModel(private val productsRepository: ProductsRepository) : ViewModel() {
    val productsUiState = MutableStateFlow(ProductsUiState())
    var filterState by mutableStateOf("")

    init {
        viewModelScope.launch {
            loadProducts(productsUiState.value.filter)
        }
    }

    fun filter(filter: String) {
        filterState = filter
        viewModelScope.launch {
            loadProducts(filter)
        }
    }

    private suspend fun loadProducts(filter: String) {
        productsUiState.update {
            ProductsUiState(
                isLoading = true
            )
        }
        if (filter.isBlank()) {
            productsRepository.getAllProductsStream().collect { products ->
                productsUiState.update {
                    ProductsUiState(
                        productList = products.filter { !it.isDeleted },
                        filter = ""
                    )
                }
            }
        } else {
            productsRepository.getProductsByName(filter).collect { products ->
                productsUiState.update {
                    ProductsUiState(
                        productList = products.filter { !it.isDeleted },
                        filter = filter
                    )
                }
            }
        }
        productsUiState.update {
            ProductsUiState(
                isLoading = false
            )
        }
    }
}