import {Box, Button} from "@mui/material";
import PrintForm from "./PrintForm.tsx";
import PrintPreview from "./PrintPreview.tsx";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import usePrintPage from "./usePrintPage.ts";

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
        <Box className={"h-full w-full flex justify-between"}>
            <Box className={'w-full h-full flex flex-col px-[1rem]'}>
                {!!products?.length &&
                    <Box className={'flex justify-between'}>
                        <Button onClick={handleClearList}>Очистить очередь печати</Button>
                        <FilledButton onClick={handlePrint} variant={'contained'}>Печать</FilledButton>
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
    )
}

export default PrintPage;