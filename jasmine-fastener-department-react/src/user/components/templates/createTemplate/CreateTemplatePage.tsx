import Page from "../../../../shared/components/layout/Page.tsx";
import useCreateTemplatePage from "./useCreateTemplatePage.ts";
import TemplateForm from "../shared/TemplateForm.tsx";
import Loader from "../../../../shared/components/Loader.tsx";

const CreateTemplatePage = () => {
    const {
        model,
        types,
        columns,
        preview,
        loading,
        isSubmitting,
        handleCreateTemplate,
        handleUpdatePreview,
    } = useCreateTemplatePage();

    if (loading) {
        return <Loader />;
    }

    return (
        <Page
            title={'Создание шаблона'}
            description={'Выбор типа шаблона и настройка параметров'}
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
                onSubmit={handleCreateTemplate}
            />
        </Page>
    );
}

export default CreateTemplatePage;