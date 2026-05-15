import {useNavigate, useParams} from "react-router-dom";
import {type SyntheticEvent, useEffect, useState} from "react";
import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import {changeProduct, getProduct, getProductTypes, getSuppliers, save} from "../../../slices/ChangeProductSlice.ts";
import type {ChangeProduct, ChangeProductPageState, Product} from "../../../models/productModel.ts";
import ProductForm from "../shared/ProductForm.tsx";
import Page from "../../../../shared/components/layout/Page.tsx";
import {Box, Tab, Tabs} from "@mui/material";
import PriceTagPreview from "../shared/PriceTagPreview.tsx";
import ProductHistory from "../history/ProductHistory.tsx";
import Loader from "../../../../shared/components/Loader.tsx";
import ProductHeader from "../shared/ProductHeader.tsx";

const ChangeProductPage = () => {
    const navigate = useNavigate();
    const params = useParams();
    const dispatch = useAppDispatch();
    const [tabIndex, setTabIndex] = useState(0);

    const state = useAppSelector<ChangeProductPageState>(
        (state) => state.changeProduct
    );

    function handleFormChanged(model: ChangeProduct) {
        dispatch(changeProduct(model));
    }

    const handleChangeTabIndex = (_event: SyntheticEvent, value: number) => {
        setTabIndex(value);
    };

    async function handleSubmit() {
        await dispatch(save({id: params.id, model: state.changeModel})).unwrap();
        navigate('/');
    }

    useEffect(() => {
        if (!params.id) {
            navigate('/products')
            return;
        }

        dispatch(getSuppliers());
        dispatch(getProductTypes());
        dispatch(getProduct(params.id!));
    }, [params.id, dispatch, navigate])

    const product: Product = {
        number: state.changeModel.number,
        price: state.changeModel.price,
        isHardwareSizeEnabled: state.changeModel.isHardwareSizeEnabled,
        measurementUnitCode: state.changeModel.measurementUnitCode,
        name: state.changeModel.name,
        priceTagCode: state.changeModel.priceTagCode,
        id: state.product?.id || '',
        isNeededToOrder: state.changeModel.isNeededToOrder,
        isNeededToPrint: state.changeModel.isNeededToPrint,
        historyEntries: state.product?.historyEntries || [],
        suppliers: state.suppliers || []
    }

    if (state.loading) {
        return <Loader text={`Загрузка информации о товаре`}/>;
    }

    return (
        <Page>
            <ProductHeader model={state.product}/>

            <Tabs value={tabIndex} onChange={handleChangeTabIndex} centered>
                <Tab label="Параметры"/>
                <Tab label={`История (${product.historyEntries.length})`}/>
            </Tabs>

            {tabIndex === 0 &&
                <Box className={"flex w-full"}>
                    <ProductForm
                        changeModel={state.changeModel}
                        suppliers={state.suppliers}
                        productTypes={state.productTypes}
                        onSubmit={handleSubmit}
                        onChanged={handleFormChanged}/>

                    <PriceTagPreview product={product}/>

                </Box>
            }

            {tabIndex === 1 &&
                <Box className={'w-full'}>
                    <ProductHistory history={product.historyEntries} productTypes={state.productTypes}/>
                </Box>
            }
        </Page>
    )
}

export default ChangeProductPage;