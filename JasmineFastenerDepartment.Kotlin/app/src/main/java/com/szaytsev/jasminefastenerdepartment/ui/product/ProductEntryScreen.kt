package com.szaytsev.jasminefastenerdepartment.ui.product

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.Button
import androidx.compose.material3.DropdownMenu
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.ExposedDropdownMenuBox
import androidx.compose.material3.ExposedDropdownMenuDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.dimensionResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.tooling.preview.Preview
import androidx.lifecycle.viewmodel.compose.viewModel
import com.szaytsev.jasminefastenerdepartment.JasmineTopAppBar
import com.szaytsev.jasminefastenerdepartment.R
import com.szaytsev.jasminefastenerdepartment.data.models.ProductMeasurementUnitCode
import com.szaytsev.jasminefastenerdepartment.data.models.ProductPriceTagCode
import com.szaytsev.jasminefastenerdepartment.ui.AppViewModelProvider
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiModel
import com.szaytsev.jasminefastenerdepartment.ui.models.ProductUiState
import com.szaytsev.jasminefastenerdepartment.ui.navigation.NavigationDestination
import com.szaytsev.jasminefastenerdepartment.ui.theme.JasmineFastenerDepartmentTheme
import kotlinx.coroutines.launch
import java.util.Currency
import java.util.Locale

