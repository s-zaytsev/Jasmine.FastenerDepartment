package com.szaytsev.jasminefastenerdepartment.ui.shared

import androidx.compose.material3.LocalTextStyle
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.tooling.preview.Preview
import com.szaytsev.jasminefastenerdepartment.data.models.ProductMeasurementUnitCode

@Composable
fun ProductPrice(
    value: Double,
    unitCode: ProductMeasurementUnitCode,
    modifier: Modifier = Modifier,
    style: TextStyle = LocalTextStyle.current
) {
    Text(
        "${"%.2f".format(value)} руб./${
            stringResource(
                ProductMeasurementUnitCode.getShortDisplayNameResourceId(
                    unitCode
                )
            )
        }.",
        modifier = modifier,
        style = style
    )
}

@Composable
@Preview(showBackground = true)
fun ProductPricePreview() {
    ProductPrice(30.0, ProductMeasurementUnitCode.Sets)
}