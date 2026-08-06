import type {ChangeCompany} from "../../../models/companyModels.ts";
import {useForm} from "react-hook-form";
import {Box} from "@mui/material";
import DetailsCompanyFormCard from "./cards/DetailsCompanyFormCard.tsx";
import AddressCompanyFormCard from "./cards/AddressCompanyFormCard.tsx";
import {useEffect} from "react";
import ContactsCompanyFormCard from "./cards/ContactsCompanyFormCard.tsx";

type CompanyFormProps = {
    model: ChangeCompany;
    onSubmit: (model: ChangeCompany) => void;
}

const CompanyForm = (props: CompanyFormProps) => {
    const {
        formState: {
            isValid,
            errors
        },
        control,
        handleSubmit,
        reset,
    } = useForm<ChangeCompany>({
        values: props.model,
        mode: "onBlur"
    });

    const onSubmit = (data: ChangeCompany) => {
        if (!isValid) return;
        props.onSubmit(data);
    };

    useEffect(() => {
        reset(props.model);
    }, [reset, props.model]);

    return (
        <Box
            component="form"
            id="company-edit-form"
            onSubmit={handleSubmit(onSubmit)}
            className="flex flex-col w-full gap-[1rem]"
        >
            <DetailsCompanyFormCard control={control} errors={errors}/>
            <AddressCompanyFormCard control={control} errors={errors}/>
            <ContactsCompanyFormCard control={control} errors={errors}/>
        </Box>
    );
}

export default CompanyForm;