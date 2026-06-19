import {Box} from "@mui/material";
import type {ExtendedSupplier} from "../../models/supplierModels.ts";
import {ApartmentOutlined, ArrowRight, Edit, LocationOnOutlined} from "@mui/icons-material";
import Typography from "../../../shared/components/Typography.tsx";
import Card from "../../../shared/components/Card.tsx";
import IconButton from "../../../shared/components/buttons/IconButton.tsx";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";
import {semanticColors} from "../../../assets/variables/semanticColors.ts";
import IconBox from "../../../shared/components/IconBox.tsx";
import {memo} from "react";

type SupplierGridCardProps = {
    supplier: ExtendedSupplier;
    onEdit: (id: string) => void;
    onNavigateToSupplierProducts: (parameter: string) => void;
}

const SupplierGridCard = (props: SupplierGridCardProps) => {
    return (
        <Card>
            <Box className={'w-full'}>
                <Box className={'flex justify-between items-center'}>
                    <IconBox>
                        <ApartmentOutlined color={'primary'}/>
                    </IconBox>

                    <IconButton
                        description={'Редактировать'}
                        onClick={() => props.onEdit(props.supplier.id)}
                    >
                        <Edit/>
                    </IconButton>
                </Box>

                <Box className={'mt-[2rem]'}>
                    <Typography variant={'headlineH2'}>{props.supplier.name}</Typography>
                </Box>

                <Box className={'flex items-center mt-[0.5rem]'}>
                    <LocationOnOutlined
                        color={'action'}
                        sx={{
                            visibility: props.supplier.address ? 'visible' : 'hidden',
                            marginLeft: '-5px'
                        }}/>
                    <Typography variant={'bodySmall'} color={'tertiary'}>{props.supplier.address}</Typography>
                </Box>

                <Box className={'mt-[2rem] mb-[1rem]'} sx={{border: `1px solid ${semanticColors.surface.medium}`}}/>

                <Box className={'flex justify-between items-center'}>
                    <Box>
                        <Typography variant={'bodyRegular'} color={'tertiary'}>Товары</Typography>
                        <Typography variant={'bodyRegularBold'}>{props.supplier.productCount}</Typography>
                    </Box>

                    <Box>
                        <Typography variant={'bodyRegular'} color={'tertiary'}>Активные заказы</Typography>
                        <Typography variant={'bodyRegularBold'}
                                    color={'primary'}>{props.supplier.activeOrderCount}</Typography>
                    </Box>
                </Box>

                <Box className={'mt-[1rem] mb-[1rem]'} sx={{border: `1px solid ${semanticColors.surface.medium}`}}/>

                <Box className={'flex justify-end'}>
                    <FilledButton
                        variant={'text'}
                        sx={{paddingX: '0px', width: '100px'}}
                        onClick={() => props.onNavigateToSupplierProducts(props.supplier.id)}
                        className={'mx-[0]'}>
                        К товарам <ArrowRight/>
                    </FilledButton>
                </Box>
            </Box>
        </Card>
    );
}

export default memo(SupplierGridCard);