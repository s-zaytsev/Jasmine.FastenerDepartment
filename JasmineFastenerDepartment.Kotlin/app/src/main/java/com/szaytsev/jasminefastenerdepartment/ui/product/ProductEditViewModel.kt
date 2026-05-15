package com.szaytsev.jasminefastenerdepartment.ui.product

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.szaytsev.jasminefastenerdepartment.data.repositories.ProductsRepository
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiModel
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiState
import com.szaytsev.jasminefastenerdepartment.ui.toProduct
import com.szaytsev.jasminefastenerdepartment.ui.toProductUiState
import com.szaytsev.jasminefastenerdepartment.ui.toUtc
import kotlinx.coroutines.flow.filterNotNull
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.launch
import java.time.ZonedDateTime

class ProductEditViewModel(
    savedStateHandle: SavedStateHandle,
    private val productsRepository: ProductsRepository
) : ViewModel() {

    var productUiState by mutableStateOf(ProductUiState())
        private set

    private val productId: String =
        checkNotNull(savedStateHandle[ProductEditDestination.productIdArg]).toString()

    init {
        viewModelScope.launch {
            productUiState = productsRepository.getProductStream(productId)
                .filterNotNull()
                .first()
                .toProductUiState(true)
        }
    }

    suspend fun updateItem() {
        if (validateInput(productUiState.productUiModel)) {
            val product = productUiState.productUiModel.toProduct()
                .copy(modifiedDate = ZonedDateTime.now().toUtc())
            productsRepository.updateProduct(product)
        }
    }

    fun updateUiState(productUiModel: ProductUiModel) {
        productUiState =
            ProductUiState(
                productUiModel = productUiModel,
                isEntryValid = validateInput(productUiModel)
            )
    }

    private fun validateInput(uiState: ProductUiModel = productUiState.productUiModel): Boolean {
        return with(uiState) {
            name.isNotBlank() && price > 0 && number > 0
        }
    }
}