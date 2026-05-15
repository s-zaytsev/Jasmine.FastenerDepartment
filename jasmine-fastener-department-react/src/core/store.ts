import {configureStore} from "@reduxjs/toolkit";
import productsSlice from "../user/slices/ProductsSlice.ts";
import changeProductSlice from "../user/slices/ChangeProductSlice.ts";
import printSlice from "../user/slices/PrintSlice.ts";
import ordersSlice from "../user/slices/OrdersSlice.ts";
import productsHistorySlice from "../user/slices/ProductsHistorySlice.ts";
import createProductSlice from "../user/slices/CreateProductSlice.ts";
import exportSlice from "../user/slices/ExportSlice.ts";
import suppliersSlice from "../user/slices/SuppliersSlice.ts";
import supplierProductsSlice from "../user/slices/SupplierProductsSlice.ts";
import createOrderSlice from "../user/slices/CreateOrderSlice.ts";
import changeOrderSlice from "../user/slices/ChangeOrderSlice.ts";
import completeOrderSlice from "../user/slices/CompleteOrderSlice.ts";
import productTypesSlice from "../user/slices/ProductTypesSlice.ts";
import orderDetailsSlice from "../user/slices/OrderDetailsSlice.ts";

export const store = configureStore({
    reducer: {
        products: productsSlice,
        createProduct: createProductSlice,
        changeProduct: changeProductSlice,
        print: printSlice,
        orders: ordersSlice,
        createOrder: createOrderSlice,
        changeOrder: changeOrderSlice,
        completeOrder: completeOrderSlice,
        orderDetails: orderDetailsSlice,
        history: productsHistorySlice,
        export: exportSlice,
        suppliers: suppliersSlice,
        supplierProducts: supplierProductsSlice,
        productTypes: productTypesSlice
    }
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch