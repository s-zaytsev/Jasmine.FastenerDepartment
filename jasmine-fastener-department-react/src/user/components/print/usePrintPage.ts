import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {useEffect} from "react";
import {getProductsToPrint} from "../../slices/PrintSlice.ts";
import useProductPrint from "./useProductPrint.ts";
import useProductPreview from "./useProductPreview.ts";
import type {PrintPageState} from "../../models/printModels.ts";

const usePrintPage = () => {

    const state = useAppSelector<PrintPageState>(
        (state) => state.print
    );

    const dispatch = useAppDispatch();

    const {
        handleIncrement,
        handleDecrement,
        handleChangeCount,
        handleDelete,
        handleClearList
    } = useProductPrint();

    const {
        template,
        printRef,
        handlePrint
    } = useProductPreview();

    useEffect(() => {
        dispatch(getProductsToPrint());
    }, [dispatch]);

    return {
        products: state.products,
        handleIncrement,
        handleDecrement,
        handleChangeCount,
        handleDelete,
        handleClearList,
        printRef,
        handlePrint,
        template,
        isLoading: state.loading
    };
}

export default usePrintPage;