import {memo, useEffect} from "react";
import {Box} from "@mui/material";
import MainTemplateDataCard from "./MainTemplateDataCard.tsx";
import ContentForm from "./contentForms/ContentForm.tsx";
import TemplatePreview from "./TemplatePreview.tsx";
import {
    type ChangeTemplate,
    type OrderFormTemplateContent,
    type ProductCatalogTemplateContent,
    type TemplateContentTableColumnGroup,
    type TemplateType,
    TemplateTypeCode
} from "../../../models/templateModels.ts";
import {useForm} from "react-hook-form";

type TemplateFormProps = {
    model: ChangeTemplate;
    types: TemplateType[];
    columnGroups: TemplateContentTableColumnGroup[];
    preview: string;
    onUpdatePreview: (model: ChangeTemplate) => void;
    onSubmit: (model: ChangeTemplate) => void;
}

const TemplateForm = (props: TemplateFormProps) => {
    const {
        formState: {
            isValid,
            errors
        },
        getValues,
        setValue,
        control,
        handleSubmit,
        reset,
    } = useForm<ChangeTemplate>({
        defaultValues: props.model,
        mode: "onChange"
    });

    const onTypeChanged = (code: TemplateTypeCode) => {
        if (code === TemplateTypeCode.productCatalog) {
            const content: ProductCatalogTemplateContent = {
                groupByType: false,
                tableColumns: []
            };

            setValue('content', content);
        } else if (code === TemplateTypeCode.orderForm) {
            const content: OrderFormTemplateContent = {
                hasCompanyData: true,
                groupByType: false,
                tableColumns: []
            };

            setValue('content', content);
        }

        handleFormChange();
    }

    const onSubmit = (data: ChangeTemplate) => {
        if (!isValid) return;
        props.onSubmit(data);
    };

    const handleFormChange = () => {
        const currentValues = getValues();
        props.onUpdatePreview(currentValues);
    };

    useEffect(() => {
        reset(props.model);
    }, [props.model, reset]);

    return (
        <Box
            component="form"
            id="template-change-form"
            onSubmit={handleSubmit(onSubmit)}
            className="flex w-full gap-[1rem]"
        >
            <Box className={'w-2/5 flex flex-col gap-[0.5rem]'}>
                <MainTemplateDataCard
                    control={control}
                    errors={errors}
                    templateTypes={props.types}
                    onTypeChanged={onTypeChanged}
                />

                <ContentForm
                    control={control}
                    errors={errors}
                    columnGroups={props.columnGroups}
                    onChange={handleFormChange}
                />
            </Box>

            <Box className={'w-3/5 flex flex-col gap-[0.5rem]'}>
                <TemplatePreview template={props.preview}/>
            </Box>
        </Box>);
}

export default memo(TemplateForm);