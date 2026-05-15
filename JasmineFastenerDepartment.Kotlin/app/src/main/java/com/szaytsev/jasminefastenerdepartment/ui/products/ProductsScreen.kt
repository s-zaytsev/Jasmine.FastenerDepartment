package com.szaytsev.jasminefastenerdepartment.ui.products

import android.annotation.SuppressLint
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.LazyListState
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material.icons.filled.ShoppingCart
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.FloatingActionButton
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TopAppBarDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.derivedStateOf
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableIntStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.res.dimensionResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.lifecycle.viewmodel.compose.viewModel
import com.szaytsev.jasminefastenerdepartment.JasmineTopAppBar
import com.szaytsev.jasminefastenerdepartment.R
import com.szaytsev.jasminefastenerdepartment.data.models.Product
import com.szaytsev.jasminefastenerdepartment.data.models.ProductMeasurementUnitCode
import com.szaytsev.jasminefastenerdepartment.data.models.ProductPriceTagCode
import com.szaytsev.jasminefastenerdepartment.ui.AppViewModelProvider
import com.szaytsev.jasminefastenerdepartment.ui.navigation.NavigationDestination
import com.szaytsev.jasminefastenerdepartment.ui.shared.ProductPrice
import com.szaytsev.jasminefastenerdepartment.ui.theme.JasmineFastenerDepartmentTheme
import kotlinx.coroutines.launch
import java.time.ZonedDateTime
import java.util.UUID

object ProductsDestination : NavigationDestination {
    override val route = "products"
    override val titleRes = R.string.app_name
}

@OptIn(ExperimentalMaterial3Api::class)
@SuppressLint("UnusedMaterial3ScaffoldPaddingParameter")
@Composable
fun ProductsScreen(
    navigateToProductEntry: () -> Unit,
    navigateToProductUpdate: (String) -> Unit,
    modifier: Modifier = Modifier,
    viewModel: ProductsViewModel = viewModel(factory = AppViewModelProvider.Factory)
) {
    val productsUiState by viewModel.productsUiState.collectAsState()
    val scrollBehavior = TopAppBarDefaults.enterAlwaysScrollBehavior()
    val coroutineScope = rememberCoroutineScope()
    val listState = rememberLazyListState()

    Scaffold(
        modifier = modifier.nestedScroll(scrollBehavior.nestedScrollConnection),
        topBar = {
            JasmineTopAppBar(
                filter = viewModel.filterState, onValueChange = {
                    viewModel.filter(it)
                    coroutineScope.launch {
                        listState.scrollToItem(0)
                    }
                }, scrollBehavior = scrollBehavior
            )
        },
        floatingActionButton = {
            FloatingActionButton(
                onClick = navigateToProductEntry,
                shape = MaterialTheme.shapes.medium,
                modifier = Modifier.padding(dimensionResource(id = R.dimen.padding_large))
            ) {
                Row(
                    modifier = Modifier.padding(horizontal = 16.dp)
                ) {
                    Icon(
                        imageVector = Icons.Default.Add,
                        contentDescription = stringResource(R.string.product_entry_title)
                    )
                    AnimatedVisibility(listState.isScrollingUp()) {
                        Text(
                            text = stringResource(R.string.product_entry_title),
                            modifier = Modifier.padding(start = 8.dp, top = 3.dp)
                        )
                    }
                }
            }
        },
    ) { innerPadding ->
        if (productsUiState.isLoading) {
            CircularProgressIndicator(
                modifier
                    .padding(innerPadding)
                    .fillMaxHeight()
            )
        } else {
            ProductsScreenBody(
                itemList = productsUiState.productList,
                onItemClick = navigateToProductUpdate,
                state = listState,
                modifier = modifier
                    .padding(innerPadding)
                    .fillMaxSize()
            )
        }
    }
}

@Composable
fun ProductsScreenBody(
    itemList: List<Product>,
    onItemClick: (String) -> Unit,
    state: LazyListState,
    modifier: Modifier = Modifier
) {
    Column(
        horizontalAlignment = Alignment.CenterHorizontally, modifier = modifier
    ) {
        if (itemList.isEmpty()) {
            Text(
                text = stringResource(R.string.no_products_description),
                textAlign = TextAlign.Center,
                style = MaterialTheme.typography.titleLarge
            )
        } else {
            ProductList(
                productList = itemList,
                state = state,
                onItemClick = { onItemClick(it.id) },
                modifier = Modifier.padding(horizontal = dimensionResource(id = R.dimen.padding_small))
            )
        }
    }
}

