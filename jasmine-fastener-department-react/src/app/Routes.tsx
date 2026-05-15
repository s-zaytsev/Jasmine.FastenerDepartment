import { Routes, Route } from "react-router-dom";
import RootLayout from "../shared/components/layout/RootLayout.tsx";
import ErrorPage from "../shared/components/layout/ErrorPage.tsx";
import { userRoutes } from "../user/UserRoutes";
export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/" element={<RootLayout />} errorElement={<ErrorPage />}>
                {userRoutes}
            </Route>
        </Routes>
    );
}