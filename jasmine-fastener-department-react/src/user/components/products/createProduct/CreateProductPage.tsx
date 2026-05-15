import {useNavigate} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {ChangeProduct, CreateProductPageState, Product} from "../../../models/productModel.ts";
import {useEffect} from "react";
import {Box} from "@mui/material";
import Page from "../../../../shared/components/layout/Page.tsx";
import ProductForm from "../shared/ProductForm.tsx";
import PriceTagPreview from "../shared/PriceTagPreview.tsx";
import {
    changeProduct,
    getLastId, getProductTypes,
    getSuppliers,
    saveProduct,
    setError,
    setSuccess
} from "../../../slices/CreateProductSlice.ts";
import Loader from "../../../../shared/components/Loader.tsx";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import type {AxiosError} from "axios";

const CreateProductPage = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const notification = useNotify();

    const state = useAppSelector<CreateProductPageState>(
        (state) => state.createProduct
    );

    function handleFormChanged(model: ChangeProduct) {
        dispatch(changeProduct(model));
    }

    function handleCreate() {
        dispatch(saveProduct(state.changeModel));
    }
    
    useEffect(() => {
        if (state.success) {
            notification.notifySuccess(state.success);
            dispatch(setSuccess(undefined));
            navigate('/');
        }
    }, [state.success, navigate, notification, dispatch]);

    useEffect(() => {
        if (state.error) {
            notification.notifyError((state.error as AxiosError).message);
            dispatch(setError(undefined));
        }
    }, [state.error, navigate, notification, dispatch]);

    useEffect(() => {
        dispatch(getLastId());
        dispatch(getSuppliers());
        dispatch(getProductTypes());
    }, [dispatch])

    if (state.loading) {
        return <Loader text={'Загрузка данных для создания товара'}/>;
    }

    const product: Product = {
        number: state.changeModel.number,
        price: state.changeModel.price,
        isHardwareSizeEnabled: state.changeModel.isHardwareSizeEnabled,
        measurementUnitCode: state.changeModel.measurementUnitCode,
        name: state.changeModel.name,
        priceTagCode: state.changeModel.priceTagCode,
        id: '',
        isNeededToOrder: state.changeModel.isNeededToOrder,
        isNeededToPrint: state.changeModel.isNeededToPrint,
        historyEntries: [],
        suppliers: []
    }

    return (
        <Page>
            <Box className={"flex"}>
                <ProductForm
                    changeModel={state.changeModel!}
                    suppliers={state.suppliers}
                    productTypes={state.productTypes}
                    onSubmit={handleCreate}
                    onChanged={handleFormChanged}/>

                <PriceTagPreview product={product}/>
            </Box>
        </Page>
    )
}

export default CreateProductPage;