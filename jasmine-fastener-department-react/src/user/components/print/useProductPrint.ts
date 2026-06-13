import {useAppDispatch} from "../../../shared/hooks/sharedHooks.ts";
import {
    changeCount, clear,
    decrementCount,
    deleteAllProductsFromList,
    deleteProductFromList,
    incrementCount,
    removeProduct
} from "../../slices/PrintSlice.ts";
import {localStorageService} from "../../../shared/services/localStorageService.ts";
import {useCallback} from "react";

const useProductPrint = () => {
    const dispatch = useAppDispatch();

    const handleIncrement = useCallback((id: string) => {
        dispatch(incrementCount({id}));
    }, [dispatch]);

    const handleDecrement = useCallback((id: string) => {
        dispatch(decrementCount({id}));
    }, [dispatch]);

    const handleClearList = useCallback(() => {
        dispatch(deleteAllProductsFromList());
        localStorageService.clearAllCounts();
        dispatch(clear())
    }, [dispatch]);

    const handleChangeCount = useCallback((id: string, count: number) => {
        dispatch(changeCount({id, count}));
    }, [dispatch]);

    const handleDelete = useCallback((id: string) => {
        dispatch(removeProduct({id}));
        dispatch(deleteProductFromList(id));
    }, [dispatch]);

    return {
        handleIncrement,
        handleDecrement,
        handleChangeCount,
        handleDelete,
        handleClearList
    }
}

export default useProductPrint;