package com.szaytsev.jasminefastenerdepartment.ui.product

import androidx.annotation.StringRes
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Edit
import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Button
import androidx.compose.material3.Card
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.FloatingActionButton
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableIntStateOf
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.saveable.rememberSaveable
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.dimensionResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.lifecycle.viewmodel.compose.viewModel
import com.szaytsev.jasminefastenerdepartment.JasmineTopAppBar
import com.szaytsev.jasminefastenerdepartment.R
import com.szaytsev.jasminefastenerdepartment.data.models.Product
import com.szaytsev.jasminefastenerdepartment.data.models.ProductMeasurementUnitCode
import com.szaytsev.jasminefastenerdepartment.data.models.ProductPriceTagCode
import com.szaytsev.jasminefastenerdepartment.ui.AppViewModelProvider
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductDetailsUiState
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiModel
import com.szaytsev.jasminefastenerdepartment.ui.navigation.NavigationDestination
import com.szaytsev.jasminefastenerdepartment.ui.theme.JasmineFastenerDepartmentTheme
import com.szaytsev.jasminefastenerdepartment.ui.toProduct
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import kotlin.time.Duration.Companion.seconds

object ProductDetailsDestination : NavigationDestination {
    override val route = "product_details"
    override val titleRes = R.string.product_detail_title
    const val productIdArg = "productId"
    val routeWithArgs = "$route/{$productIdArg}"
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ProductDetailsScreen(
    navigateToEditItem: (String) -> Unit,
    navigateBack: () -> Unit,
    modifier: Modifier = Modifier,
    viewModel: ProductDetailsViewModel = viewModel(factory = AppViewModelProvider.Factory)
) {
    val uiState = viewModel.uiState.collectAsState()
    val coroutineScope = rememberCoroutineScope()

    var counter by remember { mutableIntStateOf(0) }
    LaunchedEffect(Unit) {
        while (true) {
            delay(1.seconds)
            counter++
        }
    }

    Scaffold(
        topBar = {
            JasmineTopAppBar(
                title = stringResource(ProductDetailsDestination.titleRes),
                canNavigateBack = true,
                navigateUp = navigateBack
            )
        },
        floatingActionButton = {
            FloatingActionButton(
                onClick = { navigateToEditItem(uiState.value.productUiModel.id) },
                shape = MaterialTheme.shapes.medium,
                modifier = Modifier.padding(dimensionResource(id = R.dimen.padding_large))
            ) {
                Row(
                    modifier = Modifier.padding(horizontal = 16.dp)
                ) {
                    Icon(
                        imageVector = Icons.Default.Edit,
                        contentDescription = stringResource(R.string.edit_product_title),
                    )
                    AnimatedVisibility(counter < 2) {
                        Text(
                            text = stringResource(R.string.edit_product_title),
                            modifier = Modifier
                                .padding(start = 8.dp, top = 3.dp)
                        )
                    }
                }
            }
        },
        modifier = modifier
    ) { innerPadding ->
        ProductDetailsBody(
            productDetailsUiState = uiState.value,
            onChangeOrderState = {
                coroutineScope.launch {
                    viewModel.changeOrderState()
                }
            },
            onDelete = {
                coroutineScope.launch {
                    viewModel.deleteItem()
                    navigateBack()
                }
            },
            modifier = Modifier
                .padding(innerPadding)
        )
    }
}

@Composable
private fun ProductDetailsBody(
    productDetailsUiState: ProductDetailsUiState,
    onChangeOrderState: () -> Unit,
    onDelete: () -> Unit,
    modifier: Modifier = Modifier
) {
    Column(
        modifier = modifier.padding(dimensionResource(id = R.dimen.padding_medium)),
        verticalArrangement = Arrangement.spacedBy(dimensionResource(id = R.dimen.padding_medium))
    ) {
        var deleteConfirmationRequired by rememberSaveable { mutableStateOf(false) }
        ProductDetails(
            product = productDetailsUiState.productUiModel.toProduct(),
            modifier = Modifier.fillMaxWidth()
        )
        Button(
            onClick = onChangeOrderState,
            modifier = Modifier.fillMaxWidth(),
            shape = MaterialTheme.shapes.small
        ) {
            if (!productDetailsUiState.productUiModel.isNeededToOrder) {
                Text(stringResource(R.string.addToOrder))
            } else {
                Text(stringResource(R.string.removeFromOrder))
            }
        }
        OutlinedButton(
            onClick = { deleteConfirmationRequired = true },
            shape = MaterialTheme.shapes.small,
            modifier = Modifier.fillMaxWidth()
        ) {
            Text(stringResource(R.string.delete))
        }
        if (deleteConfirmationRequired) {
            DeleteConfirmationDialog(
                onDeleteConfirm = {
                    deleteConfirmationRequired = false
                    onDelete()
                },
                onDeleteCancel = { deleteConfirmationRequired = false },
                modifier = Modifier.padding(dimensionResource(id = R.dimen.padding_medium))
            )
        }
    }
}


@Composable
fun ProductDetails(
    product: Product, modifier: Modifier = Modifier
) {
    Card(
        modifier = modifier
    ) {
        Column(
            modifier = Modifier
                .fillMaxWidth()
                .padding(dimensionResource(id = R.dimen.padding_medium)),
            verticalArrangement = Arrangement.spacedBy(dimensionResource(id = R.dimen.padding_medium))
        ) {
            ProductDetailsRow(
                labelResID = R.string.product_id,
                itemDetail = product.number.toString(),
                modifier = Modifier.padding(
                    horizontal = dimensionResource(
                        id = R.dimen
                            .padding_medium
                    )
                )
            )
            ProductDetailsRow(
                labelResID = R.string.product,
                itemDetail = product.name,
                modifier = Modifier.padding(
                    horizontal = dimensionResource(
                        id = R.dimen
                            .padding_medium
                    )
                )
            )
            ProductDetailsRow(
                labelResID = R.string.price,
                itemDetail = product.price.toString(),
                modifier = Modifier.padding(
                    horizontal = dimensionResource(
                        id = R.dimen
                            .padding_medium
                    )
                )
            )
            ProductDetailsRow(
                labelResID = R.string.price_tag_size,
                itemDetail = product.priceTagCode.name,
                modifier = Modifier.padding(
                    horizontal = dimensionResource(
                        id = R.dimen
                            .padding_medium
                    )
                )
            )
            ProductDetailsRow(
                labelResID = R.string.measurement_unit,
                itemDetail = stringResource(
                    ProductMeasurementUnitCode.getDisplayNameResourceId(
                        product.measurementUnitCode
                    )
                ),
                modifier = Modifier.padding(
                    horizontal = dimensionResource(
                        id = R.dimen
                            .padding_medium
                    )
                )
            )
        }

    }
}

/*@Composable
private fun ProductDetailsRow(
    @StringRes labelResID: Int, itemDetail: String, modifier: Modifier = Modifier
) {
    Row(modifier = modifier) {
        Text(text = stringResource(labelResID))
        Spacer(modifier = Modifier.weight(1f))
        Text(text = itemDetail, fontWeight = FontWeight.Bold)
    }
}*/

@Composable
private fun ProductDetailsRow(
    @StringRes labelResID: Int, itemDetail: String, modifier: Modifier = Modifier
) {
    Column(modifier = modifier) {
        Text(text = stringResource(labelResID), fontSize = 12.sp)
        Text(text = itemDetail, fontWeight = FontWeight.Bold)
    }
}

@Composable
private fun DeleteConfirmationDialog(
    onDeleteConfirm: () -> Unit, onDeleteCancel: () -> Unit, modifier: Modifier = Modifier
) {
    AlertDialog(
        onDismissRequest = { /* Do nothing */ },
        title = { Text(stringResource(R.string.attention)) },
        text = { Text(stringResource(R.string.delete_question)) },
        modifier = modifier,
        dismissButton = {
            TextButton(onClick = onDeleteCancel) {
                Text(text = stringResource(R.string.no))
            }
        },
        confirmButton = {
            TextButton(onClick = onDeleteConfirm) {
                Text(text = stringResource(R.string.yes))
            }
        })
}

@Preview(showBackground = true)
@Composable
fun ProductDetailsScreenPreview() {
    JasmineFastenerDepartmentTheme {
        ProductDetailsBody(
            ProductDetailsUiState(
                isDeleted = false,
                productUiModel = ProductUiModel(
                    number = 1213123,
                    name = "Pen",
                    price = 100.00,
                    priceTagCode = ProductPriceTagCode.XL,
                    isNeededToOrder = true
                )
            ), onChangeOrderState = {}, onDelete = {})
    }
}

@Preview(showBackground = true)
@Composable
fun ProductDetailsScreen1Preview() {
    JasmineFastenerDepartmentTheme {
        ProductDetailsBody(
            ProductDetailsUiState(
                isDeleted = false,
                productUiModel = ProductUiModel(
                    number = 1213123,
                    name = "Pen",
                    price = 100.00,
                    priceTagCode = ProductPriceTagCode.XL,
                    isNeededToOrder = true
                )
            ), onChangeOrderState = {}, onDelete = {})
    }
}