import type {Company} from "../../models/companyModels.ts";
import Card from "../../../shared/components/Card.tsx";
import {Box} from "@mui/material";
import IconBox from "../../../shared/components/IconBox.tsx";
import {ApartmentOutlined, Edit} from "@mui/icons-material";
import IconButton from "../../../shared/components/buttons/IconButton.tsx";
import Typography from "../../../shared/components/Typography.tsx";
import {memo} from "react";

type CompaniesGridCardProps = {
    company: Company;
    onEdit: (id: string) => void;
}

const CompaniesGridCard = (props: CompaniesGridCardProps) => {
    return (
        <Card>
            <Box className={'w-full'}>
                <Box className={'flex justify-between items-center'}>
                    <IconBox>
                        <ApartmentOutlined color={'primary'}/>
                    </IconBox>

                    <IconButton
                        description={'Редактировать'}
                        onClick={() => props.onEdit(props.company.id)}
                    >
                        <Edit/>
                    </IconButton>
                </Box>

                <Box className={'mt-[2rem] flex flex-col gap-[0.5rem]'}>
                    <Typography variant={'headlineH2'}>{props.company.title}</Typography>
                    <Typography variant={'labelRegular'} color={'tertiary'}>{props.company.type}</Typography>
                </Box>
            </Box>
        </Card>
    );
}

export default memo(CompaniesGridCard);