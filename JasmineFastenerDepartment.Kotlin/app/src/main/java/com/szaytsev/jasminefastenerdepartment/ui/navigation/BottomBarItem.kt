package com.szaytsev.jasminefastenerdepartment.ui.navigation

import androidx.annotation.StringRes
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.automirrored.filled.List
import androidx.compose.material.icons.filled.List
import androidx.compose.material.icons.filled.Refresh
import androidx.compose.material.icons.filled.Settings
import androidx.compose.material.icons.filled.ShoppingCart
import androidx.compose.ui.graphics.vector.ImageVector
import com.szaytsev.jasminefastenerdepartment.R

sealed class BottomBarItem(@get:StringRes val labelResId: Int, val icon: ImageVector, val route: String) {
    object Products :
        BottomBarItem(R.string.nav_title_products, Icons.AutoMirrored.Filled.List, "products")

    object Order :
        BottomBarItem(R.string.nav_title_order, Icons.Default.ShoppingCart, "order")

    object Synchronization :
        BottomBarItem(R.string.nav_title_sync, Icons.Default.Refresh, "synchronization")

    object Settings :
        BottomBarItem(R.string.nav_title_settings, Icons.Default.Settings, "settings")
}