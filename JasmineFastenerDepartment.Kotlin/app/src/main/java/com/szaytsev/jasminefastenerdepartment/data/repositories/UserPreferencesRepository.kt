package com.szaytsev.jasminefastenerdepartment.data.repositories

import androidx.datastore.core.DataStore
import androidx.datastore.preferences.core.Preferences
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.core.emptyPreferences
import androidx.datastore.preferences.core.stringPreferencesKey
import com.szaytsev.jasminefastenerdepartment.ui.toUtc
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.catch
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.flow.map
import java.io.IOException
import java.time.ZoneOffset
import java.time.ZonedDateTime

class UserPreferencesRepository(
    private val dataStore: DataStore<Preferences>
) {
    private companion object {
        val SERVER_ADDRESS = stringPreferencesKey("server_address")
        val LAST_SYNCHRONIZE_TIME = stringPreferencesKey("last_synchronize_time")
    }

    val serverAddress: Flow<String>  = getSettings(SERVER_ADDRESS)
    val lastSynchronizedTime: Flow<String> = getSettings(LAST_SYNCHRONIZE_TIME)

    suspend fun readServerUrl(): String? {
        val dataStoreKey = SERVER_ADDRESS;
        val preferences = dataStore.data.first()
        return preferences[dataStoreKey];
    }

    suspend fun getLastSynchronizeTime(): String? {
        val dataStoreKey = LAST_SYNCHRONIZE_TIME;
        val preferences = dataStore.data.first()
        return preferences[dataStoreKey];
    }

    suspend fun saveServerUrlPreference(serverAddress: String) {
        dataStore.edit { preferences ->
            preferences[SERVER_ADDRESS] = serverAddress
        }
    }

    suspend fun saveLastSynchronizedTimePreference(dateTime: ZonedDateTime?) {
        dataStore.edit { preferences ->
            preferences[LAST_SYNCHRONIZE_TIME] = dateTime?.toUtc()?.toString() ?: ""
        }
    }

    private fun getSettings(settings: Preferences.Key<String>): Flow<String> {
        return dataStore.data
            .catch {
                if (it is IOException) {
                    emit(emptyPreferences())
                } else {
                    throw it
                }
            }
            .map { preferences ->
                preferences[settings] ?: ""
            }
    }
}