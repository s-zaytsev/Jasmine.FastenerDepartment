import api from "../../core/api.ts";
import {
    type ChangeTemplate,
    type Template,
    type TemplateContentTableColumnGroup,
    type TemplateType
} from "../models/templateModels.ts";

class TemplatesApi {
    getTemplates(): Promise<Template[]> {
        return api.get<Promise<Template[]>>(`/templates`)
            .then(x => x.data);
    }

    getTemplate(id: string): Promise<Template> {
        return api.get<Promise<Template>>(`/templates/${id}`)
            .then(x => x.data);
    }

    getTypes(): Promise<TemplateType[]> {
        return api.get<Promise<TemplateType[]>>('templates/types')
            .then(x => x.data);
    }

    getTableColumns(): Promise<TemplateContentTableColumnGroup[]> {
        return api.get<Promise<TemplateContentTableColumnGroup[]>>('templates/content-table-columns')
            .then(x => x.data);
    }

    createTemplate(model: ChangeTemplate): Promise<void> {
        return api.post<Promise<void>>('templates', model)
            .then(x => x.data);
    }

    changeTemplate(id: string, model: ChangeTemplate): Promise<void> {
        return api.put<Promise<void>>(`templates/${id}`, model)
            .then(x => x.data);
    }
}

export default new TemplatesApi();