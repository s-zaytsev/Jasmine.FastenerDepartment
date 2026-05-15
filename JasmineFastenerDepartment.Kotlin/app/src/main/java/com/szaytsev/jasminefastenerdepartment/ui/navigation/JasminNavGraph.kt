package com.szaytsev.jasminefastenerdepartment.ui.navigation

import androidx.compose.animation.AnimatedContentTransitionScope
import androidx.compose.animation.core.tween
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.navigation.NavHostController
import androidx.navigation.NavType
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.navArgument
import com.szaytsev.jasminefastenerdepartment.ui.order.OrderDestination
import com.szaytsev.jasminefastenerdepartment.ui.order.OrderScreen
import com.szaytsev.jasminefastenerdepartment.ui.product.ProductDetailsDestination
import com.szaytsev.jasminefastenerdepartment.ui.product.ProductDetailsScreen
import com.szaytsev.jasminefastenerdepartment.ui.product.ProductEditDestination
import com.szaytsev.jasminefastenerdepartment.ui.product.ProductEditScreen
import com.szaytsev.jasminefastenerdepartment.ui.product.ProductEntryDestination
import com.szaytsev.jasminefastenerdepartment.ui.product.ProductEntryScreen
import com.szaytsev.jasminefastenerdepartment.ui.products.ProductsDestination
import com.szaytsev.jasminefastenerdepartment.ui.products.ProductsScreen
import com.szaytsev.jasminefastenerdepartment.ui.settings.SettingsDestination
import com.szaytsev.jasminefastenerdepartment.ui.settings.SettingsScreen
import com.szaytsev.jasminefastenerdepartment.ui.synchronization.SynchronizationDestination
import com.szaytsev.jasminefastenerdepartment.ui.synchronization.SynchronizationScreen

@Composable
fun JasmineNavHost(
    navController: NavHostController,
    modifier: Modifier = Modifier,
) {
    NavHost(
        navController = navController,
        startDestination = ProductsDestination.route,
        enterTransition = {
            slideIntoContainer(
                AnimatedContentTransitionScope.SlideDirection.Start,
                tween(700)
            )
        },
        exitTransition = {
            slideOutOfContainer(
                AnimatedContentTransitionScope.SlideDirection.Start,
                tween(700)
            )
        },
        popEnterTransition = {
            slideIntoContainer(
                AnimatedContentTransitionScope.SlideDirection.End,
                tween(700)
            )
        },
        popExitTransition = {
            slideOutOfContainer(
                AnimatedContentTransitionScope.SlideDirection.End,
                tween(700)
            )
        },
        modifier = modifier,
    ) {
        composable(
            route = ProductsDestination.route,
        ) {
            ProductsScreen(
                navigateToProductEntry = {
                    navController.navigate(ProductEntryDestination.route)
                },
                navigateToProductUpdate = {
                    navController.navigate("${ProductDetailsDestination.route}/${it}")
                }
            )
        }

        composable(
            route = OrderDestination.route
        ) {
            OrderScreen(navigateToProductUpdate = {
                navController.navigate("${ProductDetailsDestination.route}/${it}")
            })
        }

        composable(
            route = SynchronizationDestination.route
        ) {
            SynchronizationScreen()
        }

        composable(
            route = SettingsDestination.route
        ) {
            SettingsScreen()
        }

        composable(
            route = ProductEntryDestination.route
        ) {
            ProductEntryScreen(
                navigateBack = { navController.popBackStack() },
                onNavigateUp = { navController.navigateUp() }
            )
        }
        composable(
            route = ProductDetailsDestination.routeWithArgs,
            arguments = listOf(navArgument(ProductDetailsDestination.productIdArg) {
                type = NavType.StringType
            })
        ) {
            ProductDetailsScreen(
                navigateToEditItem = {
                    navController.navigate("${ProductEditDestination.route}/$it")
                },
                navigateBack = { navController.navigateUp() }
            )
        }
        composable(
            route = ProductEditDestination.routeWithArgs,
            arguments = listOf(navArgument(ProductEditDestination.productIdArg) {
                type = NavType.StringType
            })
        ) {
            ProductEditScreen(
                navigateBack = { navController.popBackStack() },
                onNavigateUp = { navController.navigateUp() }
            )
        }
    }
}