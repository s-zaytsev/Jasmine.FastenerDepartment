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
import SettingsPage from "./components/settings/SettingsPage.tsx";
import CompaniesPage from "./components/companies/CompaniesPage.tsx";
import ChangeCompanyPage from "./components/companies/changeCompany/ChangeCompanyPage.tsx";
import CreateCompanyPage from "./components/companies/createCompany/CreateCompanyPage.tsx";
import TemplatesPage from "./components/templates/TemplatesPage.tsx";
import CreateTemplatePage from "./components/templates/createTemplate/CreateTemplatePage.tsx";
import ChangeTemplatePage from "./components/templates/changeTemplate/ChangeTemplatePage.tsx";
import RecipientsPage from "./components/recipients/RecipientsPage.tsx";

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
    <Route path="settings" element={<SettingsPage/>} key="settings"/>,
    <Route path="companies" element={<CompaniesPage/>} key="companies"/>,
    <Route path="companies/create" element={<CreateCompanyPage/>} key="create-company"/>,
    <Route path="companies/:id" element={<ChangeCompanyPage/>} key="change-company"/>,
    <Route path="templates" element={<TemplatesPage/>} key="templates"/>,
    <Route path="templates/create" element={<CreateTemplatePage/>} key="create-template"/>,
    <Route path="templates/:id" element={<ChangeTemplatePage/>} key="change-template"/>,
    <Route path="recipients" element={<RecipientsPage/>} key="recipients"/>,
    <Route index element={<Navigate to="products"/>} key="index"/>,
    <Route path="*" element={<Navigate to=""/>} key="wildcard"/>,
];