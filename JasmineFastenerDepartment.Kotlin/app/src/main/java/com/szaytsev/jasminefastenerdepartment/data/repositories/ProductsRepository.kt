package com.szaytsev.jasminefastenerdepartment.data.repositories

import com.szaytsev.jasminefastenerdepartment.data.models.Product
import kotlinx.coroutines.flow.Flow
import java.time.ZonedDateTime

interface ProductsRepository {
    fun getAllProductsStream(): Flow<List<Product>>

    fun getProductsToOrderStream(): Flow<List<Product>>

    fun getProductStream(id: String): Flow<Product?>

    fun getLastModifiedProducts(date: ZonedDateTime): Flow<List<Product>>

    fun getProductsByName(filter: String): Flow<List<Product>>

    suspend fun insertProduct(product: Product)

    suspend fun insertManyProducts(products: List<Product>)

    suspend fun deleteProduct(product: Product)

    suspend fun updateProduct(product: Product)
    suspend fun getLastNumber(): Int?
}