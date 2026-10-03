import Page from "../../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import SendOrderForm from "./SendOrderForm.tsx";
import useSendOrderPage from "./useSendOrderPage.ts";
import TemplatePreview from "../../../../shared/components/templates/TemplatePreview.tsx";
import {FileDownloadOutlined} from "@mui/icons-material";

const SendOrderPage = () => {
    const {
        companies,
        recipients,
        templates,
        preview,
        selectedTemplate,
        selectedCompany,
        selectedRecipient,
        selectedFormats,
        handleSelectCompany,
        handleSelectRecipient,
        handleSelectTemplate,
        handleSelectFormat,
        handleChangeExtraFiles,
        handleSubmit,
        handleDownload
    } = useSendOrderPage()

    return (
        <Page
            title={'Отправка заказа'}
            description={'Выбор необходимых параметров и отправка заказа'}
            button={{
                label: 'Отправить',
                isDisabled: !selectedRecipient || !selectedTemplate || selectedFormats?.length === 0,
                onClick: handleSubmit
            }}
            iconButton={{
                icon: <FileDownloadOutlined/>,
                description: 'Скачать файлы заказа',
                isDisabled: !selectedTemplate || selectedFormats?.length === 0,
                onClick: handleDownload
            }}
        >
            <Box className={'h-full flex gap-[1rem]'}>
                <Box className={'w-2/5'}>
                    <SendOrderForm
                        companies={companies}
                        templates={templates}
                        recipients={recipients}
                        selectedTemplate={selectedTemplate}
                        selectedCompany={selectedCompany}
                        selectedRecipient={selectedRecipient}
                        selectedFormats={selectedFormats}
                        onSelectTemplate={handleSelectTemplate}
                        onSelectCompany={handleSelectCompany}
                        onSelectRecipient={handleSelectRecipient}
                        onSelectFormat={handleSelectFormat}
                        onChangeExtraFiles={handleChangeExtraFiles}
                    />
                </Box>

                <Box className={'w-3/5'}>
                    <TemplatePreview template={preview}/>
                </Box>
            </Box>
        </Page>
    )
}

export default SendOrderPage;