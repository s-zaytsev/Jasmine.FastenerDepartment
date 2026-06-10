import {Box} from "@mui/material";
import PrintForm from "./PrintForm.tsx";
import PrintPreview from "./PrintPreview.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import usePrintPage from "./usePrintPage.ts";
import Page from "../../../shared/components/layout/Page.tsx";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";

const PrintPage = () => {

    const {
        products,
        handleIncrement,
        handleDecrement,
        handleChangeCount,
        handleDelete,
        handleClearList,
        printRef,
        handlePrint,
        template,
        isLoading
    } = usePrintPage();

    if (isLoading) {
        return <Loader text={'Загрузка очереди печати'}/>;
    }

    const rows = template(products);

    return (
        <Page
            title={'Печать'}
            description={'Настройка количества ценников и печать'}
            button={{
                label: 'Печать',
                onClick: handlePrint
            }}>
            <Box className={'h-full'}>
                <Box className={"h-full w-full flex justify-between"}>
                    <Box className={'w-full h-full flex flex-col px-[1rem]'}>
                        {!!products?.length &&
                            <Box className={'flex justify-end'}>
                                <FilledButton
                                    variant={'text'}
                                    onClick={handleClearList}
                                >
                                    Очистить очередь печати
                                </FilledButton>
                            </Box>}
                        <PrintForm
                            products={products}
                            onIncrement={handleIncrement}
                            onDecrement={handleDecrement}
                            onChangeCount={handleChangeCount}
                            onDelete={handleDelete}
                        />
                    </Box>
                    <div className={'w-full h-full'} ref={printRef}>
                        <PrintPreview products={products} template={rows}/>
                    </div>
                </Box>
            </Box>
        </Page>
    );
}

export default PrintPage;