import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {ChangeCompany, ChangeCompanyPageState} from "../../../models/companyModels.ts";
import {useNavigate} from "react-router-dom";
import {useCallback} from "react";
import {createCompany} from "../../../slices/CreateCompanySlice.ts";

const useCreateCompanyPage = () => {
    const state = useAppSelector<ChangeCompanyPageState>(
        (state) => state.changeCompany
    );

    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const handleSubmit = useCallback(async (model: ChangeCompany) => {
        await dispatch(createCompany(model)).unwrap();
        navigate('/companies');
    }, [dispatch, navigate]);

    return {
        model: state.model,
        loading: state.loading,
        handleSubmit
    };
}

export default useCreateCompanyPage;