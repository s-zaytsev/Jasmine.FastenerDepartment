import type {ChangeOrder, ChangeOrderForm, ChangeOrderProduct, CreateOrder} from "../../../../../models/orderModels.ts";
import {Box} from "@mui/material";
import {useFieldArray, useForm, useWatch} from "react-hook-form";
import {useEffect} from "react";
import useGroup from "../../../../../../shared/hooks/useGroup.ts";
import Section from "../../../../../../shared/components/section/Section.tsx";
import OrderProductsAmountGridSectionTable from "./OrderProductsAmountGridSectionTable.tsx";

type OrderListProps = {
    changeModel: ChangeOrder | CreateOrder;
    onUpdate: (products: ChangeOrderProduct[]) => void;
    onRemove: (id?: string) => void;
}

const OrderProductsAmountGrid = (props: OrderListProps) => {
    const {
        control,
    } = useForm<ChangeOrderForm>({
        defaultValues: {
            products: props.changeModel.products
        },
        mode: 'onChange'
    });

    const {
        fields,
        //     append,
        remove,
    } = useFieldArray({
        control,
        name: 'products'
    });

    const watchedProducts = useWatch({
        control,
        name: 'products'
    });

    const {groupBy} = useGroup();
    const groupedByType = groupBy(
        fields,
        x => x.productType?.name ?? 'Остальное',
        {
            sortFn: (a, b) => a.productName.localeCompare(b.productName),
            sortGroups: true
        });

    const onChange = (data: ChangeOrderForm) => {
        props.onUpdate(data.products);
    };

    const handleRemove = (id?: string, index?: number) => {
        props.onRemove(id);
        remove(index);
    }

    /*    const addField = () => {
            const model: ChangeOrderProduct = {
                productName: '',
                supplierProductNumber: '',
                ordered: {
                    value: 1,
                    measurementUnitCode: ProductMeasurementUnitCode.pieces
                }
            }

            append(model);
        }*/

    useEffect(() => {
        if (watchedProducts) {
            onChange({products: watchedProducts});
        }
    }, [watchedProducts]);

    return (
        <Box className={"w-[80%] m-auto p-[20px]"}>
            <Box component={"form"} className={'flex flex-col gap-[2rem]'}>
                {Object.entries(groupedByType).map(([key, groupFields]) => (
                    <Section key={key} title={key}>
                        <OrderProductsAmountGridSectionTable
                            rows={groupFields.map(field => ({
                                field,
                                productIndex: fields.findIndex(f => f.id === field.id),
                            }))}
                            control={control}
                            onRemove={handleRemove}
                        />
                    </Section>
                ))}
            </Box>
        </Box>);
}

export default OrderProductsAmountGrid;