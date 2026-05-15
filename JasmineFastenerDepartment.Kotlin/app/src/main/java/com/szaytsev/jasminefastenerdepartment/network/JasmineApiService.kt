package com.szaytsev.jasminefastenerdepartment.network

import com.szaytsev.jasminefastenerdepartment.network.models.SynchronizationRequest
import com.szaytsev.jasminefastenerdepartment.network.models.SynchronizationResponse
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface JasmineApiService {
    @GET("health-check")
    suspend fun testConnection()

    @POST("synchronization")
    suspend fun synchronize(@Body request: SynchronizationRequest): SynchronizationResponse
}