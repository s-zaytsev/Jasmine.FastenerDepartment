package com.szaytsev.jasminefastenerdepartment.ui.settings

import androidx.lifecycle.ViewModel
import com.szaytsev.jasminefastenerdepartment.data.repositories.RemoteProductsRepository

class SettingsViewModel(
    private val remoteProductsRepository: RemoteProductsRepository
) : ViewModel() {

    suspend fun checkConnection(): Boolean {
        return remoteProductsRepository.testConnection()
    }
}