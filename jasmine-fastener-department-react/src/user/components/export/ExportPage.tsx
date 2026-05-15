import {useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {DocumentType, type ExportPageState} from "../../models/exportModels.ts";
import Page from "../../../shared/components/layout/Page.tsx";
import ExportDocumentCard from "./ExportDocumentCard.tsx";
import MicrosoftWordIcon from "../../../assets/MicrosoftWordIcon.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import useExportPage from "./useExportPage.ts";

const ExportPage = () => {
    const state = useAppSelector<ExportPageState>(
        (state) => state.export
    );
    
    const {
        handleDownload
    } = useExportPage();

    if (state.loading) {
        return <Loader />;
    }

    return (
        <Page>
            <ExportDocumentCard
                title={'Microsoft Word'}
                description={'Скачать базу товаров в формате .docx'}
                icon={<MicrosoftWordIcon/>}
                onClick={() => handleDownload(DocumentType.word)}/>
        </Page>
    )
}

export default ExportPage;