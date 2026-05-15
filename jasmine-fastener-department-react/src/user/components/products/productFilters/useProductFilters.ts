import {changeQuery} from "../../../slices/ProductsSlice.ts";
import {PriceTagCode, type ProductPageFilters, type ProductsQuery} from "../../../models/productModel.ts";
import {useAppDispatch} from "../../../../shared/hooks/sharedHooks.ts";

const useProductFilters = (query: ProductsQuery, filters: ProductPageFilters) => {

    const dispatch = useAppDispatch();

    const handleResetPriceRange = () => {
        const newQuery = {
            ...query,
            priceFrom: filters.priceRange?.min || 0,
            priceTo: filters.priceRange?.max || 0,
            pageNo: 1
        };
        dispatch(changeQuery(newQuery));
    }

    const handleSupplierFilterChange = (id: string, isEnabled: boolean) => {
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
    };

    const handleTypeFilterChange = (id: string, isEnabled: boolean) => {
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
    };

    const handlePriceTagFilterChange = (id: PriceTagCode, isEnabled: boolean) => {
        let priceTags = query.priceTags || [];
        if (priceTags.find(x => x === id)) {
            priceTags = priceTags.filter(x => x !== id);
        } else if (isEnabled) {
            priceTags = [...priceTags, id]
        }

        const newQuery = {...query, priceTags, pageNo: 1};
        dispatch(changeQuery(newQuery));
    };

    const handleOnlyToPrintFilterChange = (isEnabled: boolean) => {
        const newQuery = {...query, onlyToPrint: isEnabled, pageNo: 1};
        dispatch(changeQuery(newQuery));
    }

    const handleOnlyToOrderFilterChange = (isEnabled: boolean) => {
        const newQuery = {...query, onlyToOrder: isEnabled, pageNo: 1};
        dispatch(changeQuery(newQuery));
    }

    const handlePriceRangeChange = (from: number, to: number) => {
        const newQuery = {...query, priceFrom: from, priceTo: to, pageNo: 1};
        dispatch(changeQuery(newQuery));
    }

    const handleResetTypeFilters = () => {
        const newQuery = {...query, types: [], pageNo: 1};
        dispatch(changeQuery(newQuery));
    }

    const handleResetSupplierFilters = () => {
        const newQuery = {...query, suppliers: [], pageNo: 1};
        dispatch(changeQuery(newQuery));
    }

    const handleResetPriceTagFilters = () => {
        const newQuery = {...query, priceTags: [], pageNo: 1};
        dispatch(changeQuery(newQuery));
    }

    const handleResetOnlyToPrintFilter = () => {
        const newQuery = {...query, onlyToPrint: false, pageNo: 1};
        dispatch(changeQuery(newQuery));
    }

    const handleResetOnlyToOrderFilter = () => {
        const newQuery = {...query, onlyToOrder: false, pageNo: 1};
        dispatch(changeQuery(newQuery));
    }

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
        handleResetPriceTagFilters
    };
}

export default useProductFilters;