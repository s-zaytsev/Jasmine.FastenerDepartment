import {type Control, Controller, type FieldErrors} from "react-hook-form";
import type {ChangeCompany} from "../../../../models/companyModels.ts";
import CompanyFormCard from "./CompanyFormCard.tsx";
import {ContactMailOutlined} from "@mui/icons-material";
import {Box, TextField} from "@mui/material";

type ContactsCompanyFormCardProps = {
    control: Control<ChangeCompany, unknown, ChangeCompany>;
    errors: FieldErrors<ChangeCompany>;
}

const ContactsCompanyFormCard = (props: ContactsCompanyFormCardProps) => {

    return (
        <CompanyFormCard title={'Контактная информация'} icon={<ContactMailOutlined/>}>
            <Box>
                <Box className={'w-full'}>
                    <Controller
                        name="email"
                        control={props.control}
                        rules={{required: 'Обязательное поле'}}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="Электронная почта"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.email}
                                helperText={props.errors.email?.message}
                            />
                        )}
                    />

                    <Controller
                        name="phoneNumber"
                        control={props.control}
                        rules={{required: 'Обязательное поле'}}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="Телефон"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.phoneNumber}
                                helperText={props.errors.phoneNumber?.message}
                            />
                        )}
                    />
                </Box>
            </Box>
        </CompanyFormCard>
    );
}

export default ContactsCompanyFormCard;