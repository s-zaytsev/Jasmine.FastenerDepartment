import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {OrderDetailsPageState} from "../../../models/orderModels.ts";
import {useNavigate, useParams} from "react-router-dom";
import {downloadDocument, getOrder, getProductTypes} from "../../../slices/OrderDetailsSlice.ts";
import {useEffect} from "react";

const useOrderDetailsPage = () => {

    const state = useAppSelector<OrderDetailsPageState>(
        (state) => state.orderDetails
    );
    const dispatch = useAppDispatch();
    const params = useParams();
    const navigate = useNavigate();

    function handleDownload() {
        dispatch(downloadDocument(params.id!));
    }

    useEffect(() => {
        if (!params.id) {
            navigate('/orders');
        }
        dispatch(getOrder(params.id!));
        dispatch(getProductTypes());
    }, [dispatch, params.id]);

    return {
        order: state.order,
        productTypes: state.productTypes,
        loading: state.loading,
        handleDownload
    };
}

export default useOrderDetailsPage;