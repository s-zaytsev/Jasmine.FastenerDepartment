import type {ChangeOrder, ChangeOrderForm, ChangeOrderProduct, CreateOrder} from "../../../../../models/orderModels.ts";
import {Box} from "@mui/material";
import {useFieldArray, useForm} from "react-hook-form";
import {memo, useCallback, useMemo} from "react";
import useGroup from "../../../../../../shared/hooks/useGroup.ts";
import Section from "../../../../../../shared/components/section/Section.tsx";
import OrderProductsAmountGridSectionTable from "./OrderProductsAmountGridSectionTable.tsx";
import FormObserver from "../../../../../../shared/components/forms/FormObserver.tsx";

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

    const {groupBy} = useGroup();

    const groupedTablesData = useMemo(() => {
        const groupedByType = groupBy(
            fields,
            x => x.productType?.name ?? 'Остальное',
            {
                sortFn: (a, b) => a.productName.localeCompare(b.productName),
                sortGroups: true
            }
        );

        return Object.entries(groupedByType).map(([key, groupFields]) => ({
            key,
            rows: groupFields.map(field => ({
                field,
                productIndex: fields.findIndex(f => f.id === field.id),
            }))
        }));
    }, [fields, groupBy]);

    const handleUpdate = useCallback((products: any) => {
        props.onUpdate(products);
    }, [props.onUpdate]);

    const handleRemove = useCallback((id?: string, index?: number) => {
        props.onRemove(id);
        remove(index);
    }, [props.onRemove, remove]);

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

    return (
        <Box className={"w-[80%] m-auto p-[20px]"}>
            <Box component={"form"} className={'flex flex-col gap-[2rem]'}>
                <FormObserver control={control} name={'products'} onChange={handleUpdate}/>
                {groupedTablesData.map(({key, rows}) => (
                    <Section key={key} title={key}>
                        <OrderProductsAmountGridSectionTable
                            rows={rows}
                            control={control}
                            onRemove={handleRemove}
                        />
                    </Section>
                ))}
            </Box>
        </Box>);
}

export default memo(OrderProductsAmountGrid);