package com.szaytsev.jasminefastenerdepartment

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Scaffold
import androidx.compose.ui.Modifier
import androidx.navigation.NavHostController
import androidx.navigation.compose.rememberNavController
import com.szaytsev.jasminefastenerdepartment.ui.navigation.BottomNavigationBar
import com.szaytsev.jasminefastenerdepartment.ui.navigation.JasmineNavHost
import com.szaytsev.jasminefastenerdepartment.ui.theme.JasmineFastenerDepartmentTheme

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {

            JasmineFastenerDepartmentTheme {
                val navController: NavHostController = rememberNavController()

                Scaffold(
                    bottomBar = {
                        BottomNavigationBar(
                            navController = navController,
                            modifier = Modifier
                        )
                    }) { paddingValues ->
                    JasmineNavHost(
                        navController = navController,
                        modifier = Modifier.padding(paddingValues)
                    )
                }
            }
        }
    }
}