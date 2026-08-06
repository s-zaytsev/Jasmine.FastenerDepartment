import {type Control, Controller, type FieldErrors} from "react-hook-form";
import type {ChangeCompany} from "../../../../models/companyModels.ts";
import {LocationOnOutlined} from "@mui/icons-material";
import {Box, TextField} from "@mui/material";
import CompanyFormCard from "./CompanyFormCard.tsx";

type AddressCompanyFormCardProps = {
    control: Control<ChangeCompany, unknown, ChangeCompany>;
    errors: FieldErrors<ChangeCompany>;
}

const AddressCompanyFormCard = (props: AddressCompanyFormCardProps) => {
    return (
        <CompanyFormCard title={'Адрес'} icon={<LocationOnOutlined/>}>
            <Box>
                <Box className={'w-full'}>
                    <Controller
                        name="city"
                        control={props.control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="Город"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.city}
                                helperText={props.errors.city?.message}
                            />
                        )}
                    />
                </Box>

                <Box className={'w-full flex justify-between gap-[1rem]'}>
                    <Controller
                        name="street"
                        control={props.control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="Улица"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.street}
                                helperText={props.errors.street?.message}
                            />
                        )}
                    />

                    <Controller
                        name="buildingNumber"
                        control={props.control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="Номер здания"
                                variant="outlined"
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.buildingNumber}
                                helperText={props.errors.buildingNumber?.message}
                            />
                        )}
                    />
                </Box>
            </Box>
        </CompanyFormCard>
    );
}

export default AddressCompanyFormCard;