import Page from "../../../../shared/components/layout/Page.tsx";
import useCreateTemplatePage from "./useCreateTemplatePage.ts";
import TemplateForm from "../shared/TemplateForm.tsx";

const CreateTemplatePage = () => {
    const {
        model,
        types,
        columns,
        preview,
        handleCreateTemplate,
        handleUpdatePreview,
    } = useCreateTemplatePage();

    return (
        <Page
            title={'Создание шаблона'}
            description={'Выбор типа шаблона и настройка параметров'}
            button={{
                label: 'Сохранить',
                type: "submit",
                formId: 'template-change-form'
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