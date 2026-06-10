import Page from "../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import HistoryGrid from "./HistoryGrid.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import useHistoryPage from "./useHistoryPage.ts";

const HistoryPage = () => {

    const {
        navigateToProduct,
        loading,
        items,
        productTypes
    } = useHistoryPage();

    if (loading) {
        return <Loader text={'Загрузка истории изменения товаров'}/>;
    }

    return (
        <Page
            title={'История'}
            description={'История изменений характеристик товаров'}
        >
            <Box className={'flex justify-center w-full items-center'}>
                {//date pickers
                }
            </Box>

            <Box className={'w-full flex justify-center'}>
                <Box className={'flex flex-col w-[50%] gap-[2rem]'}>
                    {items && items.map((x, index) =>
                        <HistoryGrid
                            key={index}
                            date={x.date}
                            items={x.historyEntries}
                            productTypes={productTypes}
                            onNavigate={navigateToProduct}/>)}
                </Box>
            </Box>
        </Page>
    )
}

export default HistoryPage;