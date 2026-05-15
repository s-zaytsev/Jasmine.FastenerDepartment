package com.szaytsev.jasminefastenerdepartment.ui.order

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.szaytsev.jasminefastenerdepartment.data.repositories.ProductsRepository
import com.szaytsev.jasminefastenerdepartment.ui.models.OrderUiState
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class OrderViewModel(private val productsRepository: ProductsRepository) : ViewModel() {
    var filterState by mutableStateOf("")
    val orderUiState = MutableStateFlow(OrderUiState())

    init {
        viewModelScope.launch {
            productsRepository.getProductsToOrderStream().collect { products ->
                orderUiState.value =
                    OrderUiState(productList = products.filter { !it.isDeleted })
            }
        }
    }

    fun filter(filter: String) {
        filterState = filter
        viewModelScope.launch {
            if (filter.isBlank()) {
                productsRepository.getProductsToOrderStream().collect { products ->
                    orderUiState.update {
                        OrderUiState(
                            productList = products.filter { !it.isDeleted }
                        )
                    }
                }
            } else {
                productsRepository.getProductsToOrderStream().collect { products ->
                    orderUiState.update {
                        OrderUiState(
                            productList = products.filter {
                                !it.isDeleted && it.name.lowercase().contains(filter.lowercase())
                            },
                        )
                    }
                }
            }
        }
    }
}