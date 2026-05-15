package com.szaytsev.jasminefastenerdepartment.ui.synchronization

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.szaytsev.jasminefastenerdepartment.data.models.Product
import com.szaytsev.jasminefastenerdepartment.data.repositories.ProductsRepository
import com.szaytsev.jasminefastenerdepartment.data.repositories.RemoteProductsRepository
import com.szaytsev.jasminefastenerdepartment.data.repositories.UserPreferencesRepository
import com.szaytsev.jasminefastenerdepartment.network.models.SynchronizationRequest
import com.szaytsev.jasminefastenerdepartment.network.models.SynchronizationResponse
import com.szaytsev.jasminefastenerdepartment.ui.models.SynchronizationUiState
import com.szaytsev.jasminefastenerdepartment.ui.models.SynchronizeStatus
import com.szaytsev.jasminefastenerdepartment.ui.toLocalDateTimeOrNull
import com.szaytsev.jasminefastenerdepartment.ui.toProduct
import com.szaytsev.jasminefastenerdepartment.ui.toSynchronizationProduct
import com.szaytsev.jasminefastenerdepartment.ui.toUtc
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.launch
import java.time.ZonedDateTime

class SynchronizationViewModel(
    private val productsRepository: ProductsRepository,
    private val remoteProductsRepository: RemoteProductsRepository,
    private val userPreferencesRepository: UserPreferencesRepository
) : ViewModel() {

    private var _uiState =
        MutableStateFlow(SynchronizationUiState(status = SynchronizeStatus.Pending))
    val uiState: StateFlow<SynchronizationUiState> = _uiState

    init {
        viewModelScope.launch {
            updateSynchronizedInfo()
        }
    }

    fun setPendingStatus() {
        viewModelScope.launch {
            _uiState.value = _uiState.value.copy(status = SynchronizeStatus.Pending)
            updateSynchronizedInfo()
        }
    }

    fun synchronize() {
        val request =
            SynchronizationRequest(
                lastSynchronizeUtcTime = uiState.value.lastSynchronizeTime.toLocalDateTimeOrNull(),
                products = uiState.value.products.map { it.toSynchronizationProduct() }
            )

        _uiState.value = _uiState.value.copy(status = SynchronizeStatus.Loading)

        viewModelScope.launch {
            var response: SynchronizationResponse

            try {
                response = remoteProductsRepository.synchronize(request)
            } catch (ex: Exception) {
                _uiState.value = _uiState.value.copy(
                    status = SynchronizeStatus.Error,
                    errorMessage = ex.message
                )
                return@launch
            }

            if (response.newProducts.isNotEmpty()) {
                val newProducts = mutableListOf<Product>()
                response.newProducts.forEach {
                    newProducts.add(it.toProduct())
                }
                productsRepository.insertManyProducts(newProducts)
            }

            response.modifiedProducts.forEach {
                var product = productsRepository.getProductStream(it.id).first()
                if (product != null) {
                    product = it.toProduct()
                    productsRepository.updateProduct(product)
                }
            }

            var successMessage = "";

            val lastSyncTime = ZonedDateTime.now().toUtc()
            userPreferencesRepository.saveLastSynchronizedTimePreference(
                lastSyncTime
            )

            if (uiState.value.lastSynchronizeTime == null) {
                successMessage = "Добавлено товаров: ${response.newProducts.size}"
            }

            _uiState.value = _uiState.value.copy(
                lastSynchronizeTime = lastSyncTime,
                status = SynchronizeStatus.Success,
                historyItems = response.historyEntries,
                successMessage = successMessage
            )
        }
    }

    private suspend fun updateSynchronizedInfo() {
        productsRepository.getAllProductsStream()
            .collect {
                val timeStr = userPreferencesRepository.getLastSynchronizeTime()
                val time = if (timeStr != null) {
                    ZonedDateTime.parse(timeStr)
                } else {
                    null
                }

                val products = if (time != null) {
                    it.filter { x ->
                        x.modifiedDate > time
                    }
                } else {
                    emptyList()
                }

                _uiState.value = _uiState.value.copy(
                    products = products,
                    lastSynchronizeTime = time
                )
            }
    }
}