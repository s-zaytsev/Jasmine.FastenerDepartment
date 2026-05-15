package com.szaytsev.jasminefastenerdepartment.data.repositories

import com.szaytsev.jasminefastenerdepartment.network.JasmineApiService
import com.szaytsev.jasminefastenerdepartment.network.models.SynchronizationRequest
import com.szaytsev.jasminefastenerdepartment.network.models.SynchronizationResponse

interface RemoteProductsRepository {
    suspend fun testConnection() : Boolean
    suspend fun synchronize(request: SynchronizationRequest): SynchronizationResponse
}

class NetworkProductsRepository(private val jasmineApiService: JasmineApiService) :
    RemoteProductsRepository {
    override suspend fun testConnection() : Boolean {
        return try {
            jasmineApiService.testConnection()
            true
        } catch (ex: Exception) {
            false
        }
    }

    override suspend fun synchronize(request: SynchronizationRequest): SynchronizationResponse {
        try {
            val response = jasmineApiService.synchronize(request)
            return response
        } catch (ex: Exception) {
            throw ex
        }
    }
}