object ProductEntryDestination : NavigationDestination {
    override val route = "product_entry"
    override val titleRes = R.string.product_entry_title
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ProductEntryScreen(
    navigateBack: () -> Unit,
    onNavigateUp: () -> Unit,
    canNavigateBack: Boolean = true,
    viewModel: ProductEntryViewModel = viewModel(factory = AppViewModelProvider.Factory)
) {
    val coroutineScope = rememberCoroutineScope()
    Scaffold(
        topBar = {
            JasmineTopAppBar(
                title = stringResource(ProductEntryDestination.titleRes),
                canNavigateBack = canNavigateBack,
                navigateUp = onNavigateUp
            )
        }
    ) { innerPadding ->
        ProductEntryBody(
            productUiState = viewModel.productUiState,
            onProductValueChange = viewModel::updateUiState,
            onSaveClick = {
                coroutineScope.launch {
                    viewModel.saveItem()
                    navigateBack()
                }
            },
            modifier = Modifier
                .padding(innerPadding)
                .verticalScroll(rememberScrollState())
                .fillMaxWidth()
        )
    }
}

@Composable
fun ProductEntryBody(
    productUiState: ProductUiState,
    onProductValueChange: (ProductUiModel) -> Unit,
    onSaveClick: () -> Unit,
    modifier: Modifier = Modifier
) {
    Column(
        modifier = modifier.padding(dimensionResource(id = R.dimen.padding_medium)),
        verticalArrangement = Arrangement.spacedBy(dimensionResource(id = R.dimen.padding_large))
    ) {
        ProductInputForm(
            productUiModel = productUiState.productUiModel,
            onValueChange = onProductValueChange,
            modifier = Modifier.fillMaxWidth()
        )
        Button(
            onClick = onSaveClick,
            enabled = productUiState.isEntryValid,
            shape = MaterialTheme.shapes.small,
            modifier = Modifier.fillMaxWidth()
        ) {
            Text(text = stringResource(R.string.save_action))
        }
    }
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ProductInputForm(
    productUiModel: ProductUiModel,
    modifier: Modifier = Modifier,
    onValueChange: (ProductUiModel) -> Unit = {},
    enabled: Boolean = true
) {
    var priceTagFieldExpanded by remember { mutableStateOf(false) }
    val priceTags = ProductPriceTagCode.entries.toTypedArray()
    var selectedPriceTag by remember {
        mutableStateOf(productUiModel.priceTagCode)
    }

    var unitFieldExpanded by remember { mutableStateOf(false) }
    val units = ProductMeasurementUnitCode.entries.toTypedArray()
    var selectedUnit by remember {
        mutableStateOf(productUiModel.measurementUnitCode)
    }

    Column(
        modifier = modifier,
        verticalArrangement = Arrangement.spacedBy(dimensionResource(id = R.dimen.padding_medium))
    ) {
        OutlinedTextField(
            value = productUiModel.number.toString(),
            onValueChange = { onValueChange(productUiModel.copy(number = it.toInt())) },
            keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Number),
            label = { Text(stringResource(R.string.product_id_req)) },
            modifier = Modifier.fillMaxWidth(),
            enabled = enabled,
            singleLine = true
        )
        OutlinedTextField(
            value = productUiModel.name,
            onValueChange = { onValueChange(productUiModel.copy(name = it)) },
            label = { Text(stringResource(R.string.product_name_req)) },
            modifier = Modifier.fillMaxWidth(),
            enabled = enabled,
            singleLine = true
        )
        OutlinedTextField(
            value = productUiModel.price.toString(),
            onValueChange = { onValueChange(productUiModel.copy(price = it.toDouble())) },
            keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Decimal),
            label = { Text(stringResource(R.string.product_price_req)) },
            leadingIcon = { Text(Currency.getInstance(Locale.getDefault()).symbol) },
            modifier = Modifier.fillMaxWidth(),
            enabled = enabled,
            singleLine = true
        )

        ExposedDropdownMenuBox(
            expanded = priceTagFieldExpanded,
            onExpandedChange = {
                priceTagFieldExpanded = !priceTagFieldExpanded
            }
        ) {
            OutlinedTextField(
                value = productUiModel.priceTagCode.name,
                onValueChange = {
                },
                label = { Text((stringResource(R.string.price_tag_size))) },
                readOnly = true,
                trailingIcon = { ExposedDropdownMenuDefaults.TrailingIcon(expanded = priceTagFieldExpanded) },
                modifier = Modifier
                    .menuAnchor()
                    .fillMaxWidth()
            )

            DropdownMenu(
                expanded = priceTagFieldExpanded,
                onDismissRequest = { priceTagFieldExpanded = false }) {
                priceTags.forEach {
                    DropdownMenuItem(
                        text = { Text(text = it.name) },
                        onClick = {
                            selectedPriceTag = it
                            priceTagFieldExpanded = false
                            onValueChange(productUiModel.copy(priceTagCode = selectedPriceTag))
                        })
                }
            }
        }


        ExposedDropdownMenuBox(
            expanded = unitFieldExpanded,
            onExpandedChange = {
                unitFieldExpanded = !unitFieldExpanded
            }
        ) {
            OutlinedTextField(
                value = stringResource(
                    ProductMeasurementUnitCode.getDisplayNameResourceId(
                        productUiModel.measurementUnitCode
                    )
                ),
                onValueChange = {
                },
                label = { Text((stringResource(R.string.measurement_unit))) },
                readOnly = true,
                trailingIcon = { ExposedDropdownMenuDefaults.TrailingIcon(expanded = unitFieldExpanded) },
                modifier = Modifier
                    .menuAnchor()
                    .fillMaxWidth()
            )
            DropdownMenu(
                expanded = unitFieldExpanded,
                onDismissRequest = { unitFieldExpanded = false }) {
                units.forEach {
                    DropdownMenuItem(
                        text = {
                            Text(
                                text = stringResource(
                                    ProductMeasurementUnitCode.getDisplayNameResourceId(
                                        it
                                    )
                                )
                            )
                        },
                        onClick = {
                            selectedUnit = it
                            unitFieldExpanded = false
                            onValueChange(productUiModel.copy(measurementUnitCode = selectedUnit))
                        })
                }
            }
        }
    }
}

@Preview(showBackground = true)
@Composable
private fun ItemEntryScreenPreview() {
    JasmineFastenerDepartmentTheme {
        ProductEntryBody(
            productUiState = ProductUiState(),
            onProductValueChange = {},
            onSaveClick = {})
    }
}