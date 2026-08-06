import CompanyFormCard from "./CompanyFormCard.tsx";
import {Box, TextField} from "@mui/material";
import {InfoOutlined} from "@mui/icons-material";
import {memo} from "react";
import {type Control, Controller, type FieldErrors} from "react-hook-form";
import type {ChangeCompany} from "../../../../models/companyModels.ts";

type DetailsCompanyFormCardProps = {
    control: Control<ChangeCompany, unknown, ChangeCompany>;
    errors: FieldErrors<ChangeCompany>;
}

const DetailsCompanyFormCard = (props: DetailsCompanyFormCardProps) => {
    return (
        <CompanyFormCard title={'Детали'} icon={<InfoOutlined/>}>
            <Box>
                <Box className={'w-full flex justify-between gap-[1rem]'}>
                    <Controller
                        name="title"
                        control={props.control}
                        rules={{required: 'Обязательное поле'}}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="Наименование"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.title}
                                helperText={props.errors.title?.message}
                            />
                        )}
                    />

                    <Controller
                        name="inn"
                        control={props.control}
                        rules={{required: 'Обязательное поле'}}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="ИНН"
                                variant="outlined"
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.inn}
                                helperText={props.errors.inn?.message}
                            />
                        )}
                    />
                </Box>

                <Box className={'w-full flex justify-between gap-[1rem]'}>
                    <Controller
                        name="firstName"
                        control={props.control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="Имя"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.firstName}
                                helperText={props.errors.firstName?.message}
                            />
                        )}
                    />

                    <Controller
                        name="middleName"
                        control={props.control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="Отчество"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.middleName}
                                helperText={props.errors.middleName?.message}
                            />
                        )}
                    />

                    <Controller
                        name="lastName"
                        control={props.control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="Фамилия"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.lastName}
                                helperText={props.errors.lastName?.message}
                            />
                        )}
                    />
                </Box>
            </Box>
        </CompanyFormCard>
    )
}

export default memo(DetailsCompanyFormCard);