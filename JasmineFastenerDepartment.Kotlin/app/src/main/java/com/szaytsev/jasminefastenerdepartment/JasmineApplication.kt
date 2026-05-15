package com.szaytsev.jasminefastenerdepartment

import android.app.Application
import com.szaytsev.jasminefastenerdepartment.data.AppContainer
import com.szaytsev.jasminefastenerdepartment.data.AppDataContainer

class JasmineApplication : Application() {

    lateinit var container: AppContainer

    override fun onCreate() {
        super.onCreate()
        container = AppDataContainer(this)
    }
}