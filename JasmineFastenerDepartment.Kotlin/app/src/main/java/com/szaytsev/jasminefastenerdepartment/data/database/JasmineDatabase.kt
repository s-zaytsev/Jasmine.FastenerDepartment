package com.szaytsev.jasminefastenerdepartment.data.database

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import androidx.room.TypeConverters
import com.szaytsev.jasminefastenerdepartment.data.converters.ZonedDateTimeConverter
import com.szaytsev.jasminefastenerdepartment.data.models.Product
import com.szaytsev.jasminefastenerdepartment.data.repositories.ProductDao

@Database(entities = [Product::class], version = 1, exportSchema = false)
@TypeConverters(ZonedDateTimeConverter::class)
abstract class JasmineDatabase : RoomDatabase() {
    abstract fun productDao(): ProductDao

    companion object {
        @Volatile
        private var Instance: JasmineDatabase? = null

        fun getDatabase(context: Context): JasmineDatabase {
            return Instance ?: synchronized(this) {
                Room.databaseBuilder(context, JasmineDatabase::class.java, "jasmine_database")
                    .build().also { Instance = it }
            }
        }
    }
}