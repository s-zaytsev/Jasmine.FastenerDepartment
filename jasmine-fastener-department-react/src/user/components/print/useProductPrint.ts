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

const useProductPrint = () => {
    const dispatch = useAppDispatch();

    const handleIncrement = (id: string) => {
        dispatch(incrementCount({id}));
    }

    const handleDecrement = (id: string) => {
        dispatch(decrementCount({id}));
    }

    const handleClearList = () => {
        dispatch(deleteAllProductsFromList());
        localStorageService.clearAllCounts();
        dispatch(clear())
    }

    const handleChangeCount = (id: string, count: number) => {
        dispatch(changeCount({id, count}));
    }

    const handleDelete = (id: string) => {
        dispatch(removeProduct({id}));
        dispatch(deleteProductFromList(id));
    }

    return {
        handleIncrement,
        handleDecrement,
        handleChangeCount,
        handleDelete,
        handleClearList
    }
}

export default useProductPrint;