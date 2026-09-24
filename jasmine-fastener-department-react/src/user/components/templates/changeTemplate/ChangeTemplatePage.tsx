import Page from "../../../../shared/components/layout/Page.tsx";
import TemplateForm from "../shared/TemplateForm.tsx";
import useChangeTemplatePage from "./useChangeTemplatePage.ts";

const ChangeTemplatePage = () => {
    const {
        model,
        types,
        columns,
        preview,
        handleChangeTemplate,
        handleUpdatePreview,
    } = useChangeTemplatePage();

    return (
        <Page
            title={'Редактирование шаблона'}
            description={'Редактирование параметров'}
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
                onSubmit={handleChangeTemplate}
            />
        </Page>
    );
}

export default ChangeTemplatePage;