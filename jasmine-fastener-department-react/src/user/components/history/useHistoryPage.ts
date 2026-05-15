import {useAppDispatch} from "../../../shared/hooks/sharedHooks.ts";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";
import {getDailyHistory, getProductTypes} from "../../slices/ProductsHistorySlice.ts";

const useHistoryPage = () => {
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
        navigateToProduct
    };
};

export default useHistoryPage;