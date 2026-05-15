package com.szaytsev.jasminefastenerdepartment

import android.annotation.SuppressLint
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.automirrored.filled.ArrowBack
import androidx.compose.material.icons.filled.Clear
import androidx.compose.material3.CenterAlignedTopAppBar
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.OutlinedTextFieldDefaults
import androidx.compose.material3.ShapeDefaults
import androidx.compose.material3.Text
import androidx.compose.material3.TopAppBarScrollBehavior
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.navigation.NavHostController
import androidx.navigation.compose.rememberNavController
import com.szaytsev.jasminefastenerdepartment.ui.navigation.JasmineNavHost
import com.szaytsev.jasminefastenerdepartment.ui.theme.JasmineFastenerDepartmentTheme

@SuppressLint("UnusedMaterial3ScaffoldPaddingParameter")
@Composable
fun JasmineApp(navController: NavHostController = rememberNavController()) {
    JasmineNavHost(navController = navController)
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun JasmineTopAppBar(
    title: String,
    canNavigateBack: Boolean,
    modifier: Modifier = Modifier,
    scrollBehavior: TopAppBarScrollBehavior? = null,
    navigateUp: () -> Unit = {}
) {
    CenterAlignedTopAppBar(
        title = { Text(title) },
        modifier = modifier,
        scrollBehavior = scrollBehavior,
        navigationIcon = {
            if (canNavigateBack) {
                IconButton(onClick = navigateUp) {
                    Icon(
                        imageVector = Icons.AutoMirrored.Filled.ArrowBack,
                        contentDescription = stringResource(R.string.back_button)
                    )
                }
            }
        }
    )
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun JasmineTopAppBar(
    filter: String,
    onValueChange: (String) -> Unit,
    modifier: Modifier = Modifier,
    scrollBehavior: TopAppBarScrollBehavior? = null,
) {
    CenterAlignedTopAppBar(
        scrollBehavior = scrollBehavior,
        modifier = modifier.padding(5.dp),
        title = {
            OutlinedTextField(
                value = filter,
                textStyle = TextStyle(fontSize = 16.sp),
                onValueChange = {
                    onValueChange(it)
                },
                keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Text),
                label = { Text(stringResource(R.string.product_name_req)) },
                trailingIcon = {
                    if (filter.isNotBlank()) {
                        Icon(
                            imageVector = Icons.Default.Clear,
                            contentDescription = null,
                            modifier = Modifier.clickable { onValueChange("") })
                    }
                },
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(vertical = 0.dp)
                ,
                enabled = true,
                singleLine = true,
                shape = ShapeDefaults.Medium
            )
        })
}

@OptIn(ExperimentalMaterial3Api::class)
@Preview(showBackground = true)
@Composable
fun JasmineTopAppBarPreview() {
    JasmineFastenerDepartmentTheme {
        JasmineTopAppBar(filter = "Test", onValueChange = {})
    }
}

@OptIn(ExperimentalMaterial3Api::class)
@Preview(showBackground = true)
@Composable
fun JasmineTopAppBar1Preview() {
    JasmineFastenerDepartmentTheme {
        JasmineTopAppBar(filter = "", onValueChange = {})
    }
}
