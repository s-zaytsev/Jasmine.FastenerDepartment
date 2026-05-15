package com.szaytsev.jasminefastenerdepartment.data

import android.content.Context
import androidx.datastore.core.DataStore
import androidx.datastore.preferences.SharedPreferencesMigration
import androidx.datastore.preferences.core.Preferences
import androidx.datastore.preferences.preferencesDataStore
import com.jakewharton.retrofit2.converter.kotlinx.serialization.asConverterFactory
import com.szaytsev.jasminefastenerdepartment.data.database.JasmineDatabase
import com.szaytsev.jasminefastenerdepartment.data.repositories.LocalProductRepository

import com.szaytsev.jasminefastenerdepartment.data.repositories.ProductsRepository
import com.szaytsev.jasminefastenerdepartment.data.repositories.RemoteProductsRepository
import com.szaytsev.jasminefastenerdepartment.data.repositories.NetworkProductsRepository
import com.szaytsev.jasminefastenerdepartment.data.repositories.UserPreferencesRepository
import com.szaytsev.jasminefastenerdepartment.network.JasmineApiService
import kotlinx.serialization.json.Json
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.OkHttpClient
import retrofit2.Retrofit

private const val USER_PREFERENCE_NAME = "application_settings"

private val Context.dataStore: DataStore<Preferences> by preferencesDataStore(
    name = USER_PREFERENCE_NAME,
    produceMigrations = { migrationContext ->
        listOf(SharedPreferencesMigration(migrationContext, USER_PREFERENCE_NAME))
    }
)

interface AppContainer {
    val productsRepository: ProductsRepository
    val remoteProductsRepository: RemoteProductsRepository
    val userPreferencesRepository: UserPreferencesRepository
}

class AppDataContainer(private val context: Context) : AppContainer {
    
    override val productsRepository: ProductsRepository by lazy {
        LocalProductRepository(JasmineDatabase.getDatabase(context).productDao())
    }

     private val baseUrl = "http://10.0.2.2:5034/"
    //  private val baseUrl = "http://192.168.1.4:86/"
   // private val baseUrl = "http://192.168.1.70:5034/"
    // private val baseUrl = "http://192.168.1.110:5034/"

    private val retrofit: Retrofit = Retrofit.Builder()
        .client(OkHttpClient.Builder().build())
        .addConverterFactory(
            Json {
                ignoreUnknownKeys = true;
                isLenient = true
            }.asConverterFactory(
                "application/json".toMediaType()
            )
        )
        .baseUrl(baseUrl)
        .build()

    private val retrofitService: JasmineApiService by lazy {
        retrofit.create(JasmineApiService::class.java)
    }

    override val remoteProductsRepository: RemoteProductsRepository by lazy {
        NetworkProductsRepository(retrofitService)
    }

    override val userPreferencesRepository: UserPreferencesRepository by lazy {
        UserPreferencesRepository(dataStore = context.dataStore);
    }

}