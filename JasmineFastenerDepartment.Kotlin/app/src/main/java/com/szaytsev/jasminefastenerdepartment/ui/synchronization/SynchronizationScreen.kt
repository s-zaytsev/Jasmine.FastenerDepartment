package com.szaytsev.jasminefastenerdepartment.ui.synchronization

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.material3.Button
import androidx.compose.material3.Card
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.lifecycle.viewmodel.compose.viewModel
import com.szaytsev.jasminefastenerdepartment.R
import com.szaytsev.jasminefastenerdepartment.data.models.ProductChangeReasonCode
import com.szaytsev.jasminefastenerdepartment.data.models.ProductHistoryEntry
import com.szaytsev.jasminefastenerdepartment.network.models.SynchronizationHistoryEntry
import com.szaytsev.jasminefastenerdepartment.network.models.SynchronizationHistoryEntryItem
import com.szaytsev.jasminefastenerdepartment.ui.AppViewModelProvider
import com.szaytsev.jasminefastenerdepartment.ui.models.SynchronizationUiState
import com.szaytsev.jasminefastenerdepartment.ui.models.SynchronizeStatus
import com.szaytsev.jasminefastenerdepartment.ui.navigation.NavigationDestination
import com.szaytsev.jasminefastenerdepartment.ui.shared.ProductChangeReason
import com.szaytsev.jasminefastenerdepartment.ui.theme.JasmineFastenerDepartmentTheme
import com.szaytsev.jasminefastenerdepartment.ui.toLocalDateTimeOrNull
import com.szaytsev.jasminefastenerdepartment.ui.toUtc
import kotlinx.coroutines.launch
import java.time.ZonedDateTime
import java.time.format.DateTimeFormatter
import java.util.UUID

object SynchronizationDestination : NavigationDestination {
    override val route = "synchronization"
    override val titleRes = R.string.app_name
}

@Composable
fun SynchronizationScreen(
    viewModel: SynchronizationViewModel = viewModel(factory = AppViewModelProvider.Factory)
) {
    val uiState by viewModel.uiState.collectAsState()
    val coroutineScope = rememberCoroutineScope()

    SyncBody(
        uiState = uiState,
        onSync = { coroutineScope.launch { viewModel.synchronize() } },
        onReset = { coroutineScope.launch { viewModel.setPendingStatus() } },
    )
}

@Composable
fun SyncBody(
    uiState: SynchronizationUiState,
    onSync: () -> Unit,
    onReset: () -> Unit,
) {
    Scaffold { innerPadding ->
        Column(
            verticalArrangement = Arrangement.Center,
            horizontalAlignment = Alignment.CenterHorizontally,
            modifier = Modifier
                .padding(innerPadding)
                .fillMaxSize()
        ) {
            when (uiState.status) {
                SynchronizeStatus.Pending -> SyncPendingBody(uiState = uiState, onSync = onSync)
                SynchronizeStatus.Loading -> SyncLoadingBody()
                SynchronizeStatus.Success -> SyncSuccessBody(uiState, onReset)
                SynchronizeStatus.Error -> SyncErrorBody(uiState = uiState, onReset = onReset)
            }
        }
    }
}

@Composable
fun SyncPendingBody(
    uiState: SynchronizationUiState,
    onSync: () -> Unit
) {
    var lastSync = "Нет данных"

    if (uiState.lastSynchronizeTime != null) {
        val formatter = DateTimeFormatter.ofPattern("dd MMM yyyy HH:mm:ss")
        lastSync = uiState.lastSynchronizeTime.toLocalDateTimeOrNull()!!.format(formatter)
    }

    Text("Измененные товары")
    Text("${uiState.products.count()}", fontSize = 64.sp)
    Text("Последняя синхронизация")
    Text(lastSync)
    Button(onClick = onSync) {
        Text("Синхронизация")
    }
}

@Composable
fun SyncLoadingBody() {
    CircularProgressIndicator(modifier = Modifier.width(64.dp))
}

