import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";
import {getDailyHistory, getProductTypes} from "../../slices/ProductsHistorySlice.ts";
import type {HistoryPageState} from "../../models/historyModels.ts";

const useHistoryPage = () => {

    const state = useAppSelector<HistoryPageState>(
        (state) => state.history
    );

    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        dispatch(getDailyHistory());
        dispatch(getProductTypes());
    }, [dispatch]);

    const navigateToProduct = (id: string) => {
        navigate(`/products/${id}`);
    }

    return {
        navigateToProduct,
        loading: state.loading,
        items: state.items,
        productTypes: state.productTypes
    };
};

export default useHistoryPage;