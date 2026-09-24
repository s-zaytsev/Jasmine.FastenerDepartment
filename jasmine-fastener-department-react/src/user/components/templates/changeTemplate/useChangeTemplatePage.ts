import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {ChangeTemplate, ChangeTemplatePageState} from "../../../models/templateModels.ts";
import {useNavigate, useParams} from "react-router-dom";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import {useCallback, useEffect} from "react";
import {
    changeTemplate,
    getPreview,
    getTableColumns,
    getTemplate,
    getTypes
} from "../../../slices/ChangeTemplateSlice.ts";

const useChangeTemplatePage = () => {
    const state = useAppSelector<ChangeTemplatePageState>(
        (state) => state.changeTemplate
    );

    const params = useParams();
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const notification = useNotify();

    const handleChangeTemplate = useCallback(async (model: ChangeTemplate) => {
        try {
            await dispatch(changeTemplate({id: params.id, model}));
            navigate('/templates');
        } catch (error: any) {
            notification.notifyError(error.message);
        }
    }, [dispatch, navigate]);

    const handleUpdatePreview = useCallback((model: ChangeTemplate) => {
        dispatch(getPreview(model));
    }, [dispatch]);

    useEffect(() => {
        if (!params.id) {
            navigate('/templates');
        }

        dispatch(getTypes());
        dispatch(getTableColumns());
        dispatch(getTemplate(params.id!));
    }, [dispatch, params.id, navigate]);

    useEffect(() => {
        dispatch(getPreview(state.model));
    }, [dispatch, state.model]);

    return {
        model: state.model,
        types: state.types,
        columns: state.columnGroups,
        preview: state.preview,
        handleChangeTemplate,
        handleUpdatePreview,
        loading: state.loading
    };
}

export default useChangeTemplatePage;