@Composable
fun SyncSuccessBody(
    uiState: SynchronizationUiState,
    onReset: () -> Unit
) {
    if (uiState.successMessage.isNotBlank()) {
        SyncSuccessWithMessage(uiState.successMessage, onReset)
    } else if (uiState.historyItems.isNotEmpty()) {
        SyncSuccessWithHistory(uiState.historyItems, onReset)
    } else {
        SyncSuccessNoChanges(onReset)
    }
}

@Composable
fun SyncSuccessNoChanges(
    onReset: () -> Unit
) {
    Text(
        text = "Данные актуальны на всех устройствах",
        fontSize = 24.sp,
        textAlign = TextAlign.Center
    )
    Button(
        onClick = onReset,
        modifier = Modifier
            .padding(bottom = 16.dp, top = 16.dp)
    ) {
        Text(stringResource(R.string.close))
    }
}

@Composable
fun SyncSuccessWithMessage(
    message: String,
    onReset: () -> Unit
) {
    Text(
        text = message,
        fontSize = 24.sp,
        textAlign = TextAlign.Center
    )
    Button(
        onClick = onReset,
        modifier = Modifier
            .padding(vertical = 16.dp)
    ) {
        Text(stringResource(R.string.close))
    }
}

@Composable
fun SyncSuccessWithHistory(
    historyItems: List<SynchronizationHistoryEntry>,
    onReset: () -> Unit,
    modifier: Modifier = Modifier
) {
    val localState = rememberLazyListState()
    Column() {
        Box(modifier = Modifier.weight(0.9f)) {
            LazyColumn(state = localState, modifier = modifier.padding(16.dp)) {
                items(
                    count = historyItems.size,
                    key = { historyItems[it].date },
                    itemContent = { index ->
                        HistoryItem(historyItems[index])
                    }
                )
            }
        }

        Box(modifier = Modifier.weight(0.1f).align(Alignment.CenterHorizontally)) {
            Button(onClick = onReset, modifier = Modifier.padding(bottom = 16.dp)) {
                Text(stringResource(R.string.close))
            }
        }
    }
}

@Composable
fun HistoryItem(
    item: SynchronizationHistoryEntry,
    modifier: Modifier = Modifier
) {
    val date = item.date.toLocalDateTime().format(DateTimeFormatter.ofPattern("dd.MM.yyyy"))
    Text(date, style = MaterialTheme.typography.titleMedium)
    Column(modifier = modifier.padding(vertical = 8.dp)) {
        item.historyItems.forEach { x ->
            Column(modifier = modifier.padding(vertical = 4.dp)) {
                x.productHistoryEntries.forEach { y ->
                    HistoryItemLine(y)
                }
            }
        }
    }
}

@Composable
fun HistoryItemLine(
    line: ProductHistoryEntry,
    modifier: Modifier = Modifier
) {
    val productNumber = line.productNumber.toString()
    val timeOfChange =
        line.createdDate.toLocalTime().format(DateTimeFormatter.ofPattern("HH:mm:ss"))
    Card(modifier = modifier.padding(3.dp)) {
        Column(modifier = modifier.padding(12.dp)) {
            Row(
                horizontalArrangement = Arrangement.SpaceBetween,
                verticalAlignment = Alignment.CenterVertically,
                modifier = modifier
                    .fillMaxWidth()
                    .padding(bottom = 6.dp)
            ) {
                Text(productNumber, style = MaterialTheme.typography.bodySmall)
                Text(timeOfChange, style = MaterialTheme.typography.bodySmall)
            }
            ProductChangeReason(line)
        }
    }
}

@Composable
fun SyncErrorBody(
    uiState: SynchronizationUiState,
    onReset: () -> Unit
) {
    Card(modifier = Modifier.padding(30.dp)) {
        Column(
            horizontalAlignment = Alignment.CenterHorizontally,
            modifier = Modifier.padding(10.dp)
        ) {
            Text(text = uiState.errorMessage ?: "", textAlign = TextAlign.Center)
            Button(onClick = onReset, modifier = Modifier.padding(top = 16.dp)) {
                Text(stringResource(R.string.close))
            }
        }
    }
}

