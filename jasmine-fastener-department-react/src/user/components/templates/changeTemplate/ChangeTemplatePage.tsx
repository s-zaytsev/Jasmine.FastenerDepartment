import Page from "../../../../shared/components/layout/Page.tsx";
import TemplateForm from "../shared/TemplateForm.tsx";
import useChangeTemplatePage from "./useChangeTemplatePage.ts";
import Loader from "../../../../shared/components/Loader.tsx";

const ChangeTemplatePage = () => {
    const {
        model,
        types,
        columns,
        preview,
        loading,
        isSubmitting,
        handleChangeTemplate,
        handleUpdatePreview,
    } = useChangeTemplatePage();

    if (loading) {
        return <Loader text={`Загрузка шаблона`}/>;
    }

    return (
        <Page
            title={'Редактирование шаблона'}
            description={'Редактирование параметров'}
            button={{
                label: 'Сохранить',
                type: "submit",
                formId: 'template-change-form',
                isLoading: isSubmitting
            }}
        >
            <TemplateForm
                model={model}
                types={types}
                columnGroups={columns}
                preview={preview}
                onUpdatePreview={handleUpdatePreview}
                onSubmit={handleChangeTemplate}
            />
        </Page>
    );
}

export default ChangeTemplatePage;