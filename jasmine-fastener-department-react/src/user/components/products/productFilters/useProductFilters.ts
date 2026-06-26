import {changeQuery, getPageFilters, getProducts} from "../../../slices/ProductsSlice.ts";
import {PriceTagCode, type ProductPageFilters, type ProductsQuery} from "../../../models/productModel.ts";
import {useAppDispatch} from "../../../../shared/hooks/sharedHooks.ts";
import {useCallback} from "react";

const useProductFilters = (query: ProductsQuery, filters: ProductPageFilters) => {

    const dispatch = useAppDispatch();

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
    }, [dispatch, query.suppliers]);

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
    }, [dispatch, query.types]);

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
    }, [dispatch, query.priceTags]);

    const handleOnlyToPrintFilterChange = useCallback((isEnabled: boolean) => {
        const newQuery = {...query, onlyToPrint: isEnabled, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, query.onlyToPrint]);

    const handleOnlyToOrderFilterChange = useCallback((isEnabled: boolean) => {
        const newQuery = {...query, onlyToOrder: isEnabled, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, query.onlyToOrder]);

    const handlePriceRangeChange = useCallback((from: number, to: number) => {
        const newQuery = {...query, priceFrom: from, priceTo: to, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, query.priceFrom, query.priceTo]);

    const handleResetPriceRange = useCallback(() => {
        const newQuery = {
            ...query,
            priceFrom: filters.priceRange?.min || 0,
            priceTo: filters.priceRange?.max || 0,
            pageNo: 1
        };
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, query.priceFrom, query.priceTo]);

    const handleResetTypeFilters = useCallback(() => {
        const newQuery = {...query, types: [], pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, query.types]);

    const handleResetSupplierFilters = useCallback(() => {
        const newQuery = {...query, suppliers: [], pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, query.suppliers]);

    const handleResetPriceTagFilters = useCallback(() => {
        const newQuery = {...query, priceTags: [], pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, query.priceTags]);

    const handleResetOnlyToPrintFilter = useCallback(() => {
        const newQuery = {...query, onlyToPrint: false, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, query.onlyToPrint]);

    const handleResetOnlyToOrderFilter = useCallback(() => {
        const newQuery = {...query, onlyToOrder: false, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, query.onlyToOrder]);

    const handleSearch = useCallback((value: string) => {
        const newQuery = {...query, search: value, pageNo: 1};
        dispatch(changeQuery(newQuery));
        handleReload(newQuery);
    }, [dispatch, query.search]);

    const handleReload = useCallback((query: ProductsQuery) => {
        dispatch(getPageFilters(query));
        dispatch(getProducts(query));
    }, [dispatch]);

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