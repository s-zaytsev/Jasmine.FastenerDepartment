import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import type {CompaniesPageState} from "../../models/companyModels.ts";
import {useCallback, useEffect} from "react";
import {getCompanies} from "../../slices/CompaniesSlice.ts";
import {useNavigate} from "react-router-dom";

const useCompaniesPage = () => {
    const state = useAppSelector<CompaniesPageState>(
        (state) => state.companies
    );

    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const handleCreateCompany = useCallback(() => {
        navigate('create');
    }, [navigate])

    const handleChangeCompany = useCallback((id: string) => {
        navigate(id);
    }, [navigate]);

    useEffect(() => {
        dispatch(getCompanies());
    }, [dispatch]);

    return {
        companies: state.companies,
        loading: state.loading,
        handleCreateCompany,
        handleChangeCompany
    };
}

export default useCompaniesPage;