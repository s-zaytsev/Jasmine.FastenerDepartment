import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {useCallback, useEffect} from "react";
import type {TemplatesPageState} from "../../models/templateModels.ts";
import {getTemplates} from "../../slices/TemplatesSlice.ts";
import {useNavigate} from "react-router-dom";

const useTemplatesPage = () => {
    const state = useAppSelector<TemplatesPageState>(
        (state) => state.templates
    );

    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const handleNavigateToCreate = useCallback(() => {
        navigate('create');
    }, [navigate]);

    const handleNavigateToChange = useCallback((id: string) => {
        navigate(id);
    }, [navigate]);

    useEffect(() => {
        dispatch(getTemplates());
    }, [dispatch]);

    return {
        templates: state.templates,
        handleNavigateToCreate,
        handleNavigateToChange,
        loading: state.loading
    };
}

export default useTemplatesPage;