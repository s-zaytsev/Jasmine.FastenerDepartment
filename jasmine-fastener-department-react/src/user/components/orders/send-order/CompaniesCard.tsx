import Card from "../../../../shared/components/Card.tsx";
import {Box} from "@mui/material";
import IconBox from "../../../../shared/components/IconBox.tsx";
import {ApartmentOutlined} from "@mui/icons-material";
import Typography from "../../../../shared/components/Typography.tsx";
import type {Company} from "../../../models/companyModels.ts";
import {memo} from "react";
import CompaniesCardItem from "./CompaniesCardItem.tsx";

type CompaniesCardProps = {
    companies: Company[];
    selectedCompany?: Company;
    onSelectCompany: (id: string) => void;
}

const CompaniesCard = (props: CompaniesCardProps) => {
    return (
        <Card>
            <Box className={'w-full flex flex-col gap-[1rem]'}>
                <Box className={'flex items-center gap-[0.5rem]'}>
                    <IconBox>
                        <ApartmentOutlined color={'primary'}/>
                    </IconBox>

                    <Box className={'flex flex-col gap-[0.2rem]'}>
                        <Typography variant={'headlineH3'}>Компании</Typography>
                        <Typography variant={'labelSmall'} color={'tertiary'}>
                            Выберите компанию, данные которой будет отображены в бланке заказа
                        </Typography>
                    </Box>
                </Box>

                <Box className={'flex flex-col gap-[0.5rem]'}>
                    {props.companies.map(company => (
                        <CompaniesCardItem
                            key={company.id}
                            company={company}
                            isActive={props.selectedCompany?.id === company.id}
                            onChange={props.onSelectCompany}/>
                    ))}
                </Box>
            </Box>
        </Card>
    )
}

export default memo(CompaniesCard);