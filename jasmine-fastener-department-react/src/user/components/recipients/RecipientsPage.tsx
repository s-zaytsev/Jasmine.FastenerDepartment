import Loader from "../../../shared/components/Loader.tsx";
import Page from "../../../shared/components/layout/Page.tsx";
import RecipientDialog from "./RecipientDialog.tsx";
import useRecipientsPage from "./useRecipientsPage.ts";
import RecipientsGrid from "./RecipientsGrid.tsx";

const ProductTypesPage = () => {

    const {
        open,
        handleOpenDialogToCreate,
        handleOpenDialogToChange,
        handleChange,
        handleCreate,
        handleClose,
        loading,
        recipients,
        selected
    } = useRecipientsPage();

    if (loading) {
        return <Loader text={'Загрузка контактов'}/>;
    }

    return (
        <Page
            title={'Контакты'}
            description={'Список доступных контактов'}
            button={{
                label: 'Создать',
                onClick: handleOpenDialogToCreate
            }}
        >
            <RecipientsGrid
                recipients={recipients}
                onEdit={handleOpenDialogToChange}/>

            <RecipientDialog
                recipient={selected}
                open={open}
                onClose={handleClose}
                onSubmit={!selected ? handleCreate : handleChange}
            />
        </Page>
    )
}

export default ProductTypesPage;