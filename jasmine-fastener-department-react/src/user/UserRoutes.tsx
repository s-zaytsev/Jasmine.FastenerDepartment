import {Navigate, Route} from "react-router-dom";
import ExportPage from "./components/export/ExportPage";
import HistoryPage from "./components/history/HistoryPage";
import OrdersPage from "./components/orders/orders-page/OrdersPage.tsx";
import PrintPage from "./components/print/PrintPage";
import ChangeProductPage from "./components/products/changeProduct/ChangeProductPage";
import CreateProductPage from "./components/products/createProduct/CreateProductPage";
import ProductsPage from "./components/products/ProductsPage";
import SuppliersPage from "./components/suppliers/SuppliersPage.tsx";
import SupplierProductsPage from "./components/suppliers/SupplierProductsPage.tsx";
import CreateOrderPage from "./components/orders/create-order-page/CreateOrderPage.tsx";
import ChangeOrderPage from "./components/orders/change-order-page/ChangeOrderPage.tsx";
import CompleteOrderPage from "./components/orders/complete-order/CompleteOrderPage.tsx";
import ProductTypesPage from "./components/productTypes/ProductTypesPage.tsx";
import OrderDetailsPage from "./components/orders/order-details/OrderDetailsPage.tsx";

export const userRoutes = [
    <Route path="products" element={<ProductsPage/>} key="products"/>,
    <Route path="products/create" element={<CreateProductPage/>} key="create-product"/>,
    <Route path="products/:id" element={<ChangeProductPage/>} key="change-product"/>,
    <Route path="history" element={<HistoryPage/>} key="history"/>,
    <Route path="orders" element={<OrdersPage/>} key="orders"/>,
    <Route path="orders/:id" element={<OrderDetailsPage/>} key="order-details"/>,
    <Route path="orders/create" element={<CreateOrderPage/>} key="create-order"/>,
    <Route path="orders/:id/change" element={<ChangeOrderPage/>} key="change-order"/>,
    <Route path="orders/:id/complete" element={<CompleteOrderPage/>} key="complete-order"/>,
    <Route path="print" element={<PrintPage/>} key="print"/>,
    <Route path="export" element={<ExportPage/>} key="export"/>,
    <Route path="suppliers" element={<SuppliersPage/>} key="suppliers"/>,
    <Route path="suppliers/:id" element={<SupplierProductsPage/>} key="supplier"/>,
    <Route path="product-types" element={<ProductTypesPage/>} key="product-types"/>,
    <Route index element={<Navigate to="products"/>} key="index"/>,
    <Route path="*" element={<Navigate to=""/>} key="wildcard"/>,
];