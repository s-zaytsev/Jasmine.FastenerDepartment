import Page from "../../../shared/components/layout/Page.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import SupplierDialog from "./SupplierDialog.tsx";
import SuppliersGrid from "./SuppliersGrid.tsx";
import useSuppliersPage from "./useSuppliersPage.ts";

const SuppliersPage = () => {

    const {
        open,
        handleOpenDialogToCreate,
        handleOpenDialogToChange,
        handleCreate,
        handleChange,
        handleClose,
        handleNavigateToSupplierProducts,
        loading,
        suppliers,
        selectedSupplier
    } = useSuppliersPage();

    if (loading) {
        return <Loader text={'Загрузка данных о поставщиках'}/>;
    }

    return (
        <Page
            title={'Поставщики'}
            description={'Список доступных поставщиков'}
            button={{
                label: 'Создать',
                onClick: handleOpenDialogToCreate
            }}
        >
            <SuppliersGrid
                suppliers={suppliers}
                onEdit={handleOpenDialogToChange}
                onNavigateToSupplierProducts={handleNavigateToSupplierProducts}/>

            <SupplierDialog
                supplier={selectedSupplier}
                open={open}
                onClose={handleClose}
                onSubmit={!selectedSupplier ? handleCreate : handleChange}
            />
        </Page>
    )
}

export default SuppliersPage;