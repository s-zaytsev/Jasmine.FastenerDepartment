package com.szaytsev.jasminefastenerdepartment.data.repositories

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.TypeConverters
import androidx.room.Update
import com.szaytsev.jasminefastenerdepartment.data.converters.ZonedDateTimeConverter
import com.szaytsev.jasminefastenerdepartment.data.models.Product
import kotlinx.coroutines.flow.Flow
import java.time.ZonedDateTime

@Dao
interface ProductDao {
    @Insert(onConflict = OnConflictStrategy.IGNORE)
    suspend fun insert(product: Product)

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    @JvmSuppressWildcards
    suspend fun insertMany(objects: List<Product>)

    @Update
    suspend fun update(product: Product)

    @Delete
    suspend fun delete(product: Product)

    @Query("SELECT * from products WHERE id = :id")
    fun getItem(id: String): Flow<Product>

    @Query("SELECT * from products ORDER BY name ASC")
    fun getAllItems(): Flow<List<Product>>

    @Query("SELECT * FROM products WHERE isNeededToOrder = 1 AND isDeleted = 0 ORDER BY name ASC")
    fun getProductToOrder(): Flow<List<Product>>

    @Query("SELECT * from products WHERE modifiedDate > :date")
    @TypeConverters(ZonedDateTimeConverter::class)
    fun getModifiedProductsFromDate(date: ZonedDateTime) : Flow<List<Product>>

    @Query("SELECT * FROM products WHERE name LIKE '%' || :filter || '%'")
    fun getProductsByName(filter: String) : Flow<List<Product>>

    @Query("SELECT * FROM products ORDER BY number DESC LIMIT 1")
    fun getLastProduct(): Flow<Product?>
}