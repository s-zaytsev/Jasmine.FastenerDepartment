import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {ChangeCompany, ChangeCompanyPageState} from "../../../models/companyModels.ts";
import {useNavigate, useParams} from "react-router-dom";
import {useCallback, useEffect} from "react";
import {changeCompany, getCompany} from "../../../slices/ChangeCompanySlice.ts";

const useChangeCompanyPage = () => {
    const state = useAppSelector<ChangeCompanyPageState>(
        (state) => state.changeCompany
    );

    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const params = useParams();

    const handleSubmit = useCallback(async (model: ChangeCompany) => {
        await dispatch(changeCompany({id: params.id, model: model})).unwrap();
        navigate('/companies');
    }, [dispatch, navigate, params.id]);

    useEffect(() => {
        if (!params.id) {
            navigate('/companies')
            return;
        }

        dispatch(getCompany(params.id));
    }, [params.id, dispatch, navigate]);

    return {
        model: state.model,
        loading: state.loading,
        handleSubmit
    };
}

export default useChangeCompanyPage;