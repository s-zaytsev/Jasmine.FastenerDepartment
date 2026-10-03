import {type Template, TemplateFormatCode} from "../../../models/templateModels.ts";
import type {Recipient} from "../../../models/recipientModels.ts";
import {Box} from "@mui/material";
import type {Company} from "../../../models/companyModels.ts";
import {memo} from "react";
import RecipientsCard from "./RecipientsCard.tsx";
import CompaniesCard from "./CompaniesCard.tsx";
import TemplatesCard from "./TemplatesCard.tsx";
import FormatsCard from "./FormatsCard.tsx";
import ExtraFilesCard from "./ExtraFilesCard.tsx";

type SendOrderFormProps = {
    companies: Company[];
    templates: Template[];
    recipients: Recipient[];
    selectedTemplate?: Template;
    selectedCompany?: Company;
    selectedRecipient?: Recipient;
    selectedFormats: TemplateFormatCode[];
    onSelectTemplate: (id: string) => void;
    onSelectCompany: (id: string) => void;
    onSelectRecipient: (id: string) => void;
    onSelectFormat: (code: TemplateFormatCode) => void;
    onChangeExtraFiles: (files: File[]) => void;
}

const SendOrderForm = (props: SendOrderFormProps) => {
    return (
        <Box className={'flex flex-col gap-[1rem]'}>
            <TemplatesCard
                templates={props.templates}
                selectedTemplate={props.selectedTemplate}
                onSelectTemplate={props.onSelectTemplate}
            />

            <FormatsCard
                selectedFormats={props.selectedFormats}
                onSelectFormat={props.onSelectFormat}
            />

            <CompaniesCard
                companies={props.companies}
                selectedCompany={props.selectedCompany}
                onSelectCompany={props.onSelectCompany}
            />

            <RecipientsCard
                recipients={props.recipients}
                selectedRecipient={props.selectedRecipient}
                onSelectRecipient={props.onSelectRecipient}
            />

            <ExtraFilesCard onChangeExtraFiles={props.onChangeExtraFiles}/>
        </Box>
    )
}

export default memo(SendOrderForm);