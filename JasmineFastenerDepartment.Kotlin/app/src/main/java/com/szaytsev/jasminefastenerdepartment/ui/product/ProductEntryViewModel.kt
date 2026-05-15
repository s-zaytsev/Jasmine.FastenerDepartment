package com.szaytsev.jasminefastenerdepartment.ui.product

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.szaytsev.jasminefastenerdepartment.data.repositories.ProductsRepository
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiModel
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiState
import com.szaytsev.jasminefastenerdepartment.ui.toProduct
import com.szaytsev.jasminefastenerdepartment.ui.toUtc
import kotlinx.coroutines.launch
import java.time.ZonedDateTime
import java.util.UUID
import kotlin.uuid.Uuid

class ProductEntryViewModel(
    private val productsRepository: ProductsRepository
) : ViewModel() {

    var lastNumber: Int = 0

    var productUiState by mutableStateOf(ProductUiState())
        private set

    init {
        viewModelScope.launch {
            var number = productsRepository.getLastNumber()

            if (number == null) {
                number = 0;
            }

            lastNumber = number + 1
            productUiState =
                ProductUiState(productUiModel = ProductUiModel(number = lastNumber))

        }
    }

    fun updateUiState(productUiModel: ProductUiModel) {
        productUiState =
            ProductUiState(
                productUiModel = productUiModel,
                isEntryValid = validateInput(productUiModel)
            )
    }

    suspend fun saveItem() {
        if (validateInput()) {
            val product = productUiState.productUiModel.toProduct().copy(
                id = UUID.randomUUID().toString(),
                createdDate = ZonedDateTime.now().toUtc(),
                modifiedDate = ZonedDateTime.now().toUtc()
            )
            productsRepository.insertProduct(product)
        }
    }

    private fun validateInput(uiState: ProductUiModel = productUiState.productUiModel): Boolean {
        return with(uiState) {
            name.isNotBlank() && price > 0 && number > 0
        }
    }
}