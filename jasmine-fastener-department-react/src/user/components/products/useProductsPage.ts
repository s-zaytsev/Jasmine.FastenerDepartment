import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {useNavigate} from "react-router-dom";
import {useNotify} from "../../../shared/providers/NotificationProvider.tsx";
import {useEffect} from "react";
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
import {type ProductsPageState} from "../../models/productModel.ts";
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
        handleResetPriceTagFilters
    } = useProductFilters(state.query, state.filters);

    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const notification = useNotify();

    function handleNavigateToCreate() {
        navigate('create');
    }

    function handleNavigateToProduct(id: string) {
        navigate(id)
    }

    function handleChangePrintStatus(id: string) {
        dispatch(changeNeededPrintStatus({id}));
        dispatch(changePrintStatus(id));
    }

    function handleChangeOrderStatus(id: string) {
        dispatch(changeNeededOrderStatus({id}));
        dispatch(changeOrderStatus(id));
    }

    const handleSort = (parameter: number) => {
        const sortDesc = state.query.sortBy !== parameter ? false : !state.query.sortDesc;
        const newQuery = {...state.query, sortBy: parameter, sortDesc: sortDesc, pageNo: 1};

        dispatch(changeQuery(newQuery))
    };

    const handlePageSizeChange = async (size: number) => {
        const newQuery = {...state.query, pageNo: 1, pageSize: size};
        dispatch(changeQuery(newQuery));
    };

    const handlePageNoChange = async (pageNo: number) => {
        const newQuery = {...state.query, pageNo: pageNo};
        dispatch(changeQuery(newQuery));
    };

    const handleSearch = (value: string) => {
        const newQuery = {...state.query, search: value, pageNo: 1};
        dispatch(changeQuery(newQuery));
    };

    useEffect(() => {
        if (state.error) {
            const message = NotificationMessage.error(state.error!).message;
            notification.notifyError(message!);
        }
    }, [notification, state.error]);

    useEffect(() => {
        dispatch(getProductTypes());
    }, [dispatch]);

    useEffect(() => {
        dispatch(getPageFilters(state.query));
        dispatch(getProducts(state.query));
    }, [dispatch, state.query]);

    return {
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