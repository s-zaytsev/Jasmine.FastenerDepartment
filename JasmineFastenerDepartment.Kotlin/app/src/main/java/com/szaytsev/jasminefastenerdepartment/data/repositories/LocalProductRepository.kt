package com.szaytsev.jasminefastenerdepartment.data.repositories

import com.szaytsev.jasminefastenerdepartment.data.models.Product
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.first
import java.time.ZonedDateTime

class LocalProductRepository(private val productDao: ProductDao) : ProductsRepository {
    override fun getAllProductsStream(): Flow<List<Product>> {
        return productDao.getAllItems()
    }

    override fun getProductsToOrderStream(): Flow<List<Product>> {
        return productDao.getProductToOrder()
    }

    override fun getProductStream(id: String): Flow<Product?> {
        return productDao.getItem(id)
    }

    override fun getLastModifiedProducts(date: ZonedDateTime): Flow<List<Product>> {
        return productDao.getModifiedProductsFromDate(date)
    }

    override fun getProductsByName(filter: String): Flow<List<Product>> {
        return productDao.getProductsByName(filter)
    }

    override suspend fun insertProduct(product: Product) {
        productDao.insert(product)
    }

    override suspend fun insertManyProducts(products: List<Product>) {
        productDao.insertMany(products)
    }

    override suspend fun deleteProduct(product: Product) {
        productDao.delete(product)
    }

    override suspend fun updateProduct(product: Product) {
        productDao.update(product)
    }

    override suspend fun getLastNumber(): Int? {
        return productDao.getLastProduct().first()?.number
    }
}