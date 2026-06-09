import type {CompleteOrder, CompleteOrderForm} from "../../../models/orderModels.ts";
import {Box} from "@mui/material";
import {useFieldArray, useForm} from "react-hook-form";
import type {ProductType} from "../../../models/productTypeModels.ts";
import FilledButton from "../../../../shared/components/buttons/FilledButton.tsx";
import {useEffect} from "react";
import useGroup from "../../../../shared/hooks/useGroup.ts";
import Section from "../../../../shared/components/section/Section.tsx";
import CompleteOrderProductFormSectionTable from "./CompleteOrderProductFormSectionTable.tsx";

type CompleteOrderGridProps = {
    model: CompleteOrder;
    productTypes: ProductType[];
    onSubmit: (formData: CompleteOrderForm) => void;
}

const CompleteOrderProductForm = (props: CompleteOrderGridProps) => {
    const {
        control,
        getValues,
        reset,
    } = useForm<CompleteOrderForm>({
        defaultValues: {
            products: props.model.products
        },
        mode: 'onBlur'
    });

    const {
        fields,
        //    append,
        //     remove
    } = useFieldArray({
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


    /*
        const addField = () => {
            const model: CompleteOrderProduct = {
                orderProductId: undefined,
                productId: undefined,
                productName: '',
                addToPrint: true,
                removeOrderStatus: true,
                isFulfilled: true,
                supplierProductNumber: '',
                price: 0,
                ordered: {
                    value: 0,
                    measurementUnitCode: ProductMeasurementUnitCode.pieces
                },
                fulfilled: {
                    value: 1,
                    measurementUnitCode: ProductMeasurementUnitCode.pieces
                }
            }

            append(model);
        }
    */

    useEffect(() => {
        if (props.model.products.length > 0 && fields.length === 0) {
            reset({
                products: props.model.products
            });
        }
    }, [props.model.products, reset, fields.length]);

    return (
        <Box className={'w-[90%] m-auto'}>
            <Box component={"form"} className={'flex flex-col gap-[2rem]'}>
                {Object.entries(groupedByType).map(([key, groupFields]) => (
                    <Section key={key} title={key}>
                        <CompleteOrderProductFormSectionTable
                            rows={groupFields.map(field => ({
                                field,
                                productIndex: fields.findIndex(f => f.id === field.id),
                            }))}
                            control={control}/>
                    </Section>
                ))}
            </Box>

            <Box className={'flex justify-end mt-[1rem]'}>
                <FilledButton
                    onClick={() => props.onSubmit(getValues())}
                    variant={'contained'}
                >
                    Сохранить
                </FilledButton>
            </Box>
        </Box>
    )
}

export default CompleteOrderProductForm;