import Page from "../../../shared/components/layout/Page.tsx";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";
import {Box} from "@mui/material";
import Loader from "../../../shared/components/Loader.tsx";
import SupplierDialog from "./SupplierDialog.tsx";
import SuppliersGrid from "./SuppliersGrid.tsx";
import useSuppliersPage from "./useSuppliersPage.ts";
import {useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import type {SuppliersPageState} from "../../models/supplierModels.ts";

const SuppliersPage = () => {

    const state = useAppSelector<SuppliersPageState>(
        (state) => state.suppliers
    );

    const {
        open,
        handleOpenDialogToCreate,
        handleOpenDialogToChange,
        handleCreate,
        handleChange,
        handleClose,
        handleNavigateToSupplierProducts,
    } = useSuppliersPage();

    if (state.loading) {
        return <Loader text={'Загрузка данных о поставщиках'}/>;
    }

    return (
        <Page>
            <Box className={"flex justify-between w-full mb-[1rem]"}>
                <Box></Box>
                <FilledButton onClick={handleOpenDialogToCreate} variant="contained">
                    Добавить
                </FilledButton>
            </Box>

            <SuppliersGrid
                suppliers={state.suppliers}
                onEdit={handleOpenDialogToChange}
                onNavigateToSupplierProducts={handleNavigateToSupplierProducts}/>

            <SupplierDialog
                supplier={state.selectedSupplier}
                open={open}
                onClose={handleClose}
                onSubmit={!state.selectedSupplier ? handleCreate : handleChange}
            />
        </Page>
    )
}

export default SuppliersPage;