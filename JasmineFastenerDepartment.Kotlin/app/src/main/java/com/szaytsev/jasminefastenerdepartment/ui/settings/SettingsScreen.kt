package com.szaytsev.jasminefastenerdepartment.ui.settings

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.saveable.rememberSaveable
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.dimensionResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.lifecycle.viewmodel.compose.viewModel
import com.szaytsev.jasminefastenerdepartment.R
import com.szaytsev.jasminefastenerdepartment.ui.AppViewModelProvider
import com.szaytsev.jasminefastenerdepartment.ui.navigation.NavigationDestination
import com.szaytsev.jasminefastenerdepartment.ui.theme.JasmineFastenerDepartmentTheme
import kotlinx.coroutines.launch

object SettingsDestination : NavigationDestination {
    override val route = "settings"
    override val titleRes = R.string.app_name
}

@Composable
fun SettingsScreen(
    modifier: Modifier = Modifier,
    viewModel: SettingsViewModel = viewModel(factory = AppViewModelProvider.Factory)
) {
    val coroutineScope = rememberCoroutineScope()
    var showInformationDialog by rememberSaveable { mutableStateOf(false) }
    var informationText by rememberSaveable { mutableStateOf("") }

    Scaffold(
        modifier = modifier
    ) { innerPadding ->
        SettingsEntryBody(
            onTestClick = {
                coroutineScope.launch {
                    val result = viewModel.checkConnection()
                    informationText = if (result) {
                        "Соединение успешно установлено"
                    } else {
                        "Нет доступа к серверу"
                    }
                    showInformationDialog = true
                }
            },
            modifier = Modifier.padding(innerPadding)
        )
    }

    if (showInformationDialog) {
        InformationDialog(
            text = informationText,
            onConfirm = {
                showInformationDialog = false
            },
            modifier = Modifier.padding(dimensionResource(id = R.dimen.padding_medium))
        )
    }
}

@Composable
fun SettingsEntryBody(
    onTestClick: () -> Unit,
    modifier: Modifier = Modifier
) {
    Column(
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally,
        modifier = modifier.fillMaxSize()
    ) {
        Button(
            onClick = onTestClick,
            shape = MaterialTheme.shapes.small
        ) {
            Text(text = stringResource(R.string.test_action))
        }
    }
}

@Composable
private fun InformationDialog(
    text: String,
    onConfirm: () -> Unit,
    modifier: Modifier = Modifier
) {
    AlertDialog(onDismissRequest = { /* Do nothing */ },
        title = { Text(stringResource(R.string.attention)) },
        text = { Text(text) },
        modifier = modifier,
        confirmButton = {
            TextButton(onClick = onConfirm) {
                Text(text = stringResource(R.string.yes))
            }
        })
}

@Preview(showBackground = true)
@Composable
fun SettingsScreenPreview() {
    JasmineFastenerDepartmentTheme {
        SettingsEntryBody(onTestClick = { })
    }
}