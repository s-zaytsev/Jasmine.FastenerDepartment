package com.szaytsev.jasminefastenerdepartment.ui.order

import android.annotation.SuppressLint
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Scaffold
import androidx.compose.material3.TopAppBarDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.tooling.preview.Preview
import androidx.lifecycle.viewmodel.compose.viewModel
import com.szaytsev.jasminefastenerdepartment.JasmineTopAppBar
import com.szaytsev.jasminefastenerdepartment.R
import com.szaytsev.jasminefastenerdepartment.ui.AppViewModelProvider
import com.szaytsev.jasminefastenerdepartment.ui.navigation.NavigationDestination
import com.szaytsev.jasminefastenerdepartment.ui.products.ProductsScreenBody
import com.szaytsev.jasminefastenerdepartment.ui.theme.JasmineFastenerDepartmentTheme
import kotlinx.coroutines.launch

object OrderDestination : NavigationDestination {
    override val route = "order"
    override val titleRes = R.string.order_title
}

@OptIn(ExperimentalMaterial3Api::class)
@SuppressLint("UnusedMaterial3ScaffoldPaddingParameter")
@Composable
fun OrderScreen(
    navigateToProductUpdate: (String) -> Unit,
    modifier: Modifier = Modifier,
    viewModel: OrderViewModel = viewModel(factory = AppViewModelProvider.Factory)
) {
    val productsUiState by viewModel.orderUiState.collectAsState()
    val scrollBehavior = TopAppBarDefaults.enterAlwaysScrollBehavior()
    val coroutineScope = rememberCoroutineScope()
    val listState = rememberLazyListState()

    Scaffold(
        modifier = modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        topBar = {
            JasmineTopAppBar(
                filter = viewModel.filterState,
                onValueChange = {
                    viewModel.filter(it)
                    coroutineScope.launch {
                        listState.scrollToItem(0)
                    }
                },
                scrollBehavior = scrollBehavior
            )
        }
    ) { innerPadding ->
        ProductsScreenBody(
            itemList = productsUiState.productList,
            state = listState,
            onItemClick = navigateToProductUpdate,
            modifier = modifier
                .padding(innerPadding)
                .fillMaxSize()
        )
    }
}


@Preview(showBackground = true)
@Composable
fun OrderScreenPreview() {
    JasmineFastenerDepartmentTheme {
        OrderScreen(navigateToProductUpdate = {})
    }
}