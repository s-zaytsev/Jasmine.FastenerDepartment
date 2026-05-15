import Page from "../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import {useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import type {HistoryPageState} from "../../models/historyModels.ts";
import HistoryGrid from "./HistoryGrid.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import useHistoryPage from "./useHistoryPage.ts";

const HistoryPage = () => {
    const state = useAppSelector<HistoryPageState>(
        (state) => state.history
    );

    const {
        navigateToProduct
    } = useHistoryPage();

    if (state.loading) {
        return <Loader text={'Загрузка истории изменения товаров'}/>;
    }

    return (
        <Page>
            <Box className={'flex justify-center w-full items-center'}>
                {//date pickers
                }
            </Box>

            <Box className={'w-full flex justify-center'}>
                <Box className={'flex flex-col gap-[2rem]'}>
                    {state.items && state.items.map((x, index) =>
                        <HistoryGrid
                            key={index}
                            date={x.date}
                            items={x.historyEntries}
                            productTypes={state.productTypes}
                            onNavigate={navigateToProduct}/>)}
                </Box>
            </Box>
        </Page>
    )
}

export default HistoryPage;