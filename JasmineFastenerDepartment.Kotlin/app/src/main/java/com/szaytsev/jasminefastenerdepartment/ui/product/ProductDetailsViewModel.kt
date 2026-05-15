package com.szaytsev.jasminefastenerdepartment.ui.product

import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.szaytsev.jasminefastenerdepartment.data.repositories.ProductsRepository
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductDetailsUiState
import com.szaytsev.jasminefastenerdepartment.ui.toProduct
import com.szaytsev.jasminefastenerdepartment.ui.toProductDetails
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.filterNotNull
import kotlinx.coroutines.flow.map
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch
import java.time.ZoneId
import java.time.ZonedDateTime

class ProductDetailsViewModel(
    savedStateHandle: SavedStateHandle,
    private val productsRepository: ProductsRepository
) : ViewModel() {

    private val productId: String =
        checkNotNull(savedStateHandle[ProductDetailsDestination.productIdArg]).toString()

    val uiState: StateFlow<ProductDetailsUiState> =
        productsRepository.getProductStream(productId)
            .filterNotNull()
            .map {
                ProductDetailsUiState(
                    isDeleted = !it.isDeleted,
                    productUiModel = it.toProductDetails()
                )
            }.stateIn(
                scope = viewModelScope,
                started = SharingStarted.WhileSubscribed(TIMEOUT_MILLIS),
                initialValue = ProductDetailsUiState()
            )

    suspend fun changeOrderState() {
        viewModelScope.launch {
            val currentProduct = uiState.value.productUiModel.toProduct()
            productsRepository.updateProduct(
                currentProduct.copy(
                    isNeededToOrder = !currentProduct.isNeededToOrder,
                    modifiedDate = ZonedDateTime.now().toInstant().atZone(
                        ZoneId.of("Etc/UTC")
                    )
                )
            )
        }
    }

    suspend fun deleteItem() {
        viewModelScope.launch {
            val currentProduct = uiState.value.productUiModel.toProduct()
            productsRepository.updateProduct(
                currentProduct.copy(
                    isDeleted = true,
                    modifiedDate = ZonedDateTime.now().toInstant().atZone(
                        ZoneId.of("Etc/UTC")
                    )
                )
            )
        }
    }

    companion object {
        private const val TIMEOUT_MILLIS = 5_000L
    }
}
