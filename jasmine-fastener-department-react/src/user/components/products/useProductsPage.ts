import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {useNavigate} from "react-router-dom";
import {useNotify} from "../../../shared/providers/NotificationProvider.tsx";
import {useCallback, useEffect} from "react";
import {
    changeNeededOrderStatus,
    changeNeededPrintStatus,
    changeOrderStatus,
    changePrintStatus,
    changeQuery,
    getPageFilters,
    getProducts,
    getProductTypes
} from "../../slices/ProductsSlice.ts";
import {type ProductsPageState, type ProductsQuery} from "../../models/productModel.ts";
import {NotificationMessage} from "../../../shared/models/notificationModel.ts";
import useProductFilters from "./productFilters/useProductFilters.ts";

const useProductsPage = () => {

    const state = useAppSelector<ProductsPageState>(
        (state) => state.products
    );

    const {
        handleSupplierFilterChange,
        handleTypeFilterChange,
        handlePriceTagFilterChange,
        handleOnlyToPrintFilterChange,
        handleOnlyToOrderFilterChange,
        handlePriceRangeChange,
        handleResetOnlyToOrderFilter,
        handleResetPriceRange,
        handleResetTypeFilters,
        handleResetSupplierFilters,
        handleResetOnlyToPrintFilter,
        handleResetPriceTagFilters,
        handleSearch
    } = useProductFilters(state.query, state.filters);

    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const notification = useNotify();

    const handleNavigateToCreate = useCallback(() => {
        navigate('create');
    }, [navigate]);

    const handleNavigateToProduct = useCallback((id: string) => {
        navigate(id)
    }, [navigate]);

    const handleChangePrintStatus = useCallback((id: string) => {
        dispatch(changeNeededPrintStatus({id}));
        dispatch(changePrintStatus(id));
    }, [dispatch]);

    const handleChangeOrderStatus = useCallback((id: string) => {
        dispatch(changeNeededOrderStatus({id}));
        dispatch(changeOrderStatus(id));
    }, [dispatch]);

    const handleSort = useCallback((parameter: number) => {
        const sortDesc = state.query.sortBy !== parameter ? false : !state.query.sortDesc;
        const newQuery = {...state.query, sortBy: parameter, sortDesc: sortDesc, pageNo: 1};

        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, state.query.sortBy, state.query.sortDesc]);

    const handlePageSizeChange = useCallback((size: number) => {
        const newQuery = {...state.query, pageNo: 1, pageSize: size};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, state.query.pageSize]);

    const handlePageNoChange = useCallback((pageNo: number) => {
        const newQuery = {...state.query, pageNo: pageNo};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, state.query.pageNo]);

    const handleReload = useCallback((query: ProductsQuery) => {
        dispatch(getProducts(query));
    }, [dispatch]);

    useEffect(() => {
        if (state.error) {
            const message = NotificationMessage.error(state.error!).message;
            notification.notifyError(message!);
        }
    }, [notification, state.error]);

    useEffect(() => {
        dispatch(getProductTypes());
        dispatch(getPageFilters(state.query));
    }, [dispatch]);

    return {
        page: state.page,
        query: state.query,
        filters: state.filters,
        productTypes: state.productTypes,
        loading: state.loading,
        handleNavigateToCreate,
        handleNavigateToProduct,
        handleChangePrintStatus,
        handleChangeOrderStatus,
        handleSort,
        handlePageSizeChange,
        handlePageNoChange,
        handleSearch,
        handleSupplierFilterChange,
        handleTypeFilterChange,
        handlePriceTagFilterChange,
        handleOnlyToPrintFilterChange,
        handleOnlyToOrderFilterChange,
        handlePriceRangeChange,
        handleResetOnlyToOrderFilter,
        handleResetPriceRange,
        handleResetTypeFilters,
        handleResetSupplierFilters,
        handleResetOnlyToPrintFilter,
        handleResetPriceTagFilters
    }
}

export default useProductsPage;