@Preview(showBackground = true)
@Composable
fun SyncPendingBodyPreview() {
    JasmineFastenerDepartmentTheme {
        SyncBody(
            uiState = SynchronizationUiState(
                products = listOf(),
                lastSynchronizeTime = ZonedDateTime.now(),
                status = SynchronizeStatus.Pending,
                successMessage = "",
                errorMessage = null,
                historyItems = emptyList()
            ),
            onSync = {},
            onReset = {},
        )
    }
}

@Preview(showBackground = true)
@Composable
fun SyncSuccessBodyPreview() {
    JasmineFastenerDepartmentTheme {
        SyncBody(
            uiState = SynchronizationUiState(
                products = listOf(),
                lastSynchronizeTime = ZonedDateTime.now(),
                status = SynchronizeStatus.Success,
                successMessage = "",
                errorMessage = null
            ),
            onSync = {},
            onReset = {},
        )
    }
}

@Preview(showBackground = true)
@Composable
fun SyncErrorBodyPreview() {
    JasmineFastenerDepartmentTheme {
        SyncBody(
            uiState = SynchronizationUiState(
                products = listOf(),
                lastSynchronizeTime = ZonedDateTime.now(),
                status = SynchronizeStatus.Error,
                successMessage = "",
                errorMessage = stringResource(R.string.sync_error)
            ),
            onSync = {},
            onReset = {},
        )
    }
}

@Preview(showBackground = true)
@Composable
fun SyncLoadingBodyPreview() {
    JasmineFastenerDepartmentTheme {
        SyncBody(
            uiState = SynchronizationUiState(
                products = listOf(),
                lastSynchronizeTime = ZonedDateTime.now(),
                status = SynchronizeStatus.Loading,
                successMessage = "",
                errorMessage = null
            ),
            onSync = {},
            onReset = {},
        )
    }
}

@Preview(showBackground = true)
@Composable
fun SyncSuccessWithMessagePreview() {
    JasmineFastenerDepartmentTheme {
        SyncBody(
            uiState = SynchronizationUiState(
                lastSynchronizeTime = ZonedDateTime.now().toUtc(),
                products = emptyList(),
                successMessage = "Добавлено 22 товара",
                errorMessage = null,
                status = SynchronizeStatus.Success,
                historyItems = listOf()
            ),
            onSync = {},
            onReset = {}
        )
    }
}

