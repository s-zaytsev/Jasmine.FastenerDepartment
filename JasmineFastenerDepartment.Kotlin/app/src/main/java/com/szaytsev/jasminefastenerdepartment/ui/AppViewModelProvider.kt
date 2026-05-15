package com.szaytsev.jasminefastenerdepartment.ui

import androidx.lifecycle.ViewModelProvider.AndroidViewModelFactory.Companion.APPLICATION_KEY
import androidx.lifecycle.createSavedStateHandle
import androidx.lifecycle.viewmodel.CreationExtras
import androidx.lifecycle.viewmodel.initializer
import androidx.lifecycle.viewmodel.viewModelFactory
import com.szaytsev.jasminefastenerdepartment.JasmineApplication
import com.szaytsev.jasminefastenerdepartment.ui.order.OrderViewModel
import com.szaytsev.jasminefastenerdepartment.ui.product.ProductDetailsViewModel
import com.szaytsev.jasminefastenerdepartment.ui.product.ProductEditViewModel
import com.szaytsev.jasminefastenerdepartment.ui.product.ProductEntryViewModel
import com.szaytsev.jasminefastenerdepartment.ui.products.ProductsViewModel
import com.szaytsev.jasminefastenerdepartment.ui.settings.SettingsViewModel
import com.szaytsev.jasminefastenerdepartment.ui.synchronization.SynchronizationViewModel

object AppViewModelProvider {
    val Factory = viewModelFactory {
        initializer {
            ProductsViewModel(jasmineApplication().container.productsRepository)
        }

        initializer {
            ProductEntryViewModel(jasmineApplication().container.productsRepository)
        }

        initializer {
            ProductDetailsViewModel(
                this.createSavedStateHandle(),
                jasmineApplication().container.productsRepository
            )
        }

        initializer {
            ProductEditViewModel(
                this.createSavedStateHandle(),
                jasmineApplication().container.productsRepository
            )
        }

        initializer {
            OrderViewModel(jasmineApplication().container.productsRepository)
        }

        initializer {
            SettingsViewModel(
                jasmineApplication().container.remoteProductsRepository
            )
        }

        initializer {
            SynchronizationViewModel(
                jasmineApplication().container.productsRepository,
                jasmineApplication().container.remoteProductsRepository,
                jasmineApplication().container.userPreferencesRepository
            )
        }
    }
}

fun CreationExtras.jasmineApplication(): JasmineApplication =
    (this[APPLICATION_KEY] as JasmineApplication)
