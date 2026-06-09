import {DocumentType} from "../../models/exportModels.ts";
import Page from "../../../shared/components/layout/Page.tsx";
import ExportDocumentCard from "./ExportDocumentCard.tsx";
import MicrosoftWordIcon from "../../../assets/MicrosoftWordIcon.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import useExportPage from "./useExportPage.ts";

const ExportPage = () => {
    
    const {
        handleDownload,
        loading
    } = useExportPage();

    if (loading) {
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