@Preview(showBackground = true)
@Composable
fun SyncSuccessWithHistoryPreview() {
    JasmineFastenerDepartmentTheme {
        SyncBody(
            uiState = SynchronizationUiState(
                lastSynchronizeTime = null,
                products = emptyList(),
                successMessage = "",
                errorMessage = null,
                status = SynchronizeStatus.Success,
                historyItems = listOf(
                    SynchronizationHistoryEntry(
                        date = ZonedDateTime.now(),
                        historyItems = listOf(
                            SynchronizationHistoryEntryItem(
                                productId = UUID.randomUUID().toString(),
                                productHistoryEntries = listOf(
                                    ProductHistoryEntry(
                                        id = UUID.randomUUID().toString(),
                                        productId = UUID.randomUUID().toString(),
                                        createdDate = ZonedDateTime.now(),
                                        changeReasonCode = ProductChangeReasonCode.ChangedPrice,
                                        oldValue = "2.00",
                                        newValue = "4.00",
                                        productNumber = 213123
                                    ),
                                    ProductHistoryEntry(
                                        id = UUID.randomUUID().toString(),
                                        productId = UUID.randomUUID().toString(),
                                        createdDate = ZonedDateTime.now().minusHours(1),
                                        changeReasonCode = ProductChangeReasonCode.ChangedName,
                                        oldValue = "Old Name",
                                        newValue = "New Name",
                                        productNumber = 213123
                                    )
                                )
                            ),
                            SynchronizationHistoryEntryItem(
                                productId = "Test1",
                                productHistoryEntries = listOf(
                                    ProductHistoryEntry(
                                        id = "test1",
                                        productId = "Test1",
                                        createdDate = ZonedDateTime.now(),
                                        changeReasonCode = ProductChangeReasonCode.ChangedPrice,
                                        oldValue = "2.00",
                                        newValue = "4.00",
                                        productNumber = 213123
                                    )
                                )
                            ),
                            SynchronizationHistoryEntryItem(
                                productId = "Test2",
                                productHistoryEntries = listOf(
                                    ProductHistoryEntry(
                                        id = "test2",
                                        productId = "Test2",
                                        createdDate = ZonedDateTime.now(),
                                        changeReasonCode = ProductChangeReasonCode.ChangedPrice,
                                        oldValue = "2.00",
                                        newValue = "4.00",
                                        productNumber = 213123
                                    )
                                )
                            ),
                            SynchronizationHistoryEntryItem(
                                productId = "Test3",
                                productHistoryEntries = listOf(
                                    ProductHistoryEntry(
                                        id = "test3",
                                        productId = "Test3",
                                        createdDate = ZonedDateTime.now(),
                                        changeReasonCode = ProductChangeReasonCode.ChangedPrice,
                                        oldValue = "2.00",
                                        newValue = "4.00",
                                        productNumber = 213123
                                    )
                                )
                            ),
                            SynchronizationHistoryEntryItem(
                                productId = "Test4",
                                productHistoryEntries = listOf(
                                    ProductHistoryEntry(
                                        id = "test4",
                                        productId = "Test4",
                                        createdDate = ZonedDateTime.now(),
                                        changeReasonCode = ProductChangeReasonCode.ChangedPrice,
                                        oldValue = "2.00",
                                        newValue = "4.00",
                                        productNumber = 213123
                                    )
                                )
                            )
                        )
                    ),
                    SynchronizationHistoryEntry(
                        date = ZonedDateTime.now().minusDays(3),
                        historyItems = listOf(
                            SynchronizationHistoryEntryItem(
                                productId = UUID.randomUUID().toString(),
                                productHistoryEntries = listOf(
                                    ProductHistoryEntry(
                                        id = UUID.randomUUID().toString(),
                                        productId = UUID.randomUUID().toString(),
                                        createdDate = ZonedDateTime.now(),
                                        changeReasonCode = ProductChangeReasonCode.ChangedPrice,
                                        oldValue = "2.00",
                                        newValue = "4.00",
                                        productNumber = 213123
                                    )
                                )
                            ),
                            SynchronizationHistoryEntryItem(
                                productId = "Test1",
                                productHistoryEntries = listOf(
                                    ProductHistoryEntry(
                                        id = "test1",
                                        productId = "Test1",
                                        createdDate = ZonedDateTime.now(),
                                        changeReasonCode = ProductChangeReasonCode.ChangedPrice,
                                        oldValue = "2.00",
                                        newValue = "4.00",
                                        productNumber = 213123
                                    )
                                )
                            ),
                            SynchronizationHistoryEntryItem(
                                productId = "Test2",
                                productHistoryEntries = listOf(
                                    ProductHistoryEntry(
                                        id = "test2",
                                        productId = "Test2",
                                        createdDate = ZonedDateTime.now(),
                                        changeReasonCode = ProductChangeReasonCode.ChangedPrice,
                                        oldValue = "2.00",
                                        newValue = "4.00",
                                        productNumber = 213123
                                    )
                                )
                            ),
                            SynchronizationHistoryEntryItem(
                                productId = "Test3",
                                productHistoryEntries = listOf(
                                    ProductHistoryEntry(
                                        id = "test3",
                                        productId = "Test3",
                                        createdDate = ZonedDateTime.now(),
                                        changeReasonCode = ProductChangeReasonCode.ChangedPrice,
                                        oldValue = "2.00",
                                        newValue = "4.00",
                                        productNumber = 213123
                                    )
                                )
                            ),
                            SynchronizationHistoryEntryItem(
                                productId = "Test4",
                                productHistoryEntries = listOf(
                                    ProductHistoryEntry(
                                        id = "test4",
                                        productId = "Test4",
                                        createdDate = ZonedDateTime.now(),
                                        changeReasonCode = ProductChangeReasonCode.ChangedPrice,
                                        oldValue = "2.00",
                                        newValue = "4.00",
                                        productNumber = 213123
                                    )
                                )
                            )
                        )
                    )
                ),
            ),
            onReset = {},
            onSync = {}
        )
    }
}