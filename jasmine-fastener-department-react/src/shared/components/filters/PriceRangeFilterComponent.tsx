import type {RangeFilter} from "../../models/models.ts";
import {Box, TextField} from "@mui/material";
import {useEffect, useState} from "react";

type PriceRangeFilterProps = {
    title: string;
    filter?: RangeFilter<number>;
    onChange: (from: number, to: number) => void;
}

function PriceRangeFilterComponent(props: PriceRangeFilterProps) {
    const [value, setValue] = useState<number[]>([props.filter?.from || 0, props.filter?.to || 0]);

    const handleChangePriceFrom = (price: number) => {
        const newValue = [price, value[1]];
        setValue(newValue);
    }

    const handleChangePriceTo = (price: number) => {
        const newValue = [value[0], price];
        setValue(newValue);
    }

    /*
        TODO: this useEffect breaks ProductPage useEffect

        useEffect(() => {
            const timerId = setTimeout(() => {
                props.onChange(value[0], value[1]);
            },500);
            return () => {
                clearTimeout(timerId);
            };
        }, [value]);
    */

    useEffect(() => {
        if (props.filter) {
            setValue([props.filter.from, props.filter.to]);
        }
    }, [props.filter?.from, props.filter?.to]);

    return (
        <Box className={'w-full'}>
            <Box className={'flex justify-between items-center'}>
                <TextField
                    className={'w-full'}
                    value={value[0]}
                    onChange={e => handleChangePriceFrom(+e.target.value)}
                />
                <span className={'mx-[0.5rem]'}>-</span>
                <TextField
                    className={'w-full'}
                    value={value[1]}
                    onChange={e => handleChangePriceTo(+e.target.value)}
                />
            </Box>
        </Box>
    );
}

export default PriceRangeFilterComponent;