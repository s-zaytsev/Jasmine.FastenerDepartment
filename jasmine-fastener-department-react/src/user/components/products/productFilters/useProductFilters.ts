import {changeQuery, getPageFilters, getProducts} from "../../../slices/ProductsSlice.ts";
import {PriceTagCode, type ProductPageFilters, type ProductsQuery} from "../../../models/productModel.ts";
import {useAppDispatch} from "../../../../shared/hooks/sharedHooks.ts";
import {useCallback} from "react";

const useProductFilters = (query: ProductsQuery, filters: ProductPageFilters) => {

    const dispatch = useAppDispatch();

    const handleReload = useCallback((query: ProductsQuery) => {
        dispatch(getPageFilters(query));
        dispatch(getProducts(query));
    }, [dispatch]);

    const handleSupplierFilterChange = useCallback((id: string, isEnabled: boolean) => {
        let suppliers = query.suppliers || [];
        if (suppliers.find(x => x === id)) {
            suppliers = suppliers.filter(x => x !== id);
        } else if (id === null && !isEnabled) {
            suppliers = suppliers.filter(x => x !== null);
        } else if (isEnabled) {
            suppliers = [...suppliers, id]
        }

        const newQuery = {...query, suppliers, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handleTypeFilterChange = useCallback((id: string, isEnabled: boolean) => {
        let types = query.types || [];
        if (types.find(x => x === id)) {
            types = types.filter(x => x !== id);
        } else if (id === null && !isEnabled) {
            types = types.filter(x => x !== null);
        } else if (isEnabled) {
            types = [...types, id]
        }

        const newQuery = {...query, types, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handlePriceTagFilterChange = useCallback((id: PriceTagCode, isEnabled: boolean) => {
        let priceTags = query.priceTags || [];
        if (priceTags.find(x => x === id)) {
            priceTags = priceTags.filter(x => x !== id);
        } else if (isEnabled) {
            priceTags = [...priceTags, id]
        }

        const newQuery = {...query, priceTags, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handleOnlyToPrintFilterChange = useCallback((isEnabled: boolean) => {
        const newQuery = {...query, onlyToPrint: isEnabled, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handleOnlyToOrderFilterChange = useCallback((isEnabled: boolean) => {
        const newQuery = {...query, onlyToOrder: isEnabled, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handlePriceRangeChange = useCallback((from: number, to: number) => {
        const newQuery = {...query, priceFrom: from, priceTo: to, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handleResetPriceRange = useCallback(() => {
        const newQuery = {
            ...query,
            priceFrom: filters.priceRange?.min || 0,
            priceTo: filters.priceRange?.max || 0,
            pageNo: 1
        };
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, filters.priceRange?.max, filters.priceRange?.min, handleReload, query]);

    const handleResetTypeFilters = useCallback(() => {
        const newQuery = {...query, types: [], pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handleResetSupplierFilters = useCallback(() => {
        const newQuery = {...query, suppliers: [], pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handleResetPriceTagFilters = useCallback(() => {
        const newQuery = {...query, priceTags: [], pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handleResetOnlyToPrintFilter = useCallback(() => {
        const newQuery = {...query, onlyToPrint: false, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handleResetOnlyToOrderFilter = useCallback(() => {
        const newQuery = {...query, onlyToOrder: false, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    const handleSearch = useCallback((value: string, keepPageNo?: boolean) => {
        const newQuery = {...query, search: value, pageNo: keepPageNo ? query.pageNo : 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, handleReload, query]);

    return {
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
    };
}

export default useProductFilters;