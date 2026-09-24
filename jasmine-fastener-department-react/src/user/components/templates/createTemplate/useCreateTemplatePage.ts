import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {ChangeTemplate, CreateTemplatePageState} from "../../../models/templateModels.ts";
import {useNavigate} from "react-router-dom";
import {createTemplate, getPreview, getTableColumns, getTypes} from "../../../slices/CreateTemplateSlice.ts";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import {useCallback, useEffect} from "react";

const useCreateTemplatePage = () => {
    const state = useAppSelector<CreateTemplatePageState>(
        (state) => state.createTemplate
    );

    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const notification = useNotify();

    const handleCreateTemplate = useCallback(async (model: ChangeTemplate) => {
        try {
            await dispatch(createTemplate(model));
            navigate('/templates');
        } catch (error: any) {
            notification.notifyError(error.message);
        }
    }, [dispatch, navigate]);

    const handleUpdatePreview = useCallback((model: ChangeTemplate) => {
        dispatch(getPreview(model));
    }, [dispatch]);

    useEffect(() => {
        dispatch(getTypes());
        dispatch(getTableColumns());
    }, [dispatch]);

    return {
        model: state.model,
        types: state.types,
        columns: state.columnGroups,
        preview: state.preview,
        handleCreateTemplate,
        handleUpdatePreview,
        loading: state.loading
    };
}

export default useCreateTemplatePage;