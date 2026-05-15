package com.szaytsev.jasminefastenerdepartment.ui.product

import androidx.compose.foundation.layout.padding
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.lifecycle.viewmodel.compose.viewModel
import com.szaytsev.jasminefastenerdepartment.JasmineTopAppBar
import com.szaytsev.jasminefastenerdepartment.R
import com.szaytsev.jasminefastenerdepartment.ui.AppViewModelProvider
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiModel
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiState
import com.szaytsev.jasminefastenerdepartment.ui.navigation.NavigationDestination
import com.szaytsev.jasminefastenerdepartment.ui.theme.JasmineFastenerDepartmentTheme
import kotlinx.coroutines.launch

object ProductEditDestination : NavigationDestination {
    override val route = "product_edit"
    override val titleRes = R.string.edit_product_title
    const val productIdArg = "productId"
    val routeWithArgs = "$route/{$productIdArg}"
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ProductEditScreen(
    navigateBack: () -> Unit,
    onNavigateUp: () -> Unit,
    modifier: Modifier = Modifier,
    viewModel: ProductEditViewModel = viewModel(factory = AppViewModelProvider.Factory)
) {
    val coroutineScope = rememberCoroutineScope()
    Scaffold(
        topBar = {
            JasmineTopAppBar(
                title = stringResource(ProductEditDestination.titleRes),
                canNavigateBack = true,
                navigateUp = onNavigateUp
            )
        },
        modifier = modifier
    ) { innerPadding ->
        ProductEntryBody(
            productUiState = viewModel.productUiState,
            onProductValueChange = viewModel::updateUiState,
            onSaveClick = {
                coroutineScope.launch {
                    viewModel.updateItem()
                    navigateBack()
                }
            },
            modifier = Modifier.padding(innerPadding)
        )
    }
}

@Preview(showBackground = true)
@Composable
fun ProductEntryBodyPreview() {
    JasmineFastenerDepartmentTheme {
        ProductEntryBody(
            productUiState = ProductUiState(
                productUiModel = ProductUiModel(
                    "42198",
                    number = 42198,
                    name = "Test product",
                    price = 244.00
                )
            ),
            onProductValueChange = {},
            onSaveClick = {})
    }
}