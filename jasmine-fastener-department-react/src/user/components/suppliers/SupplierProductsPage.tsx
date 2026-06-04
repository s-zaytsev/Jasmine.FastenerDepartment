import Page from "../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import ProductsSearch from "../products/ProductsSearch.tsx";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {useNavigate, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {
    changeNeededOrderStatus,
    changeNeededPrintStatus,
    changeOrderStatus,
    changePrintStatus
} from "../../slices/ProductsSlice.ts";
import type {ChangeSupplierProduct, SupplierProductsPageState} from "../../models/supplierModels.ts";
import {changeQuery, changeSupplierProduct, getPage, selectProduct} from "../../slices/SupplierProductsSlice.ts";
import SupplierProductsGrid from "./SupplierProductsGrid.tsx";
import SupplierProductDialog from "./SupplierProductDialog.tsx";

const SupplierProductsPage = () => {

    const state = useAppSelector<SupplierProductsPageState>(
        (state) => state.supplierProducts
    );
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const params = useParams();

    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    function handleNavigateToCreate() {
        navigate('add');
    }

    function handleNavigateToProduct(id: string) {
        navigate(`/products/${id}`)
    }

    function handlePrintStatus(id: string) {
        dispatch(changeNeededPrintStatus({id}));
        dispatch(changePrintStatus(id));
    }

    function handleOrderStatus(id: string) {
        dispatch(changeNeededOrderStatus({id}));
        dispatch(changeOrderStatus(id));
    }

    function handleOpenDialog(id: string) {
        dispatch(selectProduct({id}));
        handleOpen();
    }

    async function handleChange(model: ChangeSupplierProduct) {
        const id = state.selectedProduct?.id;

        if (!id) {
            return;
        }

        handleClose();
        await dispatch(changeSupplierProduct({id, model}));
        await dispatch(getPage(state.query));
    }

    const handleSort = (parameter: number) => {
        const sortDesc = state.query.sortBy !== parameter ? false : !state.query.sortDesc;
        const newQuery = {...state.query, sortBy: parameter, sortDesc: sortDesc, pageNo: 1};

        dispatch(changeQuery(newQuery))
        dispatch(getPage(newQuery));
    };

    const handlePageSizeChange = (size: number) => {
        const newQuery = {...state.query, pageNo: 1, pageSize: size};
        dispatch(changeQuery(newQuery));
        dispatch(getPage(newQuery));
    };

    const handlePageNoChange = (pageNo: number) => {
        const newQuery = {...state.query, pageNo: pageNo};
        dispatch(changeQuery(newQuery));
        dispatch(getPage(newQuery));
    };

    const handleSearch = (value: string) => {
        const newQuery = {...state.query, search: value, pageNo: 1, supplierId: params.id!};
        dispatch(changeQuery(newQuery));
        dispatch(getPage(newQuery));
    };

    useEffect(() => {
        if (!params.id) {
            navigate('/');
        }
        const newQuery = {...state.query, supplierId: params.id!};
        dispatch((changeQuery(newQuery)));
        dispatch(getPage(newQuery));
    }, []);

    return (
        <Page>
            <Box className={"flex justify-between w-full"}>
                <Box className={'flex items-center w-[50%]'}>
                    <ProductsSearch value={state.query.search} onSearch={handleSearch}/>
                </Box>

                <FilledButton onClick={handleNavigateToCreate} variant="contained">
                    Добавить
                </FilledButton>
            </Box>

            {state.loading && <Loader text={'Загрузка списка товаров'}/>}

            {!state.loading && <SupplierProductsGrid
                page={state.page}
                query={state.query}
                onSort={handleSort}
                onPageNoChanged={handlePageNoChange}
                onPageSizeChanged={handlePageSizeChange}
                onNavigateToProduct={handleNavigateToProduct}
                onChangePrintStatus={handlePrintStatus}
                onChangeOrderStatus={handleOrderStatus}
                onOpenDialog={handleOpenDialog}
            />
            }

            <SupplierProductDialog
                product={state.selectedProduct}
                open={open}
                onClose={handleClose}
                onSubmit={handleChange}
            />
        </Page>
    );
}

export default SupplierProductsPage;