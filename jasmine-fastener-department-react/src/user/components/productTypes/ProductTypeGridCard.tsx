import type {ExtendedProductType} from "../../models/productTypeModels.ts";
import {Box} from "@mui/material";
import Typography from "../../../shared/components/Typography.tsx";
import {Edit, HandymanOutlined} from "@mui/icons-material";
import Card from "../../../shared/components/Card.tsx";
import IconButton from "../../../shared/components/buttons/IconButton.tsx";
import IconBox from "../../../shared/components/IconBox.tsx";

type ProductTypeGridCardProps = {
    productType: ExtendedProductType;
    onEdit: (id: string) => void;
}

const ProductTypeGridCard = (props: ProductTypeGridCardProps) => {
    return (
        <Box className={'w-[23%]'}>
            <Card>
                <Box className={'w-full'}>
                    <Box className={'flex justify-between items-center'}>
                        <IconBox>
                            <HandymanOutlined/>
                        </IconBox>

                        <IconButton
                            description={'Редактировать'}
                            onClick={() => props.onEdit(props.productType.id)}
                        >
                            <Edit/>
                        </IconButton>
                    </Box>

                    <Box className={'mt-[1rem]'}>
                        <Typography variant={'headlineH2'}>{props.productType.name}</Typography>
                    </Box>

                    <Box className={'flex items-center mt-[0.5rem] gap-[0.5rem]'}>
                        <Typography
                            variant={'bodyRegular'}
                            color={'tertiary'}
                        >
                            Всего товаров:
                        </Typography>

                        <Typography
                            variant={'bodyRegularBold'}
                            color={'primary'}
                        >
                            {props.productType.productCount}
                        </Typography>
                    </Box>
                </Box>
            </Card>
        </Box>
    );
}

export default ProductTypeGridCard;