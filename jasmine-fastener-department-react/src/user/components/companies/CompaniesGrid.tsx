import type {Company} from "../../models/companyModels.ts";
import EmptyGrid from "../../../shared/components/EmptyGrid.tsx";
import {Box, Grow} from "@mui/material";
import CompaniesGridCard from "./CompaniesGridCard.tsx";
import {memo} from "react";

type CompaniesGridProps = {
    companies: Company[];
    onEdit: (id: string) => void;
}

const CompaniesGrid = (props: CompaniesGridProps) => {
    if (props.companies.length === 0) {
        return <EmptyGrid message={'Список компаний пуст'}/>
    }

    return (
        <Box className={'flex flex-wrap gap-[1rem]'}>
            {props.companies.map((supplier, index) =>
                <Grow key={supplier.id} in={true} timeout={index * 150}>
                    <Box className={'w-[23%]'}>
                        <CompaniesGridCard
                            company={supplier}
                            onEdit={props.onEdit}
                        />
                    </Box>
                </Grow>
            )}
        </Box>
    );
}

export default memo(CompaniesGrid);