@Composable
private fun ProductList(
    productList: List<Product>,
    state: LazyListState,
    onItemClick: (Product) -> Unit,
    modifier: Modifier = Modifier
) {
    LazyColumn(modifier = modifier, state = state) {
        items(
            count = productList.size,
            key = { productList[it].id },
            itemContent = { index ->
                ProductItem(
                    product = productList[index],
                    modifier = Modifier
                        .padding(dimensionResource(id = R.dimen.padding_small))
                        .clickable { onItemClick(productList[index]) })
            })
    }
}

@Composable
private fun LazyListState.isScrollingUp(): Boolean {
    var previousIndex by remember(this) { mutableIntStateOf(firstVisibleItemIndex) }
    var previousScrollOffset by remember(this) { mutableIntStateOf(firstVisibleItemScrollOffset) }
    return remember(this) {
        derivedStateOf {
            if (previousIndex != firstVisibleItemIndex) {
                previousIndex > firstVisibleItemIndex
            } else {
                previousScrollOffset >= firstVisibleItemScrollOffset
            }.also {
                previousIndex = firstVisibleItemIndex
                previousScrollOffset = firstVisibleItemScrollOffset
            }
        }
    }.value
}

@Composable
private fun ProductItem(
    product: Product, modifier: Modifier = Modifier
) {
    Card(
        modifier = modifier, elevation = CardDefaults.cardElevation(defaultElevation = 2.dp)
    ) {
        Row(
            verticalAlignment = Alignment.CenterVertically,
            horizontalArrangement = Arrangement.Center,
            modifier = Modifier.fillMaxWidth()
        ) {

            Column(
                modifier = Modifier.padding(dimensionResource(id = R.dimen.padding_large)),
                verticalArrangement = Arrangement.spacedBy(dimensionResource(id = R.dimen.padding_small))
            ) {
                Row(
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Text(
                        text = product.priceTagCode.name,
                        style = MaterialTheme.typography.titleSmall,
                        textAlign = TextAlign.Center,
                        color = MaterialTheme.colorScheme.primary,
                    )
                    Spacer(modifier.weight(1f))
                    Icon(
                        imageVector = Icons.Default.ShoppingCart,
                        contentDescription = null,
                        tint = if (product.isNeededToOrder) {
                            MaterialTheme.colorScheme.primary
                        } else {
                            Color.LightGray
                        }
                    )
                }
                Row(
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Text(
                        text = product.name,
                        style = MaterialTheme.typography.titleMedium,
                    )
                }
                Row(
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Text(
                        text = product.number.toString(),
                        style = MaterialTheme.typography.titleSmall
                    )
                    Spacer(modifier.weight(1f))
                    ProductPrice(
                        value = product.price,
                        unitCode = product.measurementUnitCode,
                        style = MaterialTheme.typography.titleSmall
                    )
                }
            }
        }
    }
}

@Preview(showBackground = true)
@Composable
fun ProductListPreview() {
    JasmineFastenerDepartmentTheme {
        ProductsScreenBody(
            listOf(
                Product(
                    id = UUID.randomUUID().toString(),
                    number = 100089,
                    name = "Game 2",
                    price = 100.0,
                    isNeededToOrder = false,
                    isNeededToPrint = true,
                    isDeleted = false,
                    priceTagCode = ProductPriceTagCode.S,
                    measurementUnitCode = ProductMeasurementUnitCode.Lists,
                    createdDate = ZonedDateTime.now(),
                    modifiedDate = ZonedDateTime.now()
                ),
                Product(
                    id = UUID.randomUUID().toString(),
                    number = 1000891,
                    name = "Game 2",
                    price = 100.0,
                    isNeededToOrder = false,
                    isNeededToPrint = true,
                    isDeleted = false,
                    priceTagCode = ProductPriceTagCode.S,
                    measurementUnitCode = ProductMeasurementUnitCode.Lists,
                    createdDate = ZonedDateTime.now(),
                    modifiedDate = ZonedDateTime.now()
                ),
            ),
            state = LazyListState(),
            onItemClick = {})
    }
}

@Preview(showBackground = true)
@Composable
fun EmptyProductListPreview() {
    JasmineFastenerDepartmentTheme {
        ProductsScreenBody(
            listOf(),
            state = LazyListState(),
            onItemClick = {})
    }
}

@Preview(showBackground = true)
@Composable
fun InventoryItemPreview() {
    JasmineFastenerDepartmentTheme {
        ProductItem(
            Product(
                id = "100089",
                number = 100089,
                name = "Длинное Длинное Длинное Длинное Длинное Длинное Длинное Длинное название",
                price = 100.0,
                isNeededToOrder = false,
                isNeededToPrint = true,
                isDeleted = false,
                priceTagCode = ProductPriceTagCode.S,
                measurementUnitCode = ProductMeasurementUnitCode.Lists,
                createdDate = ZonedDateTime.now(),
                modifiedDate = ZonedDateTime.now()
            ),
        )